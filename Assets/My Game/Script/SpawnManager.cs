using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

	// Static variable GameManager
	public static SpawnManager instance;

	// Spawn Points Struct
	[System.Serializable]
	public struct SpawnPoints
	{
		public ClientManager client;
		public Transform spawnPoint;
		public SpriteRenderer demand;
		public Slider progress;
	}

	// Public variable
	public SpawnPoints[] SpawnPointsTab;
	public ClientManager[] clientsTab;
	public Sprite[] demandTab;
	public float waitTime; 

	// Private variable
	private int _spawnNumber;

	// Use this for initialization for components and Physics
	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {

		// Deactivate all progress bar
		for (int i = 0; i < SpawnPointsTab.Length; i++) {
			SpawnPointsTab [i].progress.gameObject.SetActive (false);
		}
		// Call Coroutine
		StartCoroutine (SpawnClient (waitTime));
	}

	// Coroutine Spawn Clients 
	private IEnumerator SpawnClient(float waitTime)
	{
		while (true)
		{	
			if (GameManager.instance.gameState == GameManager.gameStates.Playing) {
				
				_spawnNumber = Random.Range (0, SpawnPointsTab.Length);
				if (SpawnPointsTab [_spawnNumber].client == null) {
					ActiveClient (_spawnNumber);
				}
				yield return new WaitForSeconds(waitTime);
			} else {
				yield  break;
			}
		}
	}

	// Active Client GameObject
	public void ActiveClient(int index){

		ClientManager _CM;

		// Active UI of this Spawn Point
		SpawnPointsTab[index].progress.gameObject.SetActive (true);

		// Choose a client
		SpawnPointsTab[index].client = clientsTab[Random.Range(0,clientsTab.Length)];
		_CM = SpawnPointsTab [index].client;

		// Set index to the client
		_CM.index = index;

		// Spawn Client
		Instantiate(SpawnPointsTab[index].client,SpawnPointsTab[index].spawnPoint);

		// Reset Slider value
		SpawnPointsTab[index].progress.maxValue = 100f;
		SpawnPointsTab[index].progress.value = SpawnPointsTab[index].progress.maxValue;

		// Start Couroutine
		StartCoroutine(ActiveProgressBar(index, 1f));
	}

	// Coroutine for each progress bar 
	public IEnumerator ActiveProgressBar(int index, float waitTime)
	{
		ClientManager _CM;
        float _minRange, _maxRange, reduceTime;

		_CM = SpawnPointsTab [index].client;
        _maxRange = GameManager.instance.RatioLevel() + _CM.nbProgressPoint;

        if (GameManager.instance.RatioLevel() - _CM.nbProgressPoint > 0.0F)
        {
            _minRange = GameManager.instance.RatioLevel() - _CM.nbProgressPoint;
        }
        else
        {
            _minRange = 0.0f;
        }

        reduceTime = Random.Range(_minRange, _maxRange);

        while (true) {

            if (GameManager.instance.gameState == GameManager.gameStates.Playing)
            {
			    SpawnPointsTab[index].progress.value -= reduceTime;
            }

			if (SpawnPointsTab[index].progress.value <= 0f) {
				PlayerStats.instance.lifePoints--;
				DeativateClient (index);
				yield break;
			} else {
				yield return new WaitForSeconds (waitTime);
			}
		}
	}


	public void DeativateClient(int index){
		// Destroy Client object spawned
		for (int i = 0; i < SpawnPointsTab [index].spawnPoint.childCount; i++) {
			if (SpawnPointsTab [index].spawnPoint.transform.GetChild (i).tag == "Client") {
				Destroy(SpawnPointsTab [index].spawnPoint.GetChild (i).gameObject);
			}
		}

        // Deactive UI of this Spawn Point
        //SpawnPointsTab[index].progress.value = SpawnPointsTab[index].progress.maxValue;
        StopCoroutine(ActiveProgressBar(index, 1f));
        SpawnPointsTab[index].progress.gameObject.SetActive (false);
		SpawnPointsTab [index].client = null;
		SpawnPointsTab [index].demand.sprite = null;

	}

	// Set Potion when client spawn
	public void ClientPotion(int index, GameObject potion) {

		SpriteRenderer _SR = SpawnPointsTab [index].demand;

		switch (potion.tag) {
		case "Potion Agility":
			_SR.sprite = demandTab [0];
			break;
		case "Potion Life":
			_SR.sprite = demandTab [1];
			break;
		case "Potion Love":
			_SR.sprite = demandTab [2];
			break;
		case "Potion Mana":
			_SR.sprite = demandTab [3];
			break;
		}
	}

	// Deactivate Spawning
	public void StopSpawning(){
		for (int i = 0; i < SpawnPointsTab.Length; i++) {
			DeativateClient (i);
		}
		// StopCoroutine (SpawnClient (waitTime));
	}
}

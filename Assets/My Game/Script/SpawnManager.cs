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
		public GameObject spawnPoint;
		public GameObject demand;
		public Slider progress;
		public GameObject client;
	}

	// Public variable
	public SpawnPoints[] SpawnPointsTab;
	public GameObject[] clientsTab;
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

		// UI Management
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

	// Coroutine for each progress bar 
	public IEnumerator ActiveProgressBar(int index, float waitTime)
	{
		ClientManager _CM;
		_CM = SpawnPointsTab [index].client.GetComponent<ClientManager> ();

		while (true) {

			SpawnPointsTab[index].progress.value -= _CM.nbProgressPoint;

			if (SpawnPointsTab[index].progress.value <= 0f) {
				PlayerStats.instance.lifePoints--;
				DeativateClient (index);
				yield break;
			} else {
				yield return new WaitForSeconds (waitTime);
			}
		}
	}

	// Active Client GameObject
	public void ActiveClient(int index){

		ClientManager _CM;

		// Active UI of this Spawn Point
		SpawnPointsTab[index].progress.gameObject.SetActive (true);

		// Choose a client
		SpawnPointsTab[index].client = clientsTab[Random.Range(0,clientsTab.Length)].gameObject;
		_CM = SpawnPointsTab [index].client.GetComponent<ClientManager> ();

		// Set index to the client
		_CM.index = index;

		// SpawnClient Client
		Instantiate(SpawnPointsTab[index].client,SpawnPointsTab[index].spawnPoint.transform);

		// Reset Slider value
		SpawnPointsTab[index].progress.maxValue = 100f;
		SpawnPointsTab[index].progress.value = SpawnPointsTab[index].progress.maxValue;

		// Start Couroutine
		StartCoroutine(ActiveProgressBar(index, 1f));
	}

	public void DeativateClient(int index){
		// Destroy Client object spawned
		for (int i = 0; i < SpawnPointsTab [index].spawnPoint.transform.childCount; i++) {
			if (SpawnPointsTab [index].spawnPoint.transform.GetChild (i).tag == "Client") {
				Destroy(SpawnPointsTab [index].spawnPoint.transform.GetChild (i).gameObject);
			}
		}
		// Deactive UI of this Spawn Point
		StopCoroutine (ActiveProgressBar(index, 1f));
		SpawnPointsTab[index].progress.gameObject.SetActive (false);
		SpawnPointsTab [index].progress.value = SpawnPointsTab [index].progress.maxValue;
		SpawnPointsTab [index].client = null;
		SpawnPointsTab [index].demand.GetComponent<SpriteRenderer> ().sprite = null;

	}

	// Set Potion when client spawn
	public void ClientPotion(int index, GameObject potion) {

		SpriteRenderer _SR = SpawnPointsTab [index].demand.GetComponent<SpriteRenderer> ();

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

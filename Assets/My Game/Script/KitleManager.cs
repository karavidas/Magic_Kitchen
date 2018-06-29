 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KitleManager : MonoBehaviour {

	// Public Variables
	public GameObject[] ingredients;
	public GameObject[] slots;
	public GameObject spawnPotion;
	public Slider progressBar;
	public Sprite backgroundSlot;
	public bool isCompleted = false;

	// Private Variables
	private GameObject _potion;
    private AudioSource _AM;
	private int _compteur;
	private bool isCooking = false;

	// Initialize Components & Physics
	void Awake() {
		if (progressBar != null) {
			progressBar.gameObject.SetActive (false);
		}
	}

    void Start(){
        _AM = GetComponent<AudioSource>();
    }

	// Update Loop 
	void Update(){
		if (GameManager.instance.gameState == GameManager.gameStates.Playing) {
			if (isCompleted) {
				CreationPotion ();
			}
		} else {
			progressBar.gameObject.SetActive (false);
			for (int i = 0; i < slots.Length; i++) {
				slots [i].gameObject.SetActive (false);
			}
		}
	}

    public void AddingElement(GameObject item) {
        if (!isCooking)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                // Fill ingredients spots
                if (ingredients[i] == null && item.gameObject.GetComponent<SpriteRenderer>().sprite != null)
                {
                    slots[i].GetComponent<Image>().sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
                    ingredients[i] = item;
                    RestartProgress();
                    break;
                }
            }
        }
    }

	// Initilialize Progress bar
	private void RestartProgress() {
		
		isCooking = true;
        _AM.Play();
		progressBar.gameObject.SetActive (true);
		progressBar.value = progressBar.maxValue;
		StartCoroutine (InProgress());
	}
		
	// Progress bar statut
	private IEnumerator InProgress(){

		while (true)
		{	
			progressBar.value -= 0.2f*progressBar.maxValue;

			if (progressBar.value <= 0f) {
				CheckIsCompleted ();
				progressBar.gameObject.SetActive (false);
                _AM.Stop ();
				isCooking = false;
				yield break;

			} else {
				yield return new WaitForSeconds (1f);
			}
		}
	}

	// Create the potion and Pop-Up and reset Ingredients
	private void CreationPotion(){

		// Identify the type of the potion created
		_potion = null;
		for (int i = 0; i < MainPotionManager.instance.PotionTab.Length; i++) {
			if (ingredients.SequenceEqual (MainPotionManager.instance.PotionTab[i].ingredients)) {
				_potion = MainPotionManager.instance.PotionTab[i].potion;
			}
		}
		// Failed Potion
		if (_potion == null) {
			_potion = MainPotionManager.instance.PotionTab[MainPotionManager.instance.PotionTab.Length-1].potion;
		}

		// Potion Spawn
		Vector3 targetPos = new Vector3 (spawnPotion.transform.position.x, spawnPotion.transform.position.y, 0);
		Instantiate (_potion, targetPos, Quaternion.Euler (Vector3.zero));

		// Reset Ingredients Tab
		for ( int i = 0; i < ingredients.Length; i++)
		{
			ingredients[i] = null;
		}

		// Reset Background slots
		for ( int i = 0; i < slots.Length; i++)
		{
			slots [i].GetComponent<Image> ().sprite = backgroundSlot;
		}

		// Reset Bool isCompleted
		isCompleted = false;
	}

	// Check if completed
	private void CheckIsCompleted() {

		_compteur = 0;

		for (int i = 0; i < ingredients.Length; i++) {
			if (ingredients [i] != null) {
				_compteur += 1; 
			}
		}

		if (_compteur == ingredients.Length) {
			isCompleted = true;
		} 
	}
}

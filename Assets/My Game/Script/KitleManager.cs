using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KitleManager : MonoBehaviour {

	// Public Variables
	public GameObject[] ingredients;
	public Slider progressBar;
	public GameObject spawnPotion;
	public bool isCompleted = false;
	public GameObject[] slots;
	public Sprite backgroundSlot;

	// Private Variables
	private GameObject _potion;
	private bool isCooking = false;
	private int _compteur;
	//private IEnumerator coroutine;
    private AudioSource _AM;
	private IngredientDragged _id;

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

	// On Click: add ingredients
	void OnMouseDown() {

		if (!isCooking && PlayerStats.instance.CurrentIngredient != null) {

			// Delete Object Dragged
			_id = FindObjectOfType<IngredientDragged> ();

			for (int i = 0; i < ingredients.Length; i++) {
				if (ingredients[i] == null) {

					// Fill ingredients spots
					if (_id.gameObject.GetComponent<SpriteRenderer> ().sprite != null) {
						slots [i].GetComponent<Image> ().sprite = _id.gameObject.GetComponent<SpriteRenderer> ().sprite;
						_id.gameObject.GetComponent<SpriteRenderer> ().sprite = null;
						Destroy (_id.gameObject);

						ingredients [i] = PlayerStats.instance.CurrentIngredient;
						PlayerStats.instance.CurrentIngredient = null;
						RestartProgress ();
					}
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
				//progressBar.value = progressBar.maxValue;
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

		/*if (ingredients.SequenceEqual (MainPotionManager.instance.creationPotion1)) {
			_potion = MainPotionManager.instance.potionTab [0];
		} else if (ingredients.SequenceEqual (MainPotionManager.instance.creationPotion2)) {
			_potion = MainPotionManager.instance.potionTab [1];
		} else if (ingredients.SequenceEqual (MainPotionManager.instance.creationPotion3)) {
			_potion = MainPotionManager.instance.potionTab [2];
		}
		else if (ingredients.SequenceEqual(MainPotionManager.instance.creationPotion4)) {
			_potion = MainPotionManager.instance.potionTab [3];
		} else {
			_potion = MainPotionManager.instance.potionTab [MainPotionManager.instance.potionTab.Length-1];
		}*/

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

		//if (!isCooking) {
		for (int i = 0; i < ingredients.Length; i++) {
			if (ingredients [i] != null) {
				_compteur += 1; 
			}
		}

		if (_compteur == ingredients.Length) {
			isCompleted = true;
		} 
		//} 
	}
}

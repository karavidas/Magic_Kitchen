using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPotionManager : MonoBehaviour {

	// Static variable GameManager
	public static MainPotionManager instance;

	[System.Serializable]
	public struct PotionType
	{
		public GameObject potion;
		public GameObject[] ingredients;
		public Image[] slotsGrimoire;
	}

	public PotionType[] PotionTab;
	public GameObject[] ingredientsTab;

	// Use this for initialization for components and Physics
	void Awake(){
		instance = this;

		// Build potion recipe randomly
		for (int i = 0; i < PotionTab.Length; i++) {
			for (int j = 1; j < PotionTab[i].ingredients.Length; j++) {
				PotionTab [i].ingredients [j] = ingredientsTab[Random.Range (0,ingredientsTab.Length)];
				PotionTab [i].slotsGrimoire[j].sprite = PotionTab [i].ingredients [j].GetComponent<SpriteRenderer>().sprite;
			}
		}
	}
}

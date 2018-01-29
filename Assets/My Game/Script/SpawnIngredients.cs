using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIngredients : MonoBehaviour {

	public static SpawnIngredients instance;

	public GameObject[] itemsTab;
	public GameObject[] spawnTab;

	void Awake(){
		instance = this;

		// Spawning ingredients
		for (int i = 0; i < spawnTab.Length; i++) {
			while (true) {
				int _index = Random.Range (0, itemsTab.Length);
				GameObject _ingredient = itemsTab [_index];

				if (_ingredient != null) {
					Instantiate (_ingredient, spawnTab [i].transform);
					itemsTab [_index] = null;
					break;
				}
			}
		}
	}
}

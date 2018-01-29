using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

	public GameObject ingredient;
	private IngredientDragged _id;

	void OnMouseDown() {
		// Ingredient GameObject
		if (PlayerStats.instance.CurrentIngredient == null) {
			PlayerStats.instance.CurrentIngredient = ingredient;
			TakeIngredient();
		} else {
			_id = FindObjectOfType<IngredientDragged> ();
			if (PlayerStats.instance.CurrentIngredient.tag != gameObject.tag) {

				Destroy (_id.gameObject);
				TakeIngredient ();
			} else {

				Destroy (_id.gameObject);
				PlayerStats.instance.CurrentIngredient = null;
			}

		}


		/*// Ingredient GameObject
		if (PlayerStats.instance.CurrentIngredient != null) {

			_id = FindObjectOfType<IngredientDragged> ();

			if (PlayerStats.instance.currentIngredientDragged.tag != gameObject.tag) {

				Destroy (_id.gameObject);
				TakeIngredient ();
			} else {
				
				Destroy (_id.gameObject);
				PlayerStats.instance.currentIngredientDragged = null;
			}

		} else {
			
			TakeIngredient();
		}*/

	}

	void TakeIngredient(){
		//PlayerStats.instance.currentIngredientDragged = ingredient.gameObject;
		Vector3 _targetPos = new Vector3 (transform.position.x, transform.position.y, 0f);
		Instantiate (ingredient, _targetPos, Quaternion.Euler (Vector3.zero));
	}
}

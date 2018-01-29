using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDragged : MonoBehaviour {

	public float moveSpeed;
	
	// Update is called once per frame
	void Update () {
		
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10f);
		Vector3 _targetPos = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = Vector3.Lerp (transform.position, _targetPos, moveSpeed * Time.deltaTime);

	}

	/*void OnMouseDown(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		// Casts the ray and get the first game object hit
		Physics.Raycast(ray, out hit);
		Debug.Log("This hit at " + hit.transform.tag );
	}*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour {

	// If the potion is the good one
	void OnMouseDown() {
		Destroy (PlayerStats.instance.currentPotion.gameObject);
	}
}

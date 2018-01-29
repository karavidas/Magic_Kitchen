using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour {

	/*public string potion;

	public string Potion
	{
		get { return potion; }
		set { potion = value; }
	}*/

	void OnMouseDown() {
		PlayerStats.instance.currentPotion = gameObject;
	}
}

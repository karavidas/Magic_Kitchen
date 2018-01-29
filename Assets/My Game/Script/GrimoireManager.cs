using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimoireManager : MonoBehaviour {



	// Use this for initialization
	void Awake () {
		Close ();
	}

	void OnMouseDown(){
		UIManager.instance.GrimoireUI (1);
	}

	public void Close(){
		UIManager.instance.GrimoireUI (0);
	}

}

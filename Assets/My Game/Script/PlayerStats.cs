using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

	// Static variable PlayerStats
	public static PlayerStats instance;

	//Public Variables
	public int lifePoints = 3;

	// Private Variables
	[SerializeField]
	private int _gold;
	[SerializeField]
	private GameObject _currentIngredient;

	public GameObject currentPotion;

	public int Gold
	{
		get { return _gold; }
		set { _gold = value; }
	}

	public GameObject CurrentIngredient
	{
		get { return _currentIngredient; }
		set { _currentIngredient = value; }
	}
		

	void Awake() {
		instance = this;
	}

	void Update() {

		UIManager.instance.goldText.text = "" + Gold;

		UIManager.instance.ModifyLife (lifePoints);
		if (lifePoints <= 0) {
			GameManager.instance.gameState = GameManager.gameStates.GameOver;
		}
	}
		
}

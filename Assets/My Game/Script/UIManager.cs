using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	//Public Variables
	[Header("Main UI")]
	public GameObject[] heartTab;
	public GameObject grimoire;
    public Canvas pause;
	public Text goldText;
    public Image goldImage;


	[Space]

	[Header("End game UI")]
	public Button playAgain;
	public Button quitGame;
	public Text endScore;

	void Awake() {
		instance = this;
	}

    private void Update()
    {
        if (pause != null)
        {
            if (GameManager.instance.gameState == GameManager.gameStates.Pause)
            {
                pause.gameObject.SetActive(true);
            }
            else
            {
                pause.gameObject.SetActive(false);
            }
        }
    }

    // Use this for initialization
    public void ShowStartUI () {
		if (grimoire != null ) {
			grimoire.gameObject.SetActive (false);
		}
        if (pause != null)
        {
            pause.gameObject.SetActive(false);
        }
        if (goldText != null ) {
			goldText.gameObject.SetActive (true);
		}
        if (goldImage != null)
        {
            goldImage.gameObject.SetActive(true);
        }
        if (playAgain != null) {
			playAgain.gameObject.SetActive (false);
		}
		if (quitGame != null) {
			quitGame.gameObject.SetActive (false);
		}
		if (endScore != null) {
			endScore.gameObject.SetActive (false);
		}
	}

	public void ShowEndUI(){
		if (grimoire != null ) {
			grimoire.gameObject.SetActive (false);
		}
		if (goldText != null ) {
			goldText.gameObject.SetActive (false);
		}
        if (goldImage != null)
        {
            goldImage.gameObject.SetActive(false);
        }
        if (playAgain != null) {
			playAgain.gameObject.SetActive (true);
		}
		if (quitGame != null) {
			quitGame.gameObject.SetActive (true);
		}
		if (endScore != null) {
			endScore.gameObject.SetActive (true);
			endScore.text = "" + PlayerStats.instance.Gold;
		}
	}

	public void GrimoireUI(int i){
		if (grimoire != null) {
			if (i == 0 || GameManager.instance.gameState != GameManager.gameStates.Playing) {
				grimoire.gameObject.SetActive (false);
			} else {
				grimoire.gameObject.SetActive (true);	
			}
		}
	}

	public void ModifyLife(int value){
		switch (value) {
		case 3:
			for (int i = 0; i < heartTab.Length; i++) {
				heartTab [i].SetActive (true);
			}
			break;
		case 2:
			for (int i = 0; i < heartTab.Length; i++) {
				if (i < heartTab.Length-1) {
					heartTab [i].SetActive (true);
				} else {
					heartTab [i].SetActive (false);
				}
			}
			break;
		case 1:
			for (int i = 0; i < heartTab.Length; i++) {
				if (i < heartTab.Length-2) {
					heartTab [i].SetActive (true);
				} else {
					heartTab [i].SetActive (false);
				}
			}
			break;
		case 0:
			for (int i = 0; i < heartTab.Length; i++) {
				heartTab [i].SetActive (false);
			}
			break;
		}
	}
}

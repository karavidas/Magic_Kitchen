using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour {

	/*// Static variable GameManager
	public static ProgressManager instance;

	public int[] maxValue;
	public int[] nbProgressPoint;
	public float waitTime;

	private Slider[] _slider;

	void Awake() {
		instance = this;
		_slider = GetComponentsInChildren<Slider> ();
	}

	// Use this for initialization
	void Start () {

		for (int i = 0; i < _slider.Length; i++) {
			ResetProgress (i);
		}

		StartCoroutine (ActiveProgressBar ());
	}

	public void ResetProgress(int index){
		_slider[index].maxValue = maxValue[index];
		_slider[index].value = maxValue[index];
	}

	// Progress Bar Client
	private IEnumerator ActiveProgressBar()
	{
		while (true)
		{

			yield return new WaitForSeconds(waitTime);

			for (int i = 0; i < _slider.Length; i++) {
				if (_slider[i].IsActive()) {
					_slider[i].value -= nbProgressPoint[i];

					if (_slider[i].value <= 0f) {
						SpawnManager.instance.DesactiveClient (i);
					}

				}
			}
		}
	}*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	static bool exists;

	private AudioSource[] audioTab;

	void Awake ()
	{
		if (!exists) {
			exists = true;
			DontDestroyOnLoad (transform.gameObject);
		} else {
			Destroy (gameObject);
		}
		instance = this;
	}

	// Use this for initialization
	/*void Start ()
	{
		audioTab = GetComponent<AudioSource> ();
	}

	public void MagicGirlSound ()
	{
		audioTab [1].Play ();
	}

	public void PotionPopUp ()
	{
		audioTab [2].Play ();
	}

	public void PlayCookingSound ()
	{
		audioTab [3].Play ();
	}

	public void StopCookingSound ()
	{
		audioTab [3].Stop ();
	}

	public void ZombieSound ()
	{
		audioTab [4].Play ();
	}*/
}

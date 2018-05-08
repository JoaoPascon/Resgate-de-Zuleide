using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Audio : MonoBehaviour {

	public AudioSource[] audios;
	public AudioSource musics;
	public GameObject[] buttonsAudio;
	public GameObject[] buttonsMusic;
	public int prefAudio, prefMusic;

	// Use this for initialization
	void Start () {

		prefAudio = PlayerPrefs.GetInt ("audioController");
		prefMusic = PlayerPrefs.GetInt ("musicController");

		if (prefAudio == 1)
			enableAudio ();
		else
			disableAudio ();

		if (prefMusic == 1)
			enableMusic ();
		else
			disableMusic ();

	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void audioLaser(){
		audios [0].enabled = true;
		audios [0].Play ();
	}

	public void audioExplosion(){
		audios[1].enabled = true;
		audios[1].Play ();
	}

	public void audioPoweUp(){
		audios[2].enabled = true;
		audios[2].Play ();
	}

	public void audioAlarmeVida(){
		audios[3].enabled = true;
		audios[3].Play ();
	}

	public void audioAlarmeChefe(){
		audios[4].enabled = true;
		audios[4].Play ();
	}


	public void enableAudio(){
		foreach (var item in audios) {
			item.volume = 100f;
		}
		buttonsAudio [0].SetActive (false);
		buttonsAudio [1].SetActive (true);
		PlayerPrefs.SetInt ("audioController", 1);
	}

	public void disableAudio(){
		foreach (var item in audios) {
			item.volume = 0f;
		}
		buttonsAudio [1].SetActive (false);
		buttonsAudio [0].SetActive (true);
		PlayerPrefs.SetInt ("audioController", 0);
	}

	public void enableMusic(){
		musics.volume = 100f;
		buttonsMusic [0].SetActive (false);
		buttonsMusic [1].SetActive (true);
		PlayerPrefs.SetInt ("musicController", 1);
	}

	public void disableMusic(){
		musics.volume = 0f;
		buttonsMusic [1].SetActive (false);
		buttonsMusic [0].SetActive (true);
		PlayerPrefs.SetInt ("musicController", 0);
	}

}

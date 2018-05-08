using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabledAudioAndTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("musicController", 1);
		PlayerPrefs.SetInt ("audioController", 1);
		PlayerPrefs.SetInt ("tutorial", 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

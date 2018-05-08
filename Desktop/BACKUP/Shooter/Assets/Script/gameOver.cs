using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class gameOver : MonoBehaviour {

	public	Text	score, recorde;

	// Use this for initialization
	void Start () {

		score.text = PlayerPrefs.GetInt ("pontos").ToString ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void restartPlay(){

		SceneManager.LoadScene ("MenuEscolha");

	}
}

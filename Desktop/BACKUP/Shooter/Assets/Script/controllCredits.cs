using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllCredits : MonoBehaviour {

	public GameObject gameObject;
	private bool	  pause=true;
	private string    nome;
	public Text firstName;


	// Use this for initialization
	void Start () {

		nome = PlayerPrefs.GetString ("Nome");
		int x = nome.IndexOf (" ");
		firstName.text = x > 0 ? nome.Remove (x) : nome;

	}
	
	// Update is called once per frame
	void Update () {

		if (!pause)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void pauseAnimation(){
		pause = !pause;
	}


}

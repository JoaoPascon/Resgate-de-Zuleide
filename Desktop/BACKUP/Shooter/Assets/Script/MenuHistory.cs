using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHistory : MonoBehaviour {

	public GameObject[] buttons;
	public GameObject[] panel;
	public int			fase;

	// Use this for initialization
	void Start () {

		fase = PlayerPrefs.GetInt ("fases");
		for (int i = 0; i <= fase; i++) {
			buttons [i].SetActive (true);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enabledPanel(int x){

		panel [x].SetActive (true);

	}

	public void disablePanel(int x){

		panel [x].SetActive (false);
		
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEscolha : MonoBehaviour {

	public	GameObject[]	fases;
	public  int 		fase;

	// Use this for initialization
	void Start () {
		fase = PlayerPrefs.GetInt ("fases");
		for (int i = 0; i <= fase; i++) {
			fases [i].SetActive (true);
		}

	}
	
	// Update is called once per frame
	void Update () {
		

	}


}

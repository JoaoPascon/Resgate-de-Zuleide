using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour {

	public GameObject	panels;
	private int ativaTutorial;


	void Start () {

		panels.SetActive (false);
		ativaTutorial = PlayerPrefs.GetInt ("tutorial");
		if(ativaTutorial == 1){
			panels.SetActive (true);
		}
	
		
	}
	
	// Update is called once per frame
	void Update () {
		print (ativaTutorial);
	}

	public void closeTutorial(){
		panels.SetActive (false);
		PlayerPrefs.SetInt("tutorial", 2);
	}

	public void openTutorial(){
		panels.SetActive (true);
	}

}

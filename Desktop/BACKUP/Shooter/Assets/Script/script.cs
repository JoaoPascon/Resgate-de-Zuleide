using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {

	public string stringToEddit = "";
	public string seuNome;
	public Texture2D textureToDisplay;

	void OnGUI (){

		seuNome = PlayerPrefs.GetString ("Nome");
		stringToEddit = GUI.TextField (new Rect (20, 1200, 500, 40),seuNome,20);
		GUI.Label(new Rect(20, 1150, 200 , 200), "DIGITE SEU NOME");
		PlayerPrefs.SetString ("Nome", stringToEddit);
	}

	// Use this for initialization
	void Start () {
		
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

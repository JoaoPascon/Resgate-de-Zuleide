using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerceiroGc : MonoBehaviour {

	public		GameObject		SpanwsD,SpanwsE, chefao3;
	private		int 			Ispawns, chamaChefe;
	public		float 			tempo;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (chamaChefe < 10) {
			ControlaSpawns ();
		} 
		else {
			chefao3.SetActive (true);
			SpanwsD.SetActive (false);
			SpanwsE.SetActive (false);
		}

	}


	public void ControlaSpawns(){
		tempo += Time.deltaTime;


		if (tempo > 5) {
			SpanwsD.SetActive (false);
			SpanwsE.SetActive (true);
		} 
		if (tempo > 10) {
			tempo = 0;
			SpanwsD.SetActive (true);
			SpanwsE.SetActive (false);
			chamaChefe++;
		}
		
		


	}


		
}

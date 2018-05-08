using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luaAmarela : MonoBehaviour {

	public lights scriptLigth;

	void Start () {
		scriptLigth = FindObjectOfType (typeof(lights)) as lights;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {
		case "Player":
			chamaLights(this.gameObject.name);
			break;
		}

	}


	public void chamaLights(string nome){

		switch (nome) {
		case "luzAmarela":
			scriptLigth.luzAmarela ();
			break;
		case "luzVerde":
			scriptLigth.luzVerde ();
			break;
		case "luzVermelha":
			scriptLigth.luzVermelha ();
			break;
		case "luzAzul":
			scriptLigth.luzAzul ();
			break;
		}

	}
}

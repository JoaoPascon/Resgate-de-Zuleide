﻿using UnityEngine;
using System.Collections;

public class meteoros : MonoBehaviour {

	public 	int				HP;
	public	GameObject		prefabexplosao;
	public	int 			contadorMeteor;
	public  SpawnMeteoros	scriptMeteor;

	public 	GameObject[] 	dropItens;
	public  GameObject 		miniMeteors;
	public  GameObject		meteorTransform;
	public	GameObject[]	extras; 



	// Use this for initialization
	void Start () {

		scriptMeteor = FindObjectOfType (typeof(SpawnMeteoros)) as SpawnMeteoros;
		
	}
	
	// Update is called once per frame
	void Update () {
		meteorTransform = GetComponent<GameObject> ();
	}

	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {

		case "tiro":
			tomarDano(1);
			break;
		case "limiteDown":
			Destroy (this.gameObject);
			scriptMeteor.contadorMeteoro++;
			break;
		case "limiteTop":
			Destroy (this.gameObject);
		
			break;
		case "limiteRight":
			Destroy (this.gameObject);

			break;
		case "limiteLeft":
			Destroy (this.gameObject);
		
			break;
		case "especial":
			tomarDano (200);
			break;
		}




	}


	public void tomarDano(int dano){
		HP -= dano;

		if(HP <= 0)
		{
			stantiedNewMeteors ();
			explodir ();
			scriptMeteor.contadorMeteoro++;
			if (this.name == "meteoro3(Clone)"){
				dropaItem ();

			}
		}
	}

	public void explodir(){
		Instantiate (prefabexplosao, transform.position, transform.rotation);
		Destroy (this.gameObject);

	}

	public void dropaItem(){
		int i = Random.Range (0, 4);
		Instantiate (dropItens[i], transform.position, transform.rotation);
	}

	public void stantiedNewMeteors(){
		
		for (int i = 0; i < 3; i++) {
			GameObject tempDrop = Instantiate (miniMeteors, transform.position, transform.rotation) as GameObject;
			transform.position = new Vector3 (transform.position.x + 0.1f, transform.position.y, transform.position.z);
		}

	}
}

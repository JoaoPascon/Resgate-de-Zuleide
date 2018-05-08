using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour {

	public GameObject prefabexplosao;
	private float speedY;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {

		case "limiteDown":
			Destroy (this.gameObject);
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
			Destroy (this.gameObject);
			break;
		case "Player":
			Destroy (this.gameObject);
			break;
		}
		explodir ();
	}

	public void explodir(){
		Instantiate (prefabexplosao, transform.position, transform.rotation);
		Destroy (this.gameObject);

	}


}

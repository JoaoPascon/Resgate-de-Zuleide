using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class armaNave : MonoBehaviour {

	public	GameObject		tiroPreFab;
	public  GameObject[]	arms;
	public	float 			forcaTiro;
	public	Audio 			scriptAudio;

	// Use this for initialization
	void Start () {


		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;


	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire1")){

			atirar ();

		}
	}

	public void atirar(){
		GameObject tempPrefab = Instantiate (tiroPreFab, transform.position, transform.rotation) as GameObject;
		tempPrefab.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, forcaTiro));
		scriptAudio.audioLaser ();
	}


	public void lua(){
		tiroPreFab = arms[0];
	}

	public void sabre(){
		tiroPreFab = arms[1];
	}

	public void red(){
		tiroPreFab = arms[2];
	}

}

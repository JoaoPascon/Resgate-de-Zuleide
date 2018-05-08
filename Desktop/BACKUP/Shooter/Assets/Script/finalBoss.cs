using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class finalBoss: MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade, i;

	public	int			HP;

	public	GameObject	prefabExplosao;

	public	GameObject	prefabTiro;
	public 	float 		tempTiro; //de quanto em quanto tempo ele atira;
	private	float		esperaTiro;
	private SpriteRenderer	chefeSR;

	public	GameObject		Spawns;
	private Audio 			scriptAudio;
	private int				proximaFase = 5;
	public  GameObject		panelDanger;
	public float 			tempo, tempoParaDano;

	public GameObject[] dropItens;
	public Animation animation;

	// Use this for initialization
	void Start () {
		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;

		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();
		scriptAudio.audioAlarmeChefe ();
		Spawns.SetActive (false);

	}

	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;
		esperaTiro += Time.deltaTime;
		//CONTROLA O TEMPO DO TIRO
		if (tempTiro <= esperaTiro) {  
			esperaTiro = 0;
			StartCoroutine ("atirar");
		}


	}

	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {

		case "tiro":
			tomarDanoChefe (1);
			break;
		case "especial":
			tomarDanoChefe (5);
			break;
		}



	}




	public void tomarDanoChefe(int dano){
		if (tempo > tempoParaDano) {
			tempo = 0;
			HP -= dano;
			chefeSR.color = new Color (255, 0, 0);
			this.gameObject.layer = 10;

			if (tempTiro >= 0.5f) {
				tempTiro -= 0.02f;
			}
			StartCoroutine ("pisca");

			if (HP <= 0) {
				explodirChefe ();
			}
		}
	}

	public void explodirChefe(){
		scriptAudio.audioExplosion ();
		Destroy (this.gameObject);
		Instantiate (prefabExplosao, transform.position, transform.rotation);
		dropItem ();
	}

	IEnumerator atirar(){

		yield return new WaitForSeconds (1);
		GameObject tempFabTiro = Instantiate (prefabTiro, Spawns.transform.position, prefabTiro.transform.rotation) as GameObject;
		tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -80));

	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		yield return new WaitForSeconds (0.3f);
		chefeSR.color = new Color (1, 1, 1, 1);
		this.gameObject.layer = 11;
	}


	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}

	public void dropItem(){
		foreach (var item in dropItens) {
			GameObject tempDrop = Instantiate (item, transform.position, transform.rotation) as GameObject;
		}

	}

}
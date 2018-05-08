using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class finalBoss2: MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade, i;

	public	int			HP;

	public	GameObject		prefabExplosao;

	public 	float 			tempTiro; //de quanto em quanto tempo ele atira;
	public	float			esperaTiro;
	private SpriteRenderer	chefeSR;

	public	GameObject[] 	ligthsPowers;
	private Audio 			scriptAudio;
	private int				proximaFase = 5;
	public  GameObject		panelDanger;
	public float 			tempo, tempoParaDano;

	public GameObject[] dropItens;
	public Animation animation;

	// Use this for initialization
	void Start () {
		tempTiro = 5;
		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		scriptAudio.audioAlarmeChefe ();
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();

	}

	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;
		esperaTiro += Time.deltaTime;
		//CONTROLA O TEMPO DO TIRO
		if (tempTiro <= esperaTiro) {  
			esperaTiro = 0;
			StartCoroutine ("enabledLigths");
			tempTiro = 3;
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
			tempTiro -= 0.05f;
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
		
	IEnumerator enabledLigths(){//para piscar a cor quando tomar dano
		ligthsPowers[0].SetActive(true);
		ligthsPowers[1].SetActive(true);
		yield return new WaitForSeconds (1f);
		ligthsPowers[0].SetActive(false);
		ligthsPowers[1].SetActive(false);
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
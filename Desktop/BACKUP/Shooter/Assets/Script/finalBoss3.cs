using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class finalBoss3: MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade, i;

	public	int			HP;

	public	GameObject		prefabExplosao;

	public 	float 			tempTiro; //de quanto em quanto tempo ele atira;
	public	float			esperaTiro;
	private SpriteRenderer	chefeSR;

	private Audio 			scriptAudio;
	private int				proximaFase = 6;
	public  GameObject		panelDanger;
	public float 			tempo, tempoParaDano;

	public Animation animation;

	// Use this for initialization
	void Start () {
		
		tempTiro = 5;
		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();
		scriptAudio.audioAlarmeChefe ();
	}

	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;
		esperaTiro += Time.deltaTime;
		//CONTROLA O TEMPO DO TIRO
		if (tempTiro <= esperaTiro) {  
			esperaTiro = 0;
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
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 6 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		Destroy (this.gameObject);
		Instantiate (prefabExplosao, transform.position, transform.rotation);
		SceneManager.LoadScene ("creditos");

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



}
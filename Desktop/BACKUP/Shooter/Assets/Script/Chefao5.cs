using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Chefao5 : MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade, i;
	public	float 		movimentoX, movimentoY;
	public	Transform	limiteLeft, LimiteRight;

	public	int			HP;

	public	GameObject	prefabExplosao;

	public	GameObject	prefabTiro;
	public	Transform[]	armaPosition;
	public 	float 		tempTiro; //de quanto em quanto tempo ele atira;
	private	float		esperaTiro;
	private SpriteRenderer	chefeSR;

	public	GameObject		Spawns;
	private Audio 			scriptAudio;
	public  _GC 			script_GC;
	private int				proximaFase = 5;
	public  GameObject		panelDanger;
	public float tempo, tempoParaDano;

	// Use this for initialization
	void Start () {
		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		script_GC = FindObjectOfType (typeof(_GC)) as _GC;

		scriptAudio.audioAlarmeChefe ();
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();

		Spawns.SetActive (false);


	}

	// Update is called once per frame
	void Update () {


		esperaTiro += Time.deltaTime;

		//CONTROLA O TEMPO DO TIRO
		if (tempTiro <= esperaTiro) {  
			esperaTiro = 0;
			StartCoroutine ("atirar");
		}
			



		//NAO PERMITE ELE SAIR DA TELA
		if (transform.position.x > LimiteRight.position.x) {
			chefeRb.transform.position = new Vector2 (LimiteRight.position.x, transform.position.y);
			movimentoX *= -1;
		}
		if (transform.position.x < limiteLeft.position.x) {
			chefeRb.transform.position = new Vector2 (limiteLeft.position.x, transform.position.y);
			movimentoX *= -1;
		}

		if (transform.position.y > 2.65f) {
			chefeRb.velocity = new Vector2 (0, movimentoY * velocidade);
		}
		else
		{
			chefeRb.velocity = new Vector2 (movimentoX * velocidade, 0);
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
		HP -= dano;
		chefeSR.color = new Color (1,1,1, 0.05f);
		this.gameObject.layer = 10;
		tempTiro -= 0.02f;
		StartCoroutine ("pisca");
		movimentoX *= 1.05f;
		if (transform.localScale.x >= 2) {
			transform.localScale -= new Vector3 (0.2f, 0.2f, 0.2f);
		}
		if (HP <= 0) {
			explodirChefe ();
		}

	}

	public void explodirChefe(){
		scriptAudio.audioExplosion ();
		Destroy (this.gameObject);
		Instantiate (prefabExplosao, transform.position, transform.rotation);
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 5 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		SceneManager.LoadScene ("MenuEscolha");

	}

	IEnumerator atirar(){

		yield return new WaitForSeconds (1);
		GameObject tempFabTiro = Instantiate (prefabTiro, armaPosition [0].position, prefabTiro.transform.rotation) as GameObject;
		tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -80));

	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		yield return new WaitForSeconds (2);
		chefeSR.color = new Color (1, 1, 1, 1);
		this.gameObject.layer = 11;
	}


	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}

}

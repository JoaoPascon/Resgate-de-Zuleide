using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Chefao3 : MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade;
	public	float 		movimentoX, movimentoY;
	public	Transform	limiteLeft, LimiteRight;

	public	int			HP;

	public	GameObject	prefabExplosao;

	public	GameObject	prefabTiro;
	public	Transform[]	armaPosition;
	public 	float 		tempTiro; //de quanto em quanto tempo ele atira;
	private	float		esperaTiro;
	private SpriteRenderer	chefeSR;

	public  float 			tempoParaDano, tempo;
	private int				proximaFase = 3;
	private Audio 			scriptAudio;
	private  _GC 			script_GC;
	public	GameObject 		TerceiroGC;
	public  GameObject		panelDanger;
	// Use this for initialization
	void Start () {

		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		script_GC = FindObjectOfType (typeof(_GC)) as _GC;
	
		TerceiroGC.SetActive (false);
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();
		scriptAudio.audioAlarmeChefe ();


	}

	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;


		chefeRb.velocity = new Vector2 (movimentoX, movimentoY);

		if (HP < 20) 
		{
			movimentoY = -5;
			if (HP < 10) {
				movimentoY = -10;
			}
		}
	}
		

	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {

		case "tiro":
			tomarDanoChefe (1);
			break;
		case "limiteDown":
			posicionaChefe ();
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
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 3 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		SceneManager.LoadScene ("MenuEscolha");

	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		yield return new WaitForSeconds (0.2f);
		chefeSR.color = new Color (1, 1, 1);
	}

	public void posicionaChefe(){
		float i = Random.Range (-1.50f, 1.50f);
		transform.position = new Vector2 (i, 6);
	}

	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}


}



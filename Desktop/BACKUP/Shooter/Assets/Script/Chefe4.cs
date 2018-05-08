using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Chefe4 : MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade;
	public	float 		movimentoX, movimentoY;
	public	Transform	limiteLeft, LimiteRight, limiteDown, limiteTop;

	public	int			HP;

	public	GameObject	prefabExplosao;

	public	GameObject	prefabTiro;
	public	Transform[]	armaPosition;
	public 	float 		tempTiro; //de quanto em quanto tempo ele atira;
	private	float		esperaTiro;
	private SpriteRenderer	chefeSR;

	public  float 			tempoParaDano, tempo;
	private int				proximaFase = 4;
	private Audio 			scriptAudio;
	private  _GC 			script_GC;
	private	TerceiroGc 		TerceiroGC;
	public  GameObject 		panelDanger;
	private controllLight 	scriptControllLight;

	// Use this for initialization
	void Start () {
		
		StartCoroutine ("danger");
		scriptControllLight = FindObjectOfType (typeof(controllLight)) as controllLight;
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		script_GC = FindObjectOfType (typeof(_GC)) as _GC;
		TerceiroGC = FindObjectOfType (typeof(TerceiroGc)) as TerceiroGc;
		scriptControllLight.disableLigths ();
		scriptAudio.audioAlarmeChefe ();
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();
		StartCoroutine ("chamaChefe");

	}

	// Update is called once per frame
	void Update () {
		scriptControllLight.disableLigths ();
		tempo += Time.deltaTime;
		chefeRb.velocity = new Vector2 (movimentoX, movimentoY);

		if (transform.position.y > limiteTop.position.y) {
			movimentoX = Random.Range (-5, 5); movimentoY = Random.Range (-6, -1);
		}
		if (transform.position.y < limiteDown.position.y) {
			movimentoX = Random.Range (-5, 4); movimentoY = Random.Range (3, 6);
		}
		if (transform.position.x < limiteLeft.position.x) {
			movimentoX = Random.Range (2, 5); movimentoY = Random.Range (-5, 4);
		}
		if (transform.position.x > LimiteRight.position.x) {
			movimentoX = Random.Range (-2, -4); movimentoY = Random.Range (-5, 4);
		}

		esperaTiro += Time.deltaTime;

		if (esperaTiro >= tempTiro) {  
			esperaTiro = 0;
			atirar ();
		}


		if (HP < 30) 
		{
			tempTiro = 0.7f;
			if (HP < 10) {
				tempTiro = 0.2f;
			}
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
		case "limiteDown":
			posicionaChefe ();
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
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 4 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		Instantiate (prefabExplosao, transform.position, transform.rotation);
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

	public void atirar(){
		GameObject tempFabTiro = Instantiate (prefabTiro, transform.position, prefabTiro.transform.rotation) as GameObject;
		tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -50));

	}

	IEnumerator chamaChefe(){
		yield return new WaitForSeconds (5);
		movimentoX = 1;
	}

	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}

}


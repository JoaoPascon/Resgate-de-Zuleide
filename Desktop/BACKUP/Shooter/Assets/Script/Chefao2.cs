using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Chefao2 : MonoBehaviour {

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

	private Audio 			scriptAudio;
	public  _GC 			script_GC;
	private int				proximaFase = 2;
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

	}

	// Update is called once per frame
	void Update () {
		tempo += Time.deltaTime;
		if (transform.position.y > 2.65f) {
			chefeRb.velocity = new Vector2 (0, movimentoY * velocidade);
		}
		else
		{
			chefeRb.velocity = new Vector2 (movimentoX * velocidade, 0);
		}

		esperaTiro += Time.deltaTime;

		//CONTROLA O TEMPO DO TIRO
		if (tempTiro <= esperaTiro) {  
			esperaTiro = 0;
			StartCoroutine ("atirar");
		}

		if (HP < 50 && HP > 20 ) {
			tempTiro = 1.5f;

		} 
		if (HP <= 20) {
			tempTiro = 1.2f;
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
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 2 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		SceneManager.LoadScene ("MenuEscolha");

	}

	IEnumerator atirar(){

		yield return new WaitForSeconds (1);
			GameObject tempFabTiro = Instantiate (prefabTiro, armaPosition [0].position, prefabTiro.transform.rotation) as GameObject;
			tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -50));

	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		yield return new WaitForSeconds (0.2f);
		chefeSR.color = new Color (1,1,1,1);
	}

	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}

}



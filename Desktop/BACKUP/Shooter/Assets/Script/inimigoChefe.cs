using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class inimigoChefe : MonoBehaviour {

	private Rigidbody2D chefeRb;
	public	int 		velocidade;
	public	float 		movimentoX, movimentoY;
	public	Transform	limiteLeft, LimiteRight;

	public	int			HP;

	public	GameObject	prefabExplosao;

	public	GameObject	prefabTiro;
	public	Transform[]	armaPosition;
	public 	int 		tempTiro; //de quanto em quanto tempo ele atira;
	private	float		esperaTiro;
	private SpriteRenderer	chefeSR;
	public  GameObject 		Spawn;

	private Audio 			scriptAudio;
	public  _GC 			script_GC;
	private MenuEscolha 	MenuEscolhaScript;
	public  float 			tempoParaDano, tempo;
	public  GameObject		panelDanger;
	private	int 			proximaFase=1;

	// Use this for initialization
	void Start () {
		StartCoroutine ("danger");
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		script_GC = FindObjectOfType (typeof(_GC)) as _GC;
		MenuEscolhaScript = FindObjectOfType (typeof(MenuEscolha)) as MenuEscolha;

		//transform.position = new Vector2 (0, 2); //coloca o chefa na posicao correta
		chefeRb = GetComponent<Rigidbody2D> ();
		chefeSR = GetComponent<SpriteRenderer> ();
		scriptAudio.audioAlarmeChefe ();
		Spawn.SetActive (false);
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
			
	
		//NAO PERMITE ELE SAIR DA TELA
		if ((transform.position.x - 0.2) > LimiteRight.position.x) {
			chefeRb.transform.position = new Vector2 (LimiteRight.position.x, transform.position.y);
			movimentoX *= -1;
		}
		if ((transform.position.x + 0.2) < limiteLeft.position.x) {
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
		proximaFase = (proximaFase >= PlayerPrefs.GetInt ("fases")) ? 1 :PlayerPrefs.GetInt ("fases");
		PlayerPrefs.SetInt ("fases", proximaFase);
		SceneManager.LoadScene ("MenuEscolha");
	}

	IEnumerator atirar(){

		yield return new WaitForSeconds (1);


			for (int i = 0; i < 3; i++) {
				GameObject tempFabTiro = Instantiate (prefabTiro, armaPosition [i].position, prefabTiro.transform.rotation) as GameObject;
				tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -100));
			}
		
		if (HP <= 10) {
			for (int i = 0; i < 5; i++) {
				if (i < 3) {
					GameObject tempFabTiro = Instantiate (prefabTiro, armaPosition [i].position, prefabTiro.transform.rotation) as GameObject;
					tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -300));
				}
				else {
					GameObject tempFabTiro = Instantiate (prefabTiro, armaPosition [i].position, prefabTiro.transform.rotation) as GameObject;
					tempFabTiro.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, -300));
				}
			}
		}
	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		yield return new WaitForSeconds (0.2f);
		chefeSR.color = new Color (255, 255, 255);
	}

	IEnumerator danger(){
		panelDanger.SetActive (true);
		yield return new WaitForSeconds (3f);
		panelDanger.SetActive (false);
	}
		
}




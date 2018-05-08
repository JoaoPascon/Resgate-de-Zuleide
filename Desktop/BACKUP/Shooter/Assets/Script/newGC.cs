using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class newGC : MonoBehaviour {

	public float tempoAtual, tempoRecorde;
	public Text txtTempoAtual, txtTempoRecorde, txtTempoReal;
	public int	pontuacao;

	public	Transform		posicaoVidas;
	public	int 			vidasExtras;
	public	GameObject		iconeVidas;

	public	GameObject[]	extras; 

	public  Button			buttonItem;
	public  GameObject 		itemEspecial;
	public  int				slotEspecial;

	public	Transform 		spawnPlayer;
	public	GameObject		preFabPlayer;


	public	GameObject		Inimigo;
	public 	Inimigo			scriptInimigo;

	// Use this for initialization

	public	Player			scriptPlayer;
	public 	Button 			ativaTela;

	private bool pause;
	public GameObject panelPause, panelRecord;


	//tempo de fase
	public float		tempFase=0, tempFase2=0, tempController;

	void Start () {

		Time.timeScale = 0;
		panelPause.SetActive (true);
		buttonItem.interactable = true;

		VidasExtras (); //crias os icones da via
		scriptInimigo = FindObjectOfType(typeof(Inimigo)) as Inimigo;
		scriptPlayer = FindObjectOfType(typeof(Player)) as Player;
	}

	// Update is called once per frame
	void Update () {


		tempoAtual += Time.deltaTime;
		txtTempoAtual.text = "" + tempoAtual.ToString ();
		txtTempoReal.text = txtTempoAtual.text;

		if(Input.GetButtonDown("Fire1")){ativaTela.interactable = true;}

	}

	void VidasExtras(){

		float posXico;
		GameObject tempVidas;

		foreach (GameObject v in extras) // pega os objetos dentrodo array Extras
		{

			if (v != null) // verifica quais estao ativos para depois destruir eles
			{
				Destroy (v);
			}
		}


		for (int i = 0; i <= vidasExtras; i++) 
		{

			posXico = posicaoVidas.position.x + (0.3f * i); // calcula a distancia correta das vidas
			tempVidas = Instantiate (iconeVidas) as GameObject; // cria a vida
			extras[i] = tempVidas;
			tempVidas.transform.position = new Vector3 (posXico, posicaoVidas.position.y, posicaoVidas.position.z);
			//posiciona a vida no local correto
		}
	
		StartCoroutine ("renascerEspera");
	}

	public void morreu(){
		vidasExtras--;
		if (vidasExtras >= 0) {
			VidasExtras ();
			buttonItem.interactable = true;
		}
		else{
			PlayerPrefs.SetFloat ("atual", tempoAtual);
			validaPontuação ();

		}
	}


	IEnumerator renascerEspera(){
		yield return new WaitForSeconds (1);
		GameObject tempPlayer = Instantiate (preFabPlayer) as GameObject; //  criar um novo jogador
		tempPlayer.transform.position = spawnPlayer.position; // coloca na posicao correta
		tempPlayer.name = "Player";

		//		scriptPlayer.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.2f);
		//StartCoroutine ("vuneravel");
	}
		


	IEnumerator especial(){
		GameObject tempPrefabEx = Instantiate (itemEspecial, transform.position, transform.rotation) as GameObject;
		yield return new WaitForSeconds (0.7f);
		Destroy (tempPrefabEx);
	}


	public void onPause(){

			Time.timeScale = 1;
			panelPause.SetActive (false);
		
	}


	//Botões Gerais

	public void chamaEspecial(){

		StartCoroutine ("especial");
		buttonItem.interactable = false;	

	}

	public void validaPontuação(){

		tempoRecorde = tempoRecorde >= tempoAtual ? tempoRecorde : tempoAtual;
		PlayerPrefs.SetFloat ("recorde", tempoRecorde);
		float tempAc = PlayerPrefs.GetFloat ("recorde");
		txtTempoRecorde.text = ""+tempAc.ToString();
		panelRecord.SetActive (true);
		Time.timeScale = 0;
	}
}

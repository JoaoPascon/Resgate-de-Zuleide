using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class _GC : MonoBehaviour {

	public Text pontos;
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
	public	GameObject 		naveChefe; // para seta na tela o chefe
	public 	GameObject		subChefe;
	public 	Inimigo			scriptInimigo;

	// Use this for initialization

	public	Player			scriptPlayer;
	public 	Button 			ativaTela;

	private bool pause;
	public GameObject panelPause;


	//tempo de fase
	public float		tempFase=0, tempFase2=0, tempController;

	void Start () {

		pause = true;
		onPause ();
		panelPause.SetActive (false);
		buttonItem.interactable = true;

		Inimigo.SetActive (true);
		VidasExtras (); //crias os icones da via
		scriptInimigo = FindObjectOfType(typeof(Inimigo)) as Inimigo;
		scriptPlayer = FindObjectOfType(typeof(Player)) as Player;
	}
	
	// Update is called once per frame
	void Update () {
		
	    
		tempController += Time.deltaTime;
		pontos.text = "" + pontuacao.ToString ();


		if (tempFase < tempController) {
			chamaChefe ();
		}
		else if (tempFase2 < tempController) {

			chamaSubChefe ();
		}

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


		for (int i = 0; i < vidasExtras; i++) 
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
			PlayerPrefs.SetInt ("pontos", pontuacao);
			SceneManager.LoadScene ("GameOver");
		}
	}

	public void chamaChefe(){
		naveChefe.SetActive(true); //seta o chefe na tela
	}

	public void chamaSubChefe(){
		subChefe.SetActive(true); //seta o chefe na tela
	}


	public void MenuEscolha(){
		SceneManager.LoadScene ("MenuEscolha");
	}



	IEnumerator renascerEspera(){
		yield return new WaitForSeconds (1);
		GameObject tempPlayer = Instantiate (preFabPlayer) as GameObject; //  criar um novo jogador
		tempPlayer.transform.position = spawnPlayer.position; // coloca na posicao correta
		tempPlayer.name = "Player";
//		scriptPlayer.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.2f);
		//StartCoroutine ("vuneravel");
	}


	public void adicionaVida(){
		vidasExtras++;

		//REPETE AQUI PARA N INSTACNIAR DOIS JOGADORES NA TELA
		float posXico;
		GameObject tempVidas;

		foreach (GameObject v in extras) // pega os objetos dentrodo array Extras
		{

			if (v != null) // verifica quais estao ativos para depois destruir eles
			{
				Destroy (v);
			}
		}


		for (int i = 0; i < vidasExtras; i++) 
		{
			posXico = posicaoVidas.position.x + (0.3f * i); // calcula a distancia correta das vidas
			tempVidas = Instantiate (iconeVidas) as GameObject; // cria a vida
			extras[i] = tempVidas;
			tempVidas.transform.position = new Vector3 (posXico, posicaoVidas.position.y, posicaoVidas.position.z);
			//posiciona a vida no local correto
		}
	}


	IEnumerator especial(){
		GameObject tempPrefabEx = Instantiate (itemEspecial, transform.position, transform.rotation) as GameObject;
		yield return new WaitForSeconds (0.7f);
		Destroy (tempPrefabEx);
	}





	//Botões Gerais

	public void chamaEspecial(){
		
			StartCoroutine ("especial");
			buttonItem.interactable = false;	

	}
		
	public void onPause(){
		pause = !pause;
		if (!pause) {
			Time.timeScale = 1;
			panelPause.SetActive (false);
		} else if (pause) {
			Time.timeScale = 0;
			panelPause.SetActive (true);
		}

	}

	public void RestartSccene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}


	public void Opcoes(){

		SceneManager.LoadScene ("Opcoes");
	}

	public void Creditos(){

		SceneManager.LoadScene ("Creditos");
	}

	public void	play1(){

		SceneManager.LoadScene ("Play");

	}
		
	public void	play2(){

		SceneManager.LoadScene ("Play2");

	}

	public void play3(){

		SceneManager.LoadScene ("Play3");
	}

	public void Estoria(){
		SceneManager.LoadScene ("Estoria");
	}

	public void play4(){

		SceneManager.LoadScene ("Play4");
	}

	public void play5(){

		SceneManager.LoadScene ("Play5");
	}

	public void play6(){

		SceneManager.LoadScene ("Play6");
	}


	public void TelaInicial(){

		SceneManager.LoadScene ("TelaInicial");
	}

	public void creditos(){

		SceneManager.LoadScene ("creditos");
	}

	public void extra1(){

		SceneManager.LoadScene ("extra1");
	}

}

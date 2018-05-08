using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Rigidbody2D 	playerRb;
	private Animator 		animatorPlayer;
	public	float 			velocidade;
	public  int 			direcao;
	private SpriteRenderer	playerSR;

	public  GameObject 		preFabExplosao;

	public  float 			HpMax,HPatual;
	public	Transform 		barraVida;
	private	float 			percentualVida;

	public  GameObject[] 	armasExtras;
	private int				poewrUpColetados;

	public	Audio 			scriptAudio;

	public	_GC 			script_GC;

	public Transform 		Top, Down, Left, Rigth;

	public 	Animator 		animatorEscudo;
	public	GameObject 		preFabEscudo;
	public	float			Hpescudo, HPmaximoEscudo;
	private	bool 			escudoAtivo;

	public 	GameObject 		itemEspecial;
	public	GameObject		iconeItemEspecial;
	public  bool			icone;

	public	VirtualJoystick	joystick;
	private Touch			toque;

	private armaNave		scriptArma;

	// Use this for initialization
	void Start () {

		Top = GameObject.Find ("Top").transform; // eles estao sendo encotrados pelo find pq o player n fica na cena
		Down = GameObject.Find ("Down").transform;
		Left = GameObject.Find ("Left").transform;
		Rigth = GameObject.Find ("Rigth").transform;

		joystick = FindObjectOfType (typeof(VirtualJoystick)) as VirtualJoystick;
		script_GC = FindObjectOfType (typeof(_GC)) as _GC;
		scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
		scriptArma = FindObjectOfType (typeof(armaNave)) as armaNave;

		playerRb = GetComponent<Rigidbody2D> ();
		animatorPlayer = GetComponent<Animator> ();
		playerSR = GetComponent<SpriteRenderer> ();
		animatorEscudo.SetBool ("escudoAtivado", true);

		barraVida = GameObject.Find ("barra_Vida").transform; // para encontrar a barra via script
		barraVida.localScale = new Vector3(1,1,1); // atualiza a barra quando morre

		HPatual = HpMax;
		Hpescudo = HPmaximoEscudo;

		armasExtras [poewrUpColetados].SetActive (true);

		//inicia com o jogador invuneravel e depois chama a funcao para tornar vuneravel de novo
		this.gameObject.layer = 10;
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0.5f);
		StartCoroutine ("vuneravel");

		//deixa o icone visivel
		//iconeItemEspecial.GetComponent<SpriteRenderer> ().enabled = true;

	}

	void Update(){

		if (HPatual <= 1) {
			StartCoroutine ("pisca");
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {



		float movimentoX = joystick.Horizontal ();

		if (movimentoX < 0) { // controla a animacao
			direcao = -1;
			//movimentoX = -1; // para não tem problema de velocidade, ela sera sempre constante
		}
		else if (movimentoX == 0) {
			direcao = 0;
		}
		else if (movimentoX > 0) {
			direcao = 1;
			//movimentoX = 1;// para não tem problema de velocidade, ela sera sempre constante
		}
			


		float movimentoY = joystick.Vertical ();

		playerRb.velocity = new Vector2 (movimentoX * velocidade, movimentoY * velocidade); // adiciona a velocidade

		//MANTEM A NAVA NO LIMITE DA TELA
		if (transform.position.x < Left.position.x) {
			transform.position = new Vector3 (Left.position.x, transform.position.y, transform.position.z);
		}
		else if (transform.position.x > Rigth.position.x) {
			transform.position = new Vector3 (Rigth.position.x, transform.position.y, transform.position.z);
		}

		if (transform.position.y < Down.position.y) {
			transform.position = new Vector3 (transform.position.x, Down.position.y, transform.position.z);
		}
		else if (transform.position.y > Top.position.y) {
			transform.position = new Vector3 (transform.position.x, Top.position.y, transform.position.z);
		}



		animatorPlayer.SetInteger ("direcao", direcao); // De acordo com a posicao X (negativa ou postiva) faz a animacao
	
	}


	void OnTriggerEnter2D(Collider2D col){

		switch (col.gameObject.tag) {

		case "bomb":
			tomarDano (3);
			break;
		case "missel":
			tomarDano (2);
			break;
		case "tiroInimigo":
			tomarDano (1);
			break;
		case "tiroChefe":
			tomarDano (3);
			Destroy (col.gameObject);
			break;
		case "powerUp":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			powerUp ();
			break;
		case "arma":
			Destroy (col.gameObject);
			chanceGun (col.name);
			scriptAudio.audioPoweUp ();
			break;
		case"escudo":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			criaEscudo ();
			break;
		case"itemEspecial":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			script_GC.buttonItem.interactable = true;
			break;
		case"vidaExtra":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			script_GC.adicionaVida ();
			break;
		case"recuperaVida":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			recuperaVida ();
			break;
		case"coin":
			Destroy (col.gameObject);
			scriptAudio.audioPoweUp ();
			script_GC.pontuacao++;
			break;
		}

	}

	void chanceGun(string name){

		print (name);
		switch (name) {
		case "lua":
			scriptArma.lua ();
			break;
		case "sabre":
			scriptArma.sabre ();
			break;
		case "red":
			scriptArma.red ();
			break;
		}

	}

	void OnCollisionEnter2D(Collision2D col){

		switch (col.gameObject.tag) {
		case "naveInimiga":
			tomarDano (2);
			break;
		case "naveChefe":
			tomarDano ((int) HpMax);
			break;
		case "meteoro":
			tomarDano ((int) HpMax);
			break;
		}
	}

	void tomarDano(int dano){
			
		if (escudoAtivo == true)
		{
			Hpescudo -= dano;
			if (Hpescudo <= 0) {
				preFabEscudo.SetActive (false);
				escudoAtivo = false;
			}
		}
		else 
		{
			HPatual -= dano;
			atualizaBarraHp ();
			StartCoroutine ("pisca");
			if (HPatual <= 0) {
				explodir ();
			}
		}
	}


	void explodir(){

		Instantiate (preFabExplosao, transform.position, transform.rotation);
		script_GC.morreu ();
		Destroy (this.gameObject);
		StartCoroutine ("vuneravel");
	}


	void atualizaBarraHp(){

		if (HPatual < 0) {
			HPatual = 0;
		}
		percentualVida = HPatual / HpMax; // acho o percentual da vida
		print(percentualVida);
		Vector3 theScale = barraVida.localScale; // armeza a barra na variavel
		theScale.x = percentualVida; // atualiza a variavel apenas na posicao X
		barraVida.localScale = theScale; //atualiza a barra corretamente

	}

	void powerUp(){

		poewrUpColetados += 1; // aciona mais um power up para o array andar
		if (poewrUpColetados <= armasExtras.Length - 1) //controla para o array n estourar
		{
			armasExtras [poewrUpColetados].SetActive (true);
		}

	}

	void recuperaVida(){
		HPatual fthe+= 2;
		if (HPatual > HpMax) {
			HPatual = HpMax;
		}
		atualizaBarraHp ();
	}

	public void criaEscudo(){
		preFabEscudo.SetActive (true);
		escudoAtivo = true;
		Hpescudo = HPmaximoEscudo;
	}


	IEnumerator  vuneravel(){ //deixar o jogador vuneravel apos 3 segundos
		yield return new WaitForSeconds (2);
		this.gameObject.layer = 8;
		this.gameObject.GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 1);
	}

	IEnumerator pisca(){//para piscar a cor quando tomar dano
		playerSR.color = new Color (255, 0, 0);
		yield return new WaitForSeconds (0.2f);
		playerSR.color = new Color (1, 1, 1);
	}


}

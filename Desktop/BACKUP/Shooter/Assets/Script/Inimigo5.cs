﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo5 : MonoBehaviour {

		private Rigidbody2D 	inimigoRb;
		private Animator 		animatorInimigo;
		public	float 			velocidade;
		private  int 			direcao;

		public	Transform[]		arma;
		public	GameObject		tiroPreFab;
		public	float 			forcaTiroY, forcaTiroX;
		private		float inverteTiro; 

		public int 				movimentoX, movimentoY;
		public	float 			tempoCurva;
		public	int				sorteio, chanceCurva;
		private float 			tempTime;
		private int 			rand;

		public	int				chanceTiro;
		public	int 			tempTiro;
		private	float 			tempTimeTiro;

		public	int 			HP;
		public	GameObject 		explosaoPreFab;

		public	int 			pontosGanhos;
		public  _GC				script_GC;

		public	GameObject[] 	dropPowerup;
		public	int				chanceDrop;

		private Audio 			scriptAudio;

		public int 				acionadorChefe;

		public GameObject 		prefabMoeda;
		public GameObject 		spawnEnemyes;
		

		// Use this for initialization
		void Start () {

			script_GC = FindObjectOfType (typeof(_GC)) as _GC;
			scriptAudio = FindObjectOfType (typeof(Audio)) as Audio;
			spawnEnemyes.SetActive (false);

			inimigoRb = GetComponent<Rigidbody2D> ();
			animatorInimigo = GetComponent<Animator> ();

			movimentoY = -1;

		}

		// Update is called once per frame
		void Update () {

			tempTime += Time.deltaTime; // vai somando o tempo ate ser maior que o tempo curva

			if (tempTime >= tempoCurva) 
			{
				tempTime = 0; // zera o tempTime para comecar de novo
				rand = Random.Range (0, 100); // faz um random pra er se muda a direção 

				if (rand <= chanceCurva)
				{ // dependendo do sorteio ele muda a direção
					rand = Random.Range (0, 100); // sorte um novo radom pra ver para qual direcao ira
					if (rand < 50) {
						movimentoX = 1;
						direcao = -1;
					} 
					else {
						movimentoX = -1;
						direcao = 1;
					}
				}
				else // se não entrar na mudança ele continua reto
				{
					movimentoX = 0;
					direcao = 0;
				}
			}

			tempTimeTiro += Time.deltaTime; //Para fazer o tiro se aleatório

			if (tempTimeTiro >= tempTiro) 
			{
				tempTimeTiro = 0;
				rand = Random.Range (0, 100);
				if (chanceTiro >= rand) 
				{
					atirar ();
				}
			}

		if (inimigoRb.transform.position.y > 1.8f) {
			inimigoRb.velocity = new Vector2 (0, movimentoY * velocidade);
		} else {
			inimigoRb.velocity = new Vector2 (0, 0);
		}
			
			

		}


		void atirar(){

		for(int i = 0; i < 2; i++){
			GameObject tempPrefab = Instantiate (tiroPreFab, arma[i].position, tiroPreFab.transform.rotation) as GameObject;
			tempPrefab.GetComponent<Rigidbody2D>().AddForce(new Vector2(forcaTiroX, forcaTiroY));
		}
		
		}


		void OnTriggerEnter2D(Collider2D col){

			switch (col.gameObject.tag)
			{

			case "tiro":
				tomarDano(1);
				break;
		case "especial":
			tomarDano (10);
				break;
			case "limiteDown":
				Destroy (this.gameObject);
				break;
			}

		}

		void OnCollisionEnter2D(Collision2D col){

			switch (col.gameObject.tag) {

			case "Player":
				tomarDano (1);
				break;
			case "naveChefe":
				tomarDano ((int) HP);
				break;
			}
		}

		void tomarDano(int danoTomado){
			HP -= danoTomado;

			if (HP <= 0) 
			{
				explodir ();
			}

		}

		void explodir(){
			int i=0;
			spawnEnemyes.SetActive (true);
			Destroy (this.gameObject);

			Instantiate (explosaoPreFab, transform.position, transform.rotation);
			scriptAudio.audioExplosion ();

			script_GC.pontuacao += pontosGanhos;
			GameObject tempDrop = Instantiate (dropPowerup[i], transform.position, transform.rotation) as GameObject;
	

		}



	}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFase4 : MonoBehaviour {

	public GameObject	inimigoPrefab;
	public float 		tempoSpawn,tempoFase;
	public Transform	limiteEsquerda, limiteDireita;
	public int 			i;
	public	GameObject  tempPrefab, Chefe4;

	private float 	minX, maxX, posX;



	private float		tempTime;

	// Use this for initialization
	void Start () {

		minX = limiteEsquerda.position.x;
		maxX = limiteDireita.position.x;

	}

	// Update is called once per frame
	void Update () {

		tempTime += Time.deltaTime;

		if (tempTime >= tempoSpawn) 
		{
			tempTime = 0;
			i = Random.Range (0, 3);
			Spawn();
		}
			

	
	}




	void Spawn(){


			posX = Random.Range (minX, maxX); //sprteia entre o lado esquerdo ao direito aonde saira o inimigo
			
				tempPrefab = Instantiate (inimigoPrefab) as GameObject;
				tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
				posX += 0.5f;
			



	}





}

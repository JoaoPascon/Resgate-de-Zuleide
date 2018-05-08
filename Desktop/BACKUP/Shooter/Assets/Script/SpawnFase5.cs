using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFase5 : MonoBehaviour {

	public GameObject[]	inimigoPrefab;
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
			i = 0;
			Spawn();
		}

		tempoFase += Time.deltaTime;

		if (tempoFase > 100) {
			StartCoroutine ("chamaChefe4");
		}

	}




	void Spawn(){


			posX = Random.Range (minX, maxX); //sprteia entre o lado esquerdo ao direito aonde saira o inimigo
//			for (int a = 0; a < 4; a++) {
				tempPrefab = Instantiate (inimigoPrefab [i]) as GameObject;
				tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
				//posX += 0.5f;
			//}

	}


	IEnumerator inimigo5(){
		yield return new WaitForSeconds(5);
		tempPrefab = Instantiate (inimigoPrefab[i]) as GameObject;
		tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}

	IEnumerator chamaChefe4(){
		yield return new WaitForSeconds(2);
		Chefe4.SetActive (true);

	}


}

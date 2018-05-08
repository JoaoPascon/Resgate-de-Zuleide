using UnityEngine;
using System.Collections;

public class SpawnInimigo : MonoBehaviour {

	public GameObject[]	onlyEnemy;
	public GameObject[] muchEnemy;
	public float 		tempoSpawn, tempoSpawn2, posXFixed;
	public Transform	limiteEsquerda, limiteDireita;
	public int 			i;
	public	GameObject  tempPrefab;

	private float 		minX, maxX, posX;

	private float		tempTime, tempTime2;

	// Use this for initialization
	void Start () {

		minX = limiteEsquerda.position.x;
		maxX = limiteDireita.position.x;
	
	}
	
	// Update is called once per frame
	void Update () {

		tempTime += Time.deltaTime;
		tempTime2 += Time.deltaTime;

		if (tempTime >= tempoSpawn) 
		{
			tempTime = 0;
			i = Random.Range (0, 2);
			spawnOnlyEnemeys();
		}


		if (tempTime2 >= tempoSpawn2) 
		{
			tempTime2 = 0;
			i = Random.Range (0, 2);
			spwanMuchEnemeys ();
		}



	}




	void spawnOnlyEnemeys(){

		GameObject tempPrefab = Instantiate (onlyEnemy[i]) as GameObject;
		float posX = Random.Range (minX, maxX); //sprteia entre o lado esquerdo ao direito aonde saira o inimigo
		tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);

	}

	void spwanMuchEnemeys(){


			for (int a = 0; a < 8; a++) {
				tempPrefab = Instantiate (muchEnemy [i]) as GameObject;
				tempPrefab.transform.position = new Vector3 (posXFixed, transform.position.y, transform.position.z);
				posXFixed += 0.5f;
			}
			posXFixed = -1;



	}

	IEnumerator inimigo5(){
		yield return new WaitForSeconds(5);
		tempPrefab = Instantiate (muchEnemy[i]) as GameObject;
		tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
	}
}

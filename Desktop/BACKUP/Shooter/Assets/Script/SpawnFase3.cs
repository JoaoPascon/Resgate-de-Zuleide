using UnityEngine;
using System.Collections;

public class SpawnFase3 : MonoBehaviour {

	public GameObject[]	inimigoPrefab;
	public float 		tempoSpawn;
	public Transform	limiteEsquerda, limiteDireita;
	public int 			i;

	private float 		minX;
	private float		maxX;

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
			i = Random.Range (0, 2);
			Spawn();
		}
	}




	void Spawn(){

		GameObject tempPrefab = Instantiate (inimigoPrefab[i]) as GameObject;
		float posX = Random.Range (minX, maxX); //sprteia entre o lado esquerdo ao direito aonde saira o inimigo
		tempPrefab.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

	}


}

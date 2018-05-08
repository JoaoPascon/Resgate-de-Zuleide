using UnityEngine;
using System.Collections;

public class SpawnMeteoros: MonoBehaviour {

	public GameObject[]	inimigoPrefab;
	public float 		tempoSpawn;
	public Transform	limiteEsquerda, limiteDireita;
	public int 			i;

	private float 		minX;
	private float		maxX;

	private float		tempTime;

	public	float 		contadorMeteoro, instatiateChefe;

	public	GameObject	Chefao2;

	public meteoros 	scriptMeteoro;

	public	GameObject	saturn;
	private	float saturnX, saturnY;
	public float		tempFase=0, tempFase2=0, tempController;
	private bool 		controlleTemp=true;
	// Use this for initialization
	void Start () {

		scriptMeteoro = FindObjectOfType(typeof (meteoros)) as meteoros;

		minX = limiteEsquerda.position.x;
		maxX = limiteDireita.position.x;

	}

	// Update is called once per frame
	void Update () {

		tempController += Time.deltaTime;
		saturnX += 0.0005f; saturnY += 0.0005f;
		if (saturnX <= 3 && saturnY <= 3) {saturn.transform.localScale = new Vector3 (saturnX, saturnY, 0);}
		if (transform.position.y >= -4.50f) {saturn.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -0.05f);}

		if (tempFase < tempController && controlleTemp) {
			controlleTemp = false;
			tempoSpawn = 1;
		}

		if (tempFase2 < tempController) {StartCoroutine ("chamaChefe");}
			


		tempTime += Time.deltaTime;

		if (tempTime >= tempoSpawn && !(tempFase2 < tempController)) 
		{
			tempTime = 0;
			i = Random.Range (0, 3);
			if (i == 2)
			{
				i = Random.Range (0, 3);
			}
			Spawn();
		}
	}
		

	void Spawn(){

		GameObject tempPrefab = Instantiate (inimigoPrefab[i]) as GameObject;
		float posX = Random.Range (minX, maxX); //sprteia entre o lado esquerdo ao direito aonde saira o inimigo
		tempPrefab.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);

	}

	IEnumerator chamaChefe(){
		yield return new WaitForSeconds (7);
		Chefao2.SetActive (true);
	}
		

}

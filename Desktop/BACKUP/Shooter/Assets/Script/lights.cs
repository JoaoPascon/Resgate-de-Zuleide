using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour {

	private controllLight 	  controllLight;
	public  GameObject[] 	  spawnEnemys;
	private GameObject 		  spawn;
	public 	GameObject 		  boss;
			
	// Use this for initialization
	void Start () {
		controllLight = FindObjectOfType (typeof(controllLight)) as controllLight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void luzAmarela(){
		spawn = spawnEnemys [0];
		StartCoroutine ("controllSpawn");

	}

	public void luzVermelha(){
		spawn = spawnEnemys [1];
		StartCoroutine ("controllSpawn");

	}

	public void luzVerde(){
		spawn = spawnEnemys [2];
		StartCoroutine ("controllSpawn");

	}

	public void luzAzul(){
		boss.SetActive (true);
		controllLight.disableLigths ();
		controllLight.disableBossLigth ();
	}
		

	IEnumerator controllSpawn(){
		controllLight.disableLigths ();
		spawn.SetActive (true);
		yield return new WaitForSeconds (3f);
		controllLight.enableLights ();
		spawn.SetActive (false);

	}

}

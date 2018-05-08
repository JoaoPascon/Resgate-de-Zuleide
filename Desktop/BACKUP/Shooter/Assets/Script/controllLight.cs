using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllLight : MonoBehaviour {

	public GameObject[] ligths;
	public GameObject ligthBoss;
	public float timeLights, timeBoss;
	private bool	bossActive=false;

	void Start () {
		disableLigths ();
	}
	
	// Update is called once per frame
	void Update () {

		timeLights += Time.deltaTime;
		timeBoss += Time.deltaTime;;

		if(!bossActive){
			ligths [0].SetActive (true);
			ligths [3].SetActive (true);
			ligths [4].SetActive (true);
		if (timeLights > 20) 
			ligths [2].SetActive (true);
		if (timeLights > 10) {
			ligths [1].SetActive (true);
		}
		if (timeBoss > 180)
			ligthBoss.SetActive (true);
		}
	}

	public void disableLigths(){
		
		foreach (var item in ligths) {
			item.SetActive (false);
		}
			
	}

	public void enableLights(){
		
		foreach (var item in ligths) {
			item.SetActive (true);
		}

	}

	public void disableBossLigth(){
		ligthBoss.SetActive (false);
		bossActive = true;
	}
}

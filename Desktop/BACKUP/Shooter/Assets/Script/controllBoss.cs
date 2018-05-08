using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllBoss : MonoBehaviour {

	public GameObject[] boss;
	public float 		timeBoss, time;
	private bool		isActiveBoss=true;
	private int			i=0;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if (isActiveBoss && (timeBoss <= time)) {
			isActiveBoss = false;
			boss [i].SetActive (true);
			i++;
		}
			
	}
		
	public void callBoss(){
		time = 0;
		isActiveBoss = true;
	}
}

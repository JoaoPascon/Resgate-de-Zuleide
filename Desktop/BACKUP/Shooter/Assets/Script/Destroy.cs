using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	private controllBoss scriptControllBoss;
	void Start () {

		scriptControllBoss = FindObjectOfType (typeof(controllBoss)) as controllBoss;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		scriptControllBoss.callBoss ();
	}
}

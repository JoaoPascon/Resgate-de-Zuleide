using UnityEngine;
using System.Collections;

public class moeda : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("destroiMoeda");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator destroiMoeda(){
		yield return new WaitForSeconds (5);
		Destroy (this.gameObject);
	}
}

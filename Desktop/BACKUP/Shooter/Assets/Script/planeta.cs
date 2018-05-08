using UnityEngine;
using System.Collections;

public class planeta : MonoBehaviour {


	private Transform planet;
	public  float rotate;

	// Use this for initialization
	void Start () {
	
		planet = GetComponent<Transform> ();

	}
	
	// Update is called once per frame
	void Update () {

		planet.Rotate (0, 0, rotate);

	}


//
}

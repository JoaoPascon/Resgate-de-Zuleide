using UnityEngine;
using System.Collections;

public class OnTimer : MonoBehaviour {

	public	float	tempoVida; //tempo que a explosao demora na tela
	private float	tempTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		tempTime += Time.deltaTime;

		if (tempTime >= tempoVida) //controla esse tempo de explocao
		{
			Destroy (this.gameObject);
		}
	
	}
}

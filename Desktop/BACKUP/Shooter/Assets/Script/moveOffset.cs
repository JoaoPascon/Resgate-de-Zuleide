using UnityEngine;
using System.Collections;

public class moveOffset : MonoBehaviour {

	private Material	currentMaterial;
	private float		offSet;
	public	float 		speed;

	// Use this for initialization
	void Start () {
	
		currentMaterial = GetComponent<Renderer> ().material;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		offSet += speed * 0.001f;

		currentMaterial.SetTextureOffset ("_MainTex", new Vector2 (0, offSet));
	
	}
}

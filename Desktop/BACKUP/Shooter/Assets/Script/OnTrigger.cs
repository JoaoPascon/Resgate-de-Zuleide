using UnityEngine;
using System.Collections;

public class OnTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col){

		if (!col.isTrigger) // só vai destruir o tiro se  objeto que el tocar não for um trigger
							// exemplos pode ser itens que estarão na tela...
		{
			Destroy (this.gameObject);
		}

		switch (col.gameObject.tag) {

		case "limiteDown":
			Destroy (this.gameObject);
			print ("DESTRUIDO");
			break;
		case "limiteTop":
			Destroy (this.gameObject);
			print ("DESTRUIDO");
			break;
		case "limiteLeft":
			Destroy (this.gameObject);
			print ("DESTRUIDO");
			break;
		case "limiteRight":
			Destroy (this.gameObject);
			print ("DESTRUIDO");
			break;
		case "especial":
			Destroy (this.gameObject);
			break;
		}
	}
}

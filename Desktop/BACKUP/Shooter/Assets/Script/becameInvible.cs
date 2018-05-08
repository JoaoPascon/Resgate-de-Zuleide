using UnityEngine;
using System.Collections;

public class becameInvible : MonoBehaviour {

	void OnBecameInvisible(){
		Destroy (this.gameObject);
	}
}

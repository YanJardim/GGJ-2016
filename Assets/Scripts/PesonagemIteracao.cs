using UnityEngine;
using System.Collections;

public class PesonagemIteracao : MonoBehaviour {
	public GameObject item = null;
	public int k;
	void Update(){
		k = this.GetComponentsInChildren<Transform> ().Length;
		if (Input.GetKeyDown ("space")){
			if (item != null && this.GetComponentsInChildren<Transform> ().Length == 2) {
				item.transform.parent = this.transform;

				//Send para desabilitar objeto
			} else if (this.GetComponentsInChildren<Transform> ().Length > 2) {
				item.transform.parent = null;

				//Send para habilitar objeto
			}
		} 
	}

	void OnTriggerEnter(Collider other){
		item = other.gameObject;

		print ("Entrou");
	}

	void OnTriggerExit(Collider other){
		item = null;

		print ("Saiu");
	}
}

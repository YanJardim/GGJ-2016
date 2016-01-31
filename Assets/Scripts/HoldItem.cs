using UnityEngine;
using System.Collections;

public class HoldItem : MonoBehaviour {
    public GameObject item = null;
    public GameObject itemSegurado = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            if (item != null && this.GetComponentsInChildren<Transform>().Length == 3)
            {
                item.transform.parent = this.transform;
                itemSegurado = item;
                print("ASD");
                //item.GetComponent<SpriteRenderer>().enabled = false;

                GameObject.Find("Socket").SendMessage("SendObject", item);
            }
            else if (this.GetComponentsInChildren<Transform>().Length > 3)
            {
                itemSegurado.transform.parent = null;
                itemSegurado = null;
                GameObject.Find("Socket").SendMessage("SendObject", item);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {

        item = other.gameObject;

        print("Entrou");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        item = null;

        print("Saiu");
    }

}

using UnityEngine;
using System.Collections;

public class HoldItem : MonoBehaviour {
    public GameObject item = null;
    public GameObject itemSegurado = null;
    public Transform posicao;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            if (item != null && this.GetComponentsInChildren<Transform>().Length == 4)
            {
                item.transform.parent = this.transform;
                itemSegurado = item;
                itemSegurado.transform.position = posicao.position;
                itemSegurado.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;

                
                print("ASD");
                //item.GetComponent<SpriteRenderer>().enabled = false;

                GameObject.Find("Socket").SendMessage("SendObject", item);
            }
            else if (this.GetComponentsInChildren<Transform>().Length > 4)
            {
                itemSegurado.transform.parent = null;

                Vector3 temp = itemSegurado.transform.position;
                temp.z = 0;
                itemSegurado.transform.position = temp;

                itemSegurado.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;
                GameObject.Find("Socket").SendMessage("SendObject", item);
                itemSegurado = null;
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

using UnityEngine;
using System.Collections;

public class HoldItem : MonoBehaviour {
    public GameObject item = null;
    public GameObject itemSegurado = null;
    public Vector2 indice = new Vector2(0, 0);
    public Transform posicao;
    public blocks last;
    


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        indice.x = Mathf.Floor(transform.position.x + 16.5f);
        indice.y = Mathf.Floor(transform.position.y + 16.5f);
        last = GameObject.Find("Grid").GetComponent<GridBehaviour>().getGridItem((int)indice.x, (int)indice.y);
        if (Input.GetKeyDown("space"))
        {
            if (item != null && item.GetComponent<Item>().canMove && this.GetComponentsInChildren<Transform>().Length == 4)
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
                itemSegurado.transform.parent = GameObject.Find("GameManager").transform;

                Vector3 temp = itemSegurado.transform.position;
                temp.x = Mathf.Floor(transform.position.x + .5f) - .5f;
                temp.y = Mathf.Floor(transform.position.y + .5f) + .5f;
                temp.z = 0;
                itemSegurado.transform.position = temp;
                itemSegurado.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;

                itemSegurado = null;

                // last = GameObject.Find("Grid").GetComponent<GridBehaviour>().getGridItem((int)indice.x, (int)indice.y);
                bool b = last.CompareTo(blocks.ALTAR) == 0;
                
                if (b)
                {
                    item.SendMessage("cant");
                    Invoke("liberar", 30);
                }
                GameObject.Find("Socket").SendMessage("SendObject", item);
            }
        }
	}

    void liberar()
    {
        item.SendMessage("can");
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

using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
    public GameObject item = null;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (item != null && this.GetComponentsInChildren<Transform>().Length == 2)
            {
                item.transform.parent = this.transform;
                //item.GetComponent<SpriteRenderer>().enabled = false;

                //Send para desabilitar objeto
            }
            else if (this.GetComponentsInChildren<Transform>().Length > 2)
            {
                item.transform.parent = null;

                //Send para habilitar objeto
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
  
        item = other.gameObject;

        print("Entrou");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        item = null;

        print("Saiu");
    }
}

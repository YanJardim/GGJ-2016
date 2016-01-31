using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    [Range(0, 20)]
    public int walkSpeed;
    [Range(0, 10)]
    public int runSpeed;

    private int currentSpeed;
    
    public GameObject item = null;

	// Use this for initialization
    void Start()
    {

        currentSpeed = walkSpeed;
      
	}
	
	// Update is called once per frame
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

        if (Input.GetAxis("Run") != 0)
        {
            currentSpeed = runSpeed;
           
        }

        else
        {
            currentSpeed = walkSpeed;
                               
        }
              
    }

    void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		transform.Translate((h/2 - v/2) * currentSpeed * Time.deltaTime, (h/2 + v/2) * currentSpeed * Time.deltaTime, 0);
		GetComponent<Rigidbody2D>().velocity = new Vector2((h/2 - v/2) * currentSpeed, (h/2 + v/2) * currentSpeed);
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

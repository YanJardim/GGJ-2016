using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private int walkSpeed;
    private int runSpeed;

    private int currentSpeed;
    
    

	// Use this for initialization
    void Start()
    {
        walkSpeed = GameManager.instance.playerSpeed;
        runSpeed = GameManager.instance.playerRunSpeed;

        currentSpeed = walkSpeed;
      
	}
	
	// Update is called once per frame
    void Update()
    {

        

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

    

}

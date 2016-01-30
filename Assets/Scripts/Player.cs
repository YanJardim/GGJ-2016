using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    [Range(0, 20)]
    public int walkSpeed;
    [Range(0, 20)]
    public int runSpeed;

    private int currentSpeed;

    // Use this for initialization
    void Start()
    {

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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(h * currentSpeed * Time.deltaTime, v * currentSpeed * Time.deltaTime, 0);

        GetComponent<Rigidbody2D>().velocity = new Vector2(h * currentSpeed, v * currentSpeed);
    }




}

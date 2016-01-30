using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    [Range(0, 20)]
    public int walkSpeed;
    [Range(0, 20)]
    public int runSpeed;

    private int currentSpeed;

    public float stamina;
    public int staminaCost;

    private bool canRun;

    public float restTime;
    private float restTimer;
    // Use this for initialization
    void Start()
    {

        currentSpeed = walkSpeed;
        stamina = 100;
        restTimer = 0;

        if (stamina >= 0)
        {
            canRun = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(h * currentSpeed * Time.deltaTime, v * currentSpeed * Time.deltaTime, 0);

        if (Input.GetAxis("Run") != 0 && canRun)
        {
            currentSpeed = runSpeed;
            if (h != 0 || v != 0)
            {
                stamina -= Time.deltaTime * staminaCost;
            }

            if (stamina <= 0)
            {
                canRun = false;
            }
            
        }

        else
        {
            currentSpeed = walkSpeed;

            if(stamina < 100)
                stamina += Time.deltaTime * staminaCost;

            
        }

        if (canRun == false)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= restTime)
            {
                canRun = true;
            }
        }

        if (stamina < 100 && h == 0 && v == 0)
            stamina += Time.deltaTime * staminaCost;      
        
    }

}

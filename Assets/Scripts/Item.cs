using UnityEngine;
using System.Collections;

public enum relics
{
    CANDLE,
    BELL,
    SCROLL,
    HORN,
    SKULL,
    CUP
};

public class Item : MonoBehaviour
{

    public bool canMove = true;

    public relics relicType;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void can()
    {
        canMove = true;
    }

    void cant()
    {
        canMove = false;
    }




}

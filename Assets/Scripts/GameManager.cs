using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public List<GameObject> relicObjects = new List<GameObject>();

    [Range(0, 20)]
    public int playerSpeed;
    [Range(0, 20)]
    public int playerRunSpeed;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class GUI_Cliserv : MonoBehaviour {
    public GameObject server, client;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if(GUI.Button(new Rect(50,50,400,50), "Server!"))
        {
            Instantiate(server, transform.position, transform.rotation);
        }
        else if (GUI.Button(new Rect(50, 120, 400, 50), "Cliente!"))
        {
            Instantiate(client, transform.position, transform.rotation);
        }
    }
}

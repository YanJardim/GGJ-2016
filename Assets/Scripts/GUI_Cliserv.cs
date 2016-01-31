using UnityEngine;
using System.Collections;
using System.Net;

public class GUI_Cliserv : MonoBehaviour {
    public GameObject server, client;
    private bool mostraSelecao;

	// Use this for initialization
	void Start () {
        mostraSelecao = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (mostraSelecao)
        {
            if (GUI.Button(new Rect(50, 50, 400, 50), "Server!"))
            {
                Instantiate(server, transform.position, transform.rotation);
                mostraSelecao = false;  
            }
            else if (GUI.Button(new Rect(50, 120, 400, 50), "Cliente!"))
            {
                Instantiate(client, transform.position, transform.rotation);
            }
        }
        else
        {
            GUI.Label(new Rect(50, 50, 400, 50), GetLocalIP());
        }
    }

    string GetLocalIP()
    {
        IPHostEntry host;
        string localIP = "?";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                localIP = ip.ToString();
            }
        }
        return localIP;
    }
}

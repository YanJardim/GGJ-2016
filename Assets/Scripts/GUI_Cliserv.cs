using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.SceneManagement;

public class GUI_Cliserv : MonoBehaviour {
    private string ipServer = "";
    public GameObject server, client;
    private int selecao;

	// Use this for initialization
	void Start () {
        selecao = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (selecao == 0)
        {
            if (GUI.Button(new Rect(50, 50, 400, 50), "Server!"))
            {
                GameObject go = Instantiate(server, transform.position, transform.rotation) as GameObject;
                go.name = "Socket";
                
                selecao = 1;  
            }
            else if (GUI.Button(new Rect(50, 120, 400, 50), "Cliente!"))
            {
                selecao = 2;
            }
        }
        else if (selecao == 1 && !server.GetComponent<Server>().isConnected())
        {
            GUI.Label(new Rect(50, 50, 400, 50), GetLocalIP());
        }
        else if (selecao == 2)
        {
            ipServer = GUI.TextField(new Rect(50, 50, 150, 25), ipServer);
            if (GUI.Button(new Rect(50, 120, 400, 50), "Conectar!!"))
            {
                client = Instantiate(client, transform.position, transform.rotation) as GameObject;
                client.name = "Socket";
                client.SendMessage("Conectar", ipServer);
                selecao = -1;
            }
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

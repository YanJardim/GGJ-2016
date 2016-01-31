﻿using UnityEngine;
using System.Net.Sockets;
using System.Threading;

public class Client : MonoBehaviour {
    Thread thread = null;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;
    TcpClient client;

    public GameObject[] sincronizar;

    // Use this for initialization
    void Start () {
        // Get a client stream for reading and writing.
        //  Stream stream = client.GetStream();
        Connect("127.0.0.1");

        stream = client.GetStream();
        sw = new System.IO.StreamWriter(stream);
        sr = new System.IO.StreamReader(stream);

        thread = new Thread(LoopMensagem);
        thread.Start();
	}

	void LoopMensagem () {
        while (client.Connected)
        {
            if (stream.DataAvailable)
            {
                Debug.Log(sr.ReadLine());
            }
        }
    }

    void Send(string message)
    {
        sw.WriteLine(message);
        sw.Flush();
    }

    void Flush()
    {
        for(int i = 0; i < sincronizar.Length; i++)
        {
            string s = sincronizar[i].name + ";" + transform.position.x + ";" + transform.position.y + ";" + transform.position.z;
            Debug.Log(s);
            // Send(sincronizar[i].name + ";" + transform.position.x + ";" + transform.position.y + ";" + transform.position.z);
        }
    }

    void Connect(string server)
    {
        try
        {
            int port = 5050;
            client = new TcpClient(server, port);

            Debug.Log("Conectou!");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void OnApplicationQuit()
    {
        stream.Close();
        client.Close();
        thread.Abort();
    }

    
}

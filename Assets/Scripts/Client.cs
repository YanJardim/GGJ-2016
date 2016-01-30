using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour {
    bool connected = false;
    TcpClient client;
    NetworkStream stream;
    System.IO.StreamWriter sw;
    System.IO.StreamReader sr;

    // Use this for initialization
    void Start () {
        Connect("127.0.0.1");
	}
	
	// Update is called once per frame
	void Update () {
        if(connected && stream.DataAvailable)
        {

            System.Byte[] bytes = new System.Byte[256];
            
            Debug.Log(sr.ReadLine());

        }

    }

    void Send(string message)
    {
        sw.WriteLine(message);
        sw.Flush();
    }

    byte[] toByte(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    void Connect(string server)
    {
        try
        {
            int port = 5050;
            client = new TcpClient(server, port);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            stream = client.GetStream();
            sw = new System.IO.StreamWriter(stream);
            sr = new System.IO.StreamReader(stream);
            Debug.Log("Conectou!");
            connected = true;
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
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(100, 300, 500,500), "Mensagem"))
        {
            Send("Hello World!");
        }
    }
}

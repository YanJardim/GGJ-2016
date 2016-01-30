using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;

public class Client : MonoBehaviour {
    bool connected = false;
    TcpClient client;
    NetworkStream stream;
    System.IO.StreamWriter sw;

    // Use this for initialization
    void Start () {
        Connect("127.0.0.1");
	}
	
	// Update is called once per frame
	void Update () {
        if(false && connected && stream.DataAvailable)
        {
            int i;
            Byte[] bytes = new Byte[256];
            string data; 

            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Debug.Log("Received: "+ data);

                // Process the data sent by the client.
                data = data.ToUpper();
                
                Debug.Log("Sent: " + data);
            }
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

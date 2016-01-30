using UnityEngine;
using System.Net.Sockets;

public class Server : MonoBehaviour {
    bool connected = false;
    TcpClient client;
    TcpListener server;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;

    string mensagem = "Sem mensagem";

    public int port;

    byte[] toByte(string str)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    // Use this for initialization
    void Start () {
        server = new TcpListener(System.Net.IPAddress.Loopback, port);
        server.Start();

        // Get a client stream for reading and writing.
        //  Stream stream = client.GetStream();
        
    }


    void Send(string message)
    {
        sw.WriteLine(message);
        sw.Flush();
    }

    // Update is called once per frame
    void Update()
    {
        if(!connected)
        {
            if (!server.Pending()) return;
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            sr = new System.IO.StreamReader(stream);
            sw = new System.IO.StreamWriter(stream);
            
            mensagem = "Conectado!!";
            connected = true;
        }

        if (connected)
        {
            
            System.Byte[] bytes = new System.Byte[256];

            mensagem = "Bytes";
            mensagem = sr.ReadLine();
            Send("FOIFOI"); 
        }

    }

    void OnApplicationQuit()
    {
        // Close everything.
        if(connected)
        {
            stream.Close();
            client.Close();
        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 1000, 1000), mensagem);
    }
}

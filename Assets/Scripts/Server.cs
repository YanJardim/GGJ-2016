using UnityEngine;
using System.Net.Sockets;

public class Server : MonoBehaviour {
    bool connected = false;
    TcpClient client;
    TcpListener server;
    NetworkStream stream;
    System.IO.StreamReader sr;

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
        stream.Write(toByte(message), 0, message.Length);
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
            mensagem = "Conectado!!";
            connected = true;
        }

        if (connected)
        {
            // if (!stream.DataAvailable) return;
            
            int i;
            System.Byte[] bytes = new System.Byte[256];
            string data;

            mensagem = "Bytes";

            mensagem = sr.ReadLine();

            /*mensagem = "Bytes";

            while (bytesCount != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesCount);
               
                // Process the data sent by the client.
                mensagem = data;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                
                Debug.Log("Sent: " + data);
                bytesCount = stream.Read(bytes, 0, bytes.Length);
            }*/
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

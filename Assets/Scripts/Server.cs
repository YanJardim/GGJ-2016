using UnityEngine;
using System.Net.Sockets;
using System.Threading;

public class Server : MonoBehaviour {
    TcpListener server;
    TcpClient client;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;
    Thread thread;

    bool parar = false;
    string mensagem = "Sem mensagem";

    int port = 5050;

    // Use this for initialization
    void Start () {
        server = new TcpListener(System.Net.IPAddress.Loopback, port);
        server.Start();

        thread = new Thread(LoopMensagem);
        thread.Start();
    }


    void Send(string message)
    {
        mensagem = "ENTROU AQUI";
        sw.WriteLine(message);
        sw.Flush();
    }

    void LoopMensagem()
    {
        while (!server.Pending())
        {
            mensagem = "Loop espera Pending";
            Thread.Sleep(10);
        }
        client = server.AcceptTcpClient();
        stream = client.GetStream();
        sr = new System.IO.StreamReader(stream);
        sw = new System.IO.StreamWriter(stream);
        mensagem = "Conectado!!";

        while (!parar)
        {
            mensagem = sr.ReadLine();
            Send("FOIFOI");
        }
    }

    void OnApplicationQuit()
    {
        // Close everything.
        try
        {
            stream.Close();
            client.Close();
            parar = true; // Encerra o loop da thread, que se encerra em seguida
        }
        catch (System.Exception e)
        {
            mensagem = e.Message;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 1000, 1000), mensagem);
    }
}

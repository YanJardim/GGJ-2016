using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
    TcpListener server;
    TcpClient client;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;
    Thread thread;

    bool mudar = false;

    bool parar = false, carregar = false;

    int port = 5050;

    // Use this for initialization
    void Start()
    {
        server = new TcpListener(IPAddress.Parse(GetLocalIP()), port);
        server.Start();

        thread = new Thread(LoopMensagem);
        thread.Start();
    }

    void Update()
    {
        if (mudar)
        {
            string[] data = mensagem.Split(new char[] { ';' }); ;
            GameObject objeto = GameObject.Find(data[0]);

            Vector3 trocar = new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
            if (objeto.GetComponent<Renderer>().enabled)
            {
                objeto.transform.Translate(new Vector3(1000, 1000, 1000));
                objeto.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                objeto.transform.position = trocar;
                objeto.GetComponent<Renderer>().enabled = true;
            }
            mudar = false;
        }
        if(carregar)
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
            carregar = false;
        }
    }

    public bool isConnected()
    {
        return (client != null);
    }

    void Send(string message)
    {
        sw.WriteLine(message);
        sw.Flush();
    }

    void SendObject(object obj)
    {
        GameObject item = obj as GameObject;
        string s = item.name + ";" + item.transform.position.x + ";" + item.transform.position.y + ";" + item.transform.position.z;
        Debug.Log(s);
        Send(s);
    }

    void LoopMensagem()
    {
        while (!server.Pending())
        {
            Thread.Sleep(10);
        }
        client = server.AcceptTcpClient();
        stream = client.GetStream();
        sr = new System.IO.StreamReader(stream);
        sw = new System.IO.StreamWriter(stream);
        carregar = true;

        while (!parar)
        {
            string tmp = sr.ReadLine();

            if (tmp != null)
            {
                mudar = true;
                mensagem = tmp;
            }
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
        GUI.Label(new Rect(100, 500, 1000, 1000), mensagem);
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

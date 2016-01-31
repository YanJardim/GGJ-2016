﻿using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;

public class Server : MonoBehaviour
{
    TcpListener server;
    TcpClient client;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;
    Thread thread;

    bool mudar = false;

    bool parar = false;
    string mensagem = "Sem mensagem";

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
            objeto.transform.position = trocar;
            mudar = false;
        }
    }

    void Send(string message)
    {
        mensagem = "ENTROU AQUI";
        sw.WriteLine(message);
        sw.Flush();
    }

    void LoopMensagem()
    {
        int i = 0;
        while (!server.Pending())
        {
            mensagem = "Loop espera Pending" + i;
            i++;
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

            mudar = true;
            Debug.Log(mensagem);
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

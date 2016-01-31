using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour {
    Thread thread = null;
    NetworkStream stream;
    System.IO.StreamReader sr;
    System.IO.StreamWriter sw;
    TcpClient client;
    

    public void Conectar(object ipServer) {
        // Get a client stream for reading and writing.
        //  Stream stream = client.GetStream();

        Connect(ipServer.ToString());

        thread = new Thread(LoopMensagem);
        thread.Start();
	}
    

	void LoopMensagem () {
        while (client.Connected)
        {
            if (stream.DataAvailable)
            {
                string[] data = sr.ReadLine().Split(new char[] { ';' }); ;
                GameObject objeto = GameObject.Find(data[0]);

                Vector3 trocar = new Vector3(System.Int32.Parse(data[1]), System.Int32.Parse(data[2]), System.Int32.Parse(data[3]));
                objeto.transform.position = trocar;

            }
        }
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

    void Connect(string server)
    {
        try
        {
            int port = 5050;
            client = new TcpClient(server, port);
            stream = client.GetStream();
            sw = new System.IO.StreamWriter(stream);
            sr = new System.IO.StreamReader(stream);
            Debug.Log("Conectou!");
        }
        catch (System.Exception e)
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
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

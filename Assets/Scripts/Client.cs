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
    string message;
    bool mudar = false;
    

    void Update()
    {
        if(mudar)
        {
            string[] data = message.Split(new char[] { ';' }); ;
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
    }

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
                string temp = sr.ReadLine();
                if (temp != null)
                {
                    message = temp;
                    mudar = true;
                }

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
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
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

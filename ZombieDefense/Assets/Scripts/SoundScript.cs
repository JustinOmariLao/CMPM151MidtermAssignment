using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityOSC;

public class SoundScript : MonoBehaviour
{

    Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
    // Start is called before the first frame update
    float zombieTime = 1000.0f;
    void Start()
    {
        Application.runInBackground = true;

        OSCHandler.Instance.Init();
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", "ready");
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", 0);

    }

    // Update is called once per frame
    void Update()
    {
        zombieTime -= Time.deltaTime;
        if (zombieTime <= 0)
        {
            OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", 6);
            zombieTime = 1000.0f;
        }
    }
    void fixedUpdate()
    {
        OSCHandler.Instance.UpdateLogs();
        Dictionary<string, ServerLog> servers = new Dictionary<string, ServerLog>();
        servers = OSCHandler.Instance.Servers;
    }

    public void sendCommand(int num)
    {

        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", num);


    }  
    public void OnApplicationQuit()
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", 0);
    }

    public void zombieSound()
    {
        OSCHandler.Instance.SendMessageToClient("pd", "/unity/trigger", 6);
        zombieTime = 0.5f;
    }

}

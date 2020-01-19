using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UDPReceiver : MonoBehaviour
{
    UDPSocket s;

    [SerializeField]
    private int port = 27002;

    public MessageEvent OnNewMessage = new MessageEvent();

    void Start()
    {
        s = new UDPSocket();
        s.Server("127.0.0.1", port);

        s.OnNewMessage = new MessageEvent();
        s.OnNewMessage.AddListener(NewMessage);
    }

    public void NewMessage(string msg)
    {
        print("receiver: " + msg);
        OnNewMessage.Invoke(msg);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPSender : MonoBehaviour
{
    private UDPSocket c;

    int counter = 0;

    [SerializeField]
    private int port = 27002;

    void Start()
    {
        //s = new UDPSocket();
        //s.Server("127.0.0.1", 27002);

        c = new UDPSocket();
        c.Client("127.0.0.1", port);
        c.Send("initial msg");

        //s.OnNewMessage = new MessageEvent();
        //s.OnNewMessage.AddListener(NewMessage);

        //OnNewMessageEvent = new MessageEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            var msg = "counter " + counter.ToString();
            c.Send(msg);
            print("sender: " + msg);
            counter += 1;
        }
    }

    //void NewMessage(string msg)
    //{
    //    print("> s-received: " + msg);
    //    OnNewMessageEvent.Invoke(msg);
    //    NoArgumentEvent.Invoke();
    //}
}

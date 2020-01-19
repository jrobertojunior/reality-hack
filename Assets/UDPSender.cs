using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPSender : MonoBehaviour
{
    UDPSocket c;
    UDPSocket s;

    // Start is called before the first frame update
    void Start()
    {
        s = new UDPSocket();
        s.Server("127.0.0.1", 27002);

        c = new UDPSocket();
        c.Client("127.0.0.1", 27002);
        c.Send("TEST!");

        //Console.ReadKey();
    }

    // Update is called once per frame
    void Update()
    {
        print(s.msg);
    }
}

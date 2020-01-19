﻿using System;
using UnityEngine;
using UnityEngine.Networking;


public class Client : MonoBehaviour
{
    int port = 9999;
    public string ip = "localhost";

    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    // The network client
    NetworkClient client;

    public void Awake()
    {
        CreateClient();

        //> ; nome; exercicio; loc; ossos errados
    }

    private float counter = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendMessage("counter: " + counter);
            counter+= 1;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            SendMessage("> ; carlos ; levantamento-lateral ; espelho ; LeftShoulder");

        }
    }

    void CreateClient()
    {
        var config = new ConnectionConfig();

        // Config the Channels we will use
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        // Create the client ant attach the configuration
        client = new NetworkClient();
        client.Configure(config, 1);

        // Register the handlers for the different network messages
        RegisterHandlers();

        // Connect to the server
        client.Connect(ip, port);
    }

    // Register the handlers for the different message types
    void RegisterHandlers()
    {

        // Unity have different Messages types defined in MsgType
        client.RegisterHandler(messageID, OnMessageReceived);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void OnConnected(NetworkMessage message)
    {
        // Do stuff when connected to the server

        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = "Hello server!";

        // Say hi to the server when connected
        client.Send(messageID, messageContainer);
    }

    void OnDisconnected(NetworkMessage message)
    {
        // Do stuff when disconnected to the server
    }

    // Message received from the server
    void OnMessageReceived(NetworkMessage netMessage)
    {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();

        Debug.Log("Message received: " + objectMessage.message);
    }

    public void SendMessage(string msg)
    {
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = msg;

        // Say hi to the server when connected
        client.Send(messageID, messageContainer);
    }
}
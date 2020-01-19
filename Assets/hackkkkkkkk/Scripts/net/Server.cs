﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{

    int port = 9999;
    int maxConnections = 10;

    // The id we use to identify our messages and register the handler
    short messageID = 1000;

    // Use this for initialization
    void Start()
    {
        // Usually the server doesn't need to draw anything on the screen
        Application.runInBackground = true;
        CreateServer();
    }


    int counter = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Broadcast("counter: " + counter.ToString());
            counter += 1;
        }
    }

    void CreateServer()
    {
        // Register handlers for the types of messages we can receive
        RegisterHandlers();

        var config = new ConnectionConfig();
        // There are different types of channels you can use, check the official documentation
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);

        var ht = new HostTopology(config, maxConnections);

        if (!NetworkServer.Configure(ht))
        {
            Debug.Log("No server created, error on the configuration definition");
            return;
        }
        else
        {
            // Start listening on the defined port
            if (NetworkServer.Listen(port))
                Debug.Log("Server created, listening on port: " + port);
            else
                Debug.Log("No server created, could not listen to the port: " + port);
        }
    }

    void OnApplicationQuit()
    {
        NetworkServer.Shutdown();
    }

    private void RegisterHandlers()
    {
        // Unity have different Messages types defined in MsgType
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnected);

        // Our message use his own message type.
        NetworkServer.RegisterHandler(messageID, OnMessageReceived);
    }

    private void RegisterHandler(short t, NetworkMessageDelegate handler)
    {
        NetworkServer.RegisterHandler(t, handler);
    }

    void OnClientConnected(NetworkMessage netMessage)
    {
        // Do stuff when a client connects to this server

        // Send a thank you message to the client that just connected
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = "Thanks for joining!";

        // This sends a message to a specific client, using the connectionId
        NetworkServer.SendToClient(netMessage.conn.connectionId, messageID, messageContainer);

        // Send a message to all the clients connected
        messageContainer = new MyNetworkMessage();
        messageContainer.message = "A new player has conencted to the server";

        // Broadcast a message a to everyone connected
        NetworkServer.SendToAll(messageID, messageContainer);
    }

    void OnClientDisconnected(NetworkMessage netMessage)
    {
        // Do stuff when a client dissconnects
    }

    void OnMessageReceived(NetworkMessage netMessage)
    {
        // You can send any object that inherence from MessageBase
        // The client and server can be on different projects, as long as the MyNetworkMessage or the class you are using have the same implementation on both projects
        // The first thing we do is deserialize the message to our custom type
        var objectMessage = netMessage.ReadMessage<MyNetworkMessage>();
        Debug.Log("Message received: " + objectMessage.message);

    }

    public void Broadcast(string msg)
    {
        // Send a message to all the clients connected
        MyNetworkMessage messageContainer = new MyNetworkMessage();
        messageContainer.message = msg;

        // Broadcast a message a to everyone connected
        NetworkServer.SendToAll(messageID, messageContainer);
    }
}

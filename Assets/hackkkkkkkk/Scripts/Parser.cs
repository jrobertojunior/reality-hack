using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Message
{
    public string name;
    public string exercise;
    public string localization;
    public string bones;

    public Message(string name, string exercise, string localization, string bones)
    {
        this.name = name;
        this.exercise = exercise;
        this.localization = localization;
        this.bones = bones;
    }
}

public static class Parser
{
    public static Message GetMessage(string rawMsg)
    {
        string[] tokens = rawMsg.Split(';');

        Message message = new Message(tokens[1], tokens[2], tokens[3], tokens[4]);

        return message;
    }
}

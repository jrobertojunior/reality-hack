using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherNotification : MonoBehaviour
{
    public string clientName;

    public string exerciseName;

    public string location;

    public string bonesInvolved;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void NotificationArrived(string msg) {
        print(msg);
    } 

    public void PrintMessage(string msg)
    {
        print("teacher received: " + msg);
    }

    public void NoArgumentPrint()
    {
        print("teacher test");
    }
}

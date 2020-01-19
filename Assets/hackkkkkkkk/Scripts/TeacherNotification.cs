using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherNotification : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
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

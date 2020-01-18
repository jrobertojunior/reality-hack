using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voxar;

public class DataReceiver : MonoBehaviour, Voxar.IReceiver<Voxar.BodyJoints[]>
{
    public void ReceiveData(BodyJoints[] data)
    {
        //by default Sensors only tracks 1 body at a time, but you can change it in the "SensorController" script
        //if you only wish to track 1 body you can remove this foreach and replace it with:

        //var body = data[0];
        //Debug.Log("Receiving Data!");
        foreach (var body in data)
        {
            //skips the bodies that are not being tracked
            if (body.status == Status.NotTracking)
            {
                continue;
            }
            Debug.Log("Processing a body!");

            //checks if the current tracked body's head is also being tracked
            if (body.joints.ContainsKey(Voxar.JointType.Head))
            {
                Debug.Log("Updating head position!");
                var head = body.joints[Voxar.JointType.Head];
                this.transform.position = head.worldPosition / 1000.0f;

                // the world position is divided by 1000 because it is given in milimeters.
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        print("script started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

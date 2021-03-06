﻿using System.Collections.Generic;
using UnityEngine;
using Voxar;

public class BodyRenderer : MonoBehaviour, IReceiver<BodyJoints[]>
{
    public GameObject JointPrefab;

    private Dictionary<int, Dictionary<JointType, GameObject>> bodySkeletons;

    public bool RelativeToSpine = false;
    public float scale = 150;

    public Vector3 offset = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        this.bodySkeletons = new Dictionary<int, Dictionary<JointType, GameObject>>();
    }

    //ReceiveData is called one per frame
    public void ReceiveData(BodyJoints[] data)
    {
        foreach (var body in data)
        {
            if (body.status == Status.NotTracking)
            {
                continue;
            }

            Dictionary<JointType, GameObject> joints;

            if (!this.bodySkeletons.ContainsKey(body.id))
            {
                joints = new Dictionary<JointType, GameObject>();

                for (int i = 0; i < Voxar.Joint.JointTypeCount; i++)
                {
                    var go = (GameObject)Instantiate(JointPrefab, Vector3.zero, Quaternion.identity);
                    go.transform.SetParent(transform);
                    go.SetActive(false);
                    joints.Add((JointType)i, go);
                }

                this.bodySkeletons.Add(body.id, joints);
            }
            else
            {
                joints = this.bodySkeletons[body.id];
            }

            foreach (KeyValuePair<JointType, Voxar.Joint> entry in body.joints)
            {
                var bodyJoint = entry.Value;
                var skeletonJoint = joints[entry.Key];

                if (bodyJoint.status != Status.NotTracking)
                {
                    if (!skeletonJoint.activeSelf)
                    {
                        skeletonJoint.SetActive(true);
                    }

                    if (RelativeToSpine)
                    {
                        var spineBase = body.joints[JointType.BaseSpine];
                        skeletonJoint.transform.localPosition =
                        new Vector3((bodyJoint.worldPosition.x - spineBase.worldPosition.x) / 1000f,
                                    (bodyJoint.worldPosition.y - spineBase.worldPosition.y) / 1000f,
                                    (bodyJoint.worldPosition.z - spineBase.worldPosition.z) / 1000f);
                    }
                    else
                    {

                        skeletonJoint.transform.localPosition =
                            new Vector3(bodyJoint.worldPosition.x / 1000f,
                                        bodyJoint.worldPosition.y / 1000f,
                                        bodyJoint.worldPosition.z / 1000f);
                    }

                    skeletonJoint.transform.localPosition += offset;

                    // scale skeleton
                    skeletonJoint.transform.localPosition.Scale(new Vector3(scale, scale, scale));



                    skeletonJoint.transform.rotation = Quaternion.LookRotation(bodyJoint.forward, bodyJoint.upwards);
                }
                else
                {
                    if (skeletonJoint.activeSelf) skeletonJoint.SetActive(false);
                }
            }
        }
    }
}
﻿using UnityEngine;
using Voxar;

public enum DefaultPose
{
    TPose,
    Upright,
    MakeContact,
    LeftArmElevation,
    LeftArmDemotion
};

public class PoseMatch : MonoBehaviour, IReceiver<BodyJoints[]>
{

    public float angleTolerance = 10.0f;
    public DefaultPose targetPose = DefaultPose.Upright;


    private DefaultPose oldTarget;
    private BodyAngles targetBodyAngles;

    [SerializeField]
    private bool gesturePerformed;

    // Start is called before the first frame update
    void Start()
    {
        ChangePose();
    }

    // Update is called once per frame
    void Update()
    {
        if (oldTarget != targetPose)
        {
            ChangePose();
        }
        oldTarget = targetPose;
    }

    private void ChangePose()
    {
        switch (targetPose)
        {
            case DefaultPose.Upright:
                targetBodyAngles = BodyAngles.Upright();
                break;
            case DefaultPose.TPose:
                targetBodyAngles = BodyAngles.TPose();
                break;
            case DefaultPose.MakeContact:
                targetBodyAngles = BodyAngles.MakeContact();
                break;
            case DefaultPose.ArmElevation:
                targetBodyAngles = BodyAngles.LateralRightShoulderElevation();
                break;
            case DefaultPose.ArmDemotion:
                targetBodyAngles = BodyAngles.LateralRightShoulderDemotion();
                break;
        }
    }

    public void ReceiveData(BodyJoints[] data)
    {
        foreach (var body in data)
        {
            if (body.status != Status.Tracking)
            {
                continue;
            }

            var currentBodyAngles = new BodyAngles(body);
            var errors = MovementAnalyzer.CompareBodyAngles(currentBodyAngles, targetBodyAngles, angleTolerance);

            if (errors.Count == 0) { gesturePerformed = true; }
            else { gesturePerformed = false; }

            foreach (var error in errors)
            { 
                
                Debug.Log(error.plane + " " + error.boneTypes[0] + "/" + error.boneTypes[1]);
            }
        }
    }
}

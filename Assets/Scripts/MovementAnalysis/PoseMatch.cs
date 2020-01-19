using UnityEngine;
using Voxar;
using UnityEngine.Events;

public enum DefaultPose
{
    TPose,
    Upright,
    MakeContact,
    LeftArmElevation,
    RightArmElevation,
    LeftArmDemotion,
    RightArmDemotion,
    ShouldersElevation,
    Stiff,
    MilDevelop,
    MilDevelopStart
};

[System.Serializable]
public class ErrorEvent : UnityEvent<string> { }

public class PoseMatch : MonoBehaviour, IReceiver<BodyJoints[]>
{
    public bool canTriggerError = false;

    public float angleToleranceAP = 20.0f;
    public float angleToleranceExercise = 10.0f;
    public DefaultPose targetPose = DefaultPose.Upright;
    public DefaultPose startPose = DefaultPose.Upright;

    public ErrorEvent ErrorNotification = new ErrorEvent();

    private DefaultPose oldTarget;
    private BodyAngles targetBodyAngles;
    private BodyAngles inicialBodyAngles;

    [SerializeField]
    private bool gesturePerformed;
    [SerializeField]
    private bool inicialPose;


    private float timer; 
    public float limitTime = 2.0f;
    private int exercisePositionLast, exercisePositionCurrent = 0;
    private int errorCount, errorTemp;
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
            case DefaultPose.LeftArmElevation:
                targetBodyAngles = BodyAngles.LateralLeftShoulderElevation();
                break;
            case DefaultPose.LeftArmDemotion:
                targetBodyAngles = BodyAngles.LateralLeftShoulderDemotion();
                break;
            case DefaultPose.RightArmElevation:
                targetBodyAngles = BodyAngles.LateralRightShoulderElevation();
                break;
            case DefaultPose.RightArmDemotion:
                targetBodyAngles = BodyAngles.LateralRightShoulderDemotion();
                break;
            case DefaultPose.ShouldersElevation:
                targetBodyAngles = BodyAngles.LateralShouldersElevation();
                break;
            case DefaultPose.MilDevelop:
                targetBodyAngles = BodyAngles.MilitaryDevelopment();
                break;
            case DefaultPose.MilDevelopStart:
                targetBodyAngles = BodyAngles.MilitaryDevelopmentStart();
                break;
        }

        switch (startPose)
        {
            case DefaultPose.Upright:
                inicialBodyAngles = BodyAngles.Upright();
                break;
            case DefaultPose.TPose:
                inicialBodyAngles = BodyAngles.TPose();
                break;
            case DefaultPose.MakeContact:
                inicialBodyAngles = BodyAngles.MakeContact();
                break;
            case DefaultPose.LeftArmElevation:
                inicialBodyAngles = BodyAngles.LateralLeftShoulderElevation();
                break;
            case DefaultPose.LeftArmDemotion:
                inicialBodyAngles = BodyAngles.LateralLeftShoulderDemotion();
                break;
            case DefaultPose.RightArmElevation:
                inicialBodyAngles = BodyAngles.LateralRightShoulderElevation();
                break;
            case DefaultPose.RightArmDemotion:
                inicialBodyAngles = BodyAngles.LateralRightShoulderDemotion();
                break;
            case DefaultPose.ShouldersElevation:
                inicialBodyAngles = BodyAngles.LateralShouldersElevation();
                break;
            case DefaultPose.MilDevelop:
                inicialBodyAngles = BodyAngles.MilitaryDevelopment();
                break;
            case DefaultPose.MilDevelopStart:
                inicialBodyAngles = BodyAngles.MilitaryDevelopmentStart();
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
            var errors = MovementAnalyzer.CompareBodyAngles(currentBodyAngles, targetBodyAngles, angleToleranceExercise);

            if (MovementAnalyzer.CompareBodyAngles(currentBodyAngles, inicialBodyAngles, angleToleranceAP).Count == 0)
            {
                inicialPose = true;
            }
            else
            {
                inicialPose = false;
            }
                
            if (errors.Count == 0) 
            { 
                gesturePerformed = true;
                
            }
            else 
            { 
                gesturePerformed = false; 
            }

            Exercise();
            IdentByHeight(body);

            Voxar.Joint Rknee =  body.joints[JointType.RightKnee];
            Voxar.Joint Lknee =  body.joints[JointType.LeftKnee];
            Voxar.Joint Rankle = body.joints[JointType.RightFoot];
            Voxar.Joint Lankle = body.joints[JointType.LeftFoot];
            Voxar.Joint Rhip = body.joints[JointType.RightHip];
            Voxar.Joint Lhip = body.joints[JointType.LeftHip];


            Vector3 RShin = -(Rknee.worldPosition - Rankle.worldPosition);
            Vector3 LShin = -(Lknee.worldPosition - Lankle.worldPosition);
            Vector3 RThigh = (Rknee.worldPosition - Rhip.worldPosition);
            Vector3 LThigh = (Lknee.worldPosition - Lhip.worldPosition);

            float RkneeAngle = Vector3.Angle(RShin,RThigh);
            float LkneeAngle = Vector3.Angle(LShin,LThigh);
            
            //float height = Vector3.Distance(head.worldPosition, basefoot) / 1000;


            /*Debug.Log("LC n LA:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LeftClavicule, BoneType.LeftArm)
                 + " LC n UB:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LeftClavicule, BoneType.UpperBody)
                 + " LA n LFA:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LeftArm, BoneType.LeftForearm) 
                 + " RC n RA:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.RightClavicule, BoneType.RightArm) 
                 + " RC n UB:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.RightClavicule, BoneType.UpperBody) 
                 + " RA n RFA" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.RightArm, BoneType.RightForearm) 
                 );*/
            

            /*foreach (var error in errors)
            {
                
                //Debug.Log(error.plane + " " + error.boneTypes[0] + "/" + error.boneTypes[1]);
            }*/
        }
    }

    private void Exercise()
    {
        
        switch(exercisePositionCurrent)
        {
            case 0:
                InicialPose();
                break;
            case 1:
                FinalPose();
                break;
        }
    }
    private void InicialPose()
    {
        timer += Time.deltaTime;
        if(inicialPose == false && gesturePerformed == true)
        {
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 1;
            timer = 0f;
        }

        else if (timer > limitTime)
        {
            errorCount++;
            if (canTriggerError)
            {
                Debug.Log("Erro aqui");
                ErrorNotification.Invoke("> ; error");
            }
            timer = 0f;
        }
    }
    private void FinalPose()
    {
        if (inicialPose == true && gesturePerformed == false)
        {
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 0;
        }
    }

    private void IdentByHeight(BodyJoints body)
    {
        Voxar.Joint head = body.joints[JointType.Head];
        Voxar.Joint Rankle = body.joints[JointType.RightFoot];
        Voxar.Joint Lankle = body.joints[JointType.LeftFoot];
        Vector3 basefoot = (Lankle.worldPosition + Rankle.worldPosition) / 2;
        float height = Vector3.Distance(head.worldPosition, basefoot)/1000;

        //Debug.Log(" Altura estimada:" + height);

        if (height > 1.3 && height < 1.5)
        {
            //GetComponent<TextMesh>().text = "Thiago Lafayette";
            //GetComponent<TextMesh>().fontSize = 3;
            /*GetComponent<TextMesh>().transform {head.transform.localPosition =
                        new Vector3(bodyJoint.worldPosition.x / 1000f,
                                    bodyJoint.worldPosition.y / 1000f,
                                    bodyJoint.worldPosition.z / 1000f)};*/
        }
        else if(height > 1.65)
        {
            //GetComponent<TextMesh>().text = "Pinho";
            //GetComponent<TextMesh>().fontSize = 3;
        }
        else
        {
            //GetComponent<TextMesh>().text = "";
        }
    }

}

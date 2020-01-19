using UnityEngine;
using Voxar;

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
    Stiff
};



public class PoseMatch : MonoBehaviour, IReceiver<BodyJoints[]>
{

    public float angleToleranceAP = 20.0f;
    public float angleToleranceExercise = 10.0f;
    public DefaultPose targetPose = DefaultPose.Upright;


    private DefaultPose oldTarget;
    private BodyAngles targetBodyAngles;

    [SerializeField]
    private bool gesturePerformed;
    [SerializeField]
    private bool inicialPose;



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

            if (MovementAnalyzer.CompareBodyAngles(currentBodyAngles, BodyAngles.Upright(), angleToleranceAP).Count == 0)
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




            /* Debug.Log("UB n LB:" + currentBodyAngles.SagittalAngles.GetAngle(BoneType.UpperBody, BoneType.LowerBody)
                 + " LB n LH:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LowerBody, BoneType.LeftHipbone) +
                 " LB n RH:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LowerBody, BoneType.RightHipbone)
                 + " LH n LT:" + currentBodyAngles.SagittalAngles.GetAngle(BoneType.LeftHipbone, BoneType.LeftThigh) +
                 " LH n RH:" + currentBodyAngles.FrontalAngles.GetAngle(BoneType.LeftHipbone, BoneType.RightHipbone) +
                 " RH n RT:" + currentBodyAngles.SagittalAngles.GetAngle(BoneType.RightHipbone, BoneType.RightThigh)
                 );*/

            

            foreach (var error in errors)
            {
                
                //Debug.Log(error.plane + " " + error.boneTypes[0] + "/" + error.boneTypes[1]);
            }
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
                TransitionPose();
                break;
            case 2:
                MovingPose();
                break;
            case 3:
                FinalPose();
                break;
        }
    }
    private void InicialPose()
    {
        if(inicialPose == true && gesturePerformed == false)
        {
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 1;
        }
    }
    private void TransitionPose()
    {
        if (inicialPose == false && gesturePerformed == false)
        {
            //exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 2;
        }
    }
    private void MovingPose()
    {
        if (inicialPose == true && gesturePerformed == false)
        {
            if (exercisePositionLast == 0)
            {
                errorTemp++;
                
                if (errorTemp > 2)
                {
                    errorCount++;
                    Debug.Log("Erro no exercicio" + errorCount);
                    errorTemp = 0;
                }
            }
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 0; 

        }
        else if (inicialPose == false && gesturePerformed == true)
        {
            if (exercisePositionLast == 3)
            {
                Debug.Log("Exercicio Estranho");
            }
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 3;
        }
    }
    private void FinalPose()
    {
        if (inicialPose == false && gesturePerformed == true)
        {
            exercisePositionLast = exercisePositionCurrent;
            exercisePositionCurrent = 1;
        }
    }

    private void IdentByHeight(BodyJoints body)
    {
        Voxar.Joint head = body.joints[JointType.Head];
        Voxar.Joint Rankle = body.joints[JointType.RightFoot];
        Voxar.Joint Lankle = body.joints[JointType.LeftFoot];
        Vector3 basefoot = (Lankle.worldPosition + Rankle.worldPosition) / 2;
        float height = Vector3.Distance(head.worldPosition, basefoot)/1000;

        Debug.Log(" Altura estimada:" + height);

        if (height > 1.5 && height < 1.6)
        {
            GetComponent<TextMesh>().text = "Thiago Lafayette";
            /*GetComponent<TextMesh>().transform = head.transform.localPosition =
                        new Vector3(bodyJoint.worldPosition.x / 1000f,
                                    bodyJoint.worldPosition.y / 1000f,
                                    bodyJoint.worldPosition.z / 1000f);*/
        }
    }

}

namespace Voxar
{
    public class BodyAngles
    {
        //a complete body targetBodyAngles, takes in consideration the angles between the bones in ALL default planes

        public PlaneBasedBodyAngles FrontalAngles;
        public PlaneBasedBodyAngles SagittalAngles;
        public PlaneBasedBodyAngles HorizontalAngles;

        //creates a body targetBodyAngles that has no angles, but initializes the partial body poses
        public BodyAngles()
        {
            FrontalAngles = new PlaneBasedBodyAngles(BasePlanes.Frontal);
            SagittalAngles = new PlaneBasedBodyAngles(BasePlanes.Sagittal);
            HorizontalAngles = new PlaneBasedBodyAngles(BasePlanes.Horizontal);
        }

        public BodyAngles(BodyJoints body)
        {
            FrontalAngles = new PlaneBasedBodyAngles(body, BasePlanes.Frontal);
            SagittalAngles = new PlaneBasedBodyAngles(body, BasePlanes.Sagittal);
            HorizontalAngles = new PlaneBasedBodyAngles(body, BasePlanes.Horizontal);
        }

        public static BodyAngles Upright()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);

            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 80);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 5);
            //pose.FrontalAngles.AddAngle(BoneType.LeftForearm, BoneType.LeftWrist, 0);

            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 80);
            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 5);
            //pose.FrontalAngles.AddAngle(BoneType.RightForearm, BoneType.RightWrist, 0);

            //pose.FrontalAngles.AddAngle(BoneType.UpperBody, BoneType.LowerBody, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.LeftHipbone, 120);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.RightHipbone, 120);

            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.LeftThigh, 70);
            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.RightHipbone, 120);

            //pose.FrontalAngles.AddAngle(BoneType.LeftThigh, BoneType.LeftLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.LeftLeg, BoneType.LeftAnkle, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightHipbone, BoneType.RightThigh, 70);

            //pose.FrontalAngles.AddAngle(BoneType.RightThigh, BoneType.RightLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightLeg, BoneType.RightAnkle, 90);

            return pose;
        }

        public static BodyAngles Stiff()
        {
            var pose = new BodyAngles();

            //pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            //pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            //pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 80);
            //pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);

            //pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 0);
            

            //pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 80);
            //pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);

            //pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 0);
           

            pose.SagittalAngles.AddAngle(BoneType.UpperBody, BoneType.LowerBody, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.LeftHipbone, 120);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.RightHipbone, 120);

            pose.SagittalAngles.AddAngle(BoneType.LeftHipbone, BoneType.LeftThigh, 70);
            pose.SagittalAngles.AddAngle(BoneType.RightHipbone, BoneType.RightThigh, 70);
            //pose.SagittalAngles.AddAngle(BoneType.RightThigh, BoneType.RightLeg, 70);
            //pose.SagittalAngles.AddAngle(BoneType.RightLeg, BoneType.RightThigh, 70);
            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.RightHipbone, 120);

            return pose;
        }

        public static BodyAngles MilitaryDevelopmentStart()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 40);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);
            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 135);


            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 40);
            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 135);

            return pose;
        }

        public static BodyAngles MilitaryDevelopment()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 30);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 80);
            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 70);


            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 40);
            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 80);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 70);

            return pose;
        }

        public static BodyAngles LateralLeftShoulderDemotion()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 0);

            return pose;
        }

        public static BodyAngles LateralLeftShoulderElevation()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 0);

            return pose;
        }
        public static BodyAngles LateralShouldersElevation()
        {
            var pose = new BodyAngles();

            /*pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);

            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);*/

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 10);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 10);


            /*pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);


            pose.FrontalAngles.AddAngle(BoneType.UpperBody, BoneType.LowerBody, 0);*/

            return pose;
        }

        public static BodyAngles LateralRightShoulderElevation()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 0);

            return pose;
        }
        public static BodyAngles LateralRightShoulderDemotion()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 0);

            return pose;
        }

        public static BodyAngles TPose()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);

            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftForearm, BoneType.LeftWrist, 0);

            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightForearm, BoneType.RightWrist, 0);

            pose.FrontalAngles.AddAngle(BoneType.UpperBody, BoneType.LowerBody, 0);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.LeftHipbone, 90);
            //pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.RightHipbone, 90);

            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.LeftThigh, 90);
            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.RightHipbone, 0);

            //pose.FrontalAngles.AddAngle(BoneType.LeftThigh, BoneType.LeftLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.LeftLeg, BoneType.LeftAnkle, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightHipbone, BoneType.RightThigh, 90);

            //pose.FrontalAngles.AddAngle(BoneType.RightThigh, BoneType.RightLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightLeg, BoneType.RightAnkle, 90);

            return pose;
        }

        public static BodyAngles MakeContact()
        {
            var pose = new BodyAngles();

            pose.FrontalAngles.AddAngle(BoneType.UpperNeck, BoneType.LowerNeck, 0);

            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.UpperBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.LeftClavicule, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerNeck, BoneType.RightClavicule, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.RightClavicule, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.LeftArm, 90);
            pose.FrontalAngles.AddAngle(BoneType.LeftClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.LeftArm, BoneType.LeftForearm, 0);
            pose.FrontalAngles.AddAngle(BoneType.LeftForearm, BoneType.LeftWrist, 0);

            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.RightArm, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightClavicule, BoneType.UpperBody, 90);

            pose.FrontalAngles.AddAngle(BoneType.RightArm, BoneType.RightForearm, 0);
            pose.FrontalAngles.AddAngle(BoneType.RightForearm, BoneType.RightWrist, 0);

            pose.FrontalAngles.AddAngle(BoneType.UpperBody, BoneType.LowerBody, 0);
            pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.LeftHipbone, 90);
            pose.FrontalAngles.AddAngle(BoneType.LowerBody, BoneType.RightHipbone, 90);

            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.LeftThigh, 90);
            //pose.FrontalAngles.AddAngle(BoneType.LeftHipbone, BoneType.RightHipbone, 0);

            //pose.FrontalAngles.AddAngle(BoneType.LeftThigh, BoneType.LeftLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.LeftLeg, BoneType.LeftAnkle, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightHipbone, BoneType.RightThigh, 90);

            //pose.FrontalAngles.AddAngle(BoneType.RightThigh, BoneType.RightLeg, 0);

            //pose.FrontalAngles.AddAngle(BoneType.RightLeg, BoneType.RightAnkle, 90);

            return pose;
        }
    }
}

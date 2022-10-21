using System;



namespace RobotController
{

    public struct MyQuat
    {
        public float w;
        public float x;
        public float y;
        public float z;
       
    }

    public struct MyVec
    {
        public float x;
        public float y;
        public float z;
    }






    public class MyRobotController
    {
        
        #region public methods

        public string Hi()
        {

            string s = "Jordi Barcia i Pol Coller";
            return s;

        }
        float time = 0;

        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {
            time = 0;
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;
            //todo: change this, use the function Rotate declared below
            MyVec axis;
            axis.x = 0;
            axis.y = 1;
            axis.z = 0;
            rot0 = Rotate(NullQ, axis, 73);
            axis.x = 1;
            axis.y = 0;
            rot1 = Rotate(rot0, axis, 0);
            rot2 = Rotate(rot1, axis, 68);
            rot3 = Rotate(rot2, axis, 34);

        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;
            bool myCondition = true;
            //todo: add a check for your condition
            MyVec axis;

            axis.x = 0;
            axis.y = 1;
            axis.z = 0;
            rot0 = Rotate(NullQ, axis, 73);
            axis.x = 1;
            axis.y = 0;
            rot1 = Rotate(rot0, axis, 0);
            rot2 = Rotate(rot1, axis, 68);
            rot3 = Rotate(rot2, axis, 34);

            if (myCondition)
            {
                //todo: add your code here

                axis.x = 0;
                axis.y = 1;
                axis.z = 0;
                rot0 = Slerp(rot0, Rotate(NullQ, axis, 35), time);
                //rot0 = Slerp(rot0, Rotate(NullQ, axis, 30), time);
                axis.x = 1;
                axis.y = 0;
                rot1 = Slerp(rot1, Rotate(rot0, axis, 5), time);
                rot2 = Slerp(rot2, Rotate(rot1, axis, 70), time);
                rot3 = Slerp(rot3, Rotate(rot2, axis, 16), time);
                //rot1 = Slerp(rot1, Rotate(rot0, axis, -5), time);
                //rot2 = Slerp(rot2, Rotate(rot1, axis, 84), time);
                //rot3 = Slerp(rot3, Rotate(rot2, axis, 16), time);
                time += 0.0033f;
                if (time >= 1f)
                {
                    return false;
                    
                }
                return true;
            }


            return false;
        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            bool myCondition = false;
            //todo: add a check for your condition



            while (myCondition)
            {
                //todo: add your code here


            }

            //todo: remove this once your code works.
            rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }




        #endregion


        #region private and internal methods

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }


        private static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        internal MyQuat Multiply(MyQuat q1, MyQuat q2)
        {
            //todo: change this so it returns a multiplication:
            MyQuat ans;
            ans.w = (q1.w * q2.w) - (q1.x * q2.x) - (q1.y * q2.y) - (q1.z * q2.z);
            ans.x = (q1.w * q2.x) + (q1.x * q2.w) + (q1.y * q2.z) - (q1.z * q2.y);
            ans.y = (q1.w * q2.y) - (q1.x * q2.z) + (q1.y * q2.w) + (q1.z * q2.x);
            ans.z = (q1.w * q2.z) + (q1.x * q2.y) - (q1.y * q2.x) + (q1.z * q2.w);

            return ans;
        }

        internal MyQuat Addition(MyQuat q1, MyQuat q2)
        {
            //todo: change this
            MyQuat ans;
            ans.w = q1.w * q2.w;
            ans.x = q1.x * q2.x;
            ans.y = q1.y * q2.y;
            ans.z = q1.z * q2.z;

            return ans;
        }

        internal MyQuat Normalize(MyQuat myQuat)
        {
            MyQuat ans;
            float multQuat = myQuat.w * myQuat.w + myQuat.x * myQuat.x + myQuat.y * myQuat.y + myQuat.z * myQuat.z;
            float sqQuat = (float)Math.Sqrt(multQuat);

            ans.w = myQuat.w / sqQuat;
            ans.x = myQuat.x / sqQuat;
            ans.y = myQuat.y / sqQuat;
            ans.z = myQuat.z / sqQuat;

            return ans;
        }
        internal MyQuat Conjugate(MyQuat myQuat)
        {
            MyQuat ans;

            ans = Normalize(myQuat);
            ans.x = -myQuat.x;
            ans.y = -myQuat.y;
            ans.z = -myQuat.z;

            return ans;
        }

        internal MyQuat Rotate(MyQuat currentRotation, MyVec axis, float angle)
        {
            //todo: change this so it takes currentRotation, and calculate a new quaternion rotated by an angle "angle" radians along the normalized axis "axis"
            MyQuat ans;
            float radian, pi;
            pi = 3.141592653589793f;

            //radian = (angle * (pi /180));
            radian = (angle * pi) / 180;
            float halfAngle = radian * 0.5f;
            float s = (float)Math.Sin(halfAngle);
            float c = (float)Math.Cos(halfAngle);

            ans.w = c;

            ans.x = s * axis.x;
            ans.y = s * axis.y;
            ans.z = s * axis.z;

            ans = Normalize(ans);
            currentRotation = Normalize(currentRotation);

            currentRotation = Multiply(currentRotation, ans);

            return Normalize(currentRotation);
        }

        //todo: add here all the functions needed

        internal MyQuat Slerp(MyQuat q1, MyQuat q2, float time)
        {
            float angle;
            angle = q1.w * q2.w + q1.x * q2.x + q1.y * q2.y + q1.z * q2.z;
            MyQuat ans;
  
            ans.w = ((float)Math.Sin((1 - time) * angle) / (float)Math.Sin(angle)) * q1.w + ((float)Math.Sin((time) * angle) / (float)Math.Sin(angle)) * q2.w;
            ans.x = ((float)Math.Sin((1 - time) * angle) / (float)Math.Sin(angle)) * q1.x + ((float)Math.Sin((time) * angle) / (float)Math.Sin(angle)) * q2.x;
            ans.y = ((float)Math.Sin((1 - time) * angle) / (float)Math.Sin(angle)) * q1.y + ((float)Math.Sin((time) * angle) / (float)Math.Sin(angle)) * q2.y;
            ans.z = ((float)Math.Sin((1 - time) * angle) / (float)Math.Sin(angle)) * q1.z + ((float)Math.Sin((time) * angle) / (float)Math.Sin(angle)) * q2.z;
            //(sin((1-t)radian)/sin(radian))* a + (sin(radian)/sin(t*radian))* b
            Normalize(ans);
            return ans;
        }

        #endregion






    }
}
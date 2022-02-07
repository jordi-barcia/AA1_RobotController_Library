using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RobotController;


public class RobotAnimator : MonoBehaviour
{

    [System.Serializable]
    enum Exercises {
        Exercise0,Exercise1,Exercise2,Exercise3
    };
    
    [SerializeField]
    Exercises ex;

    [SerializeField]
    PickMe stud;

    [SerializeField]
    Text TextCanvas;


    MyRobotController robot = new MyRobotController();


    //the quaternion values:
    RobotController.MyQuat j0 = new MyQuat(), j1 = new MyQuat(), j2 = new MyQuat(), j3 = new MyQuat();
    
    //here I would want to define an implicit cast with a syntax like:
    //public static implicit operator Quaternion(MyQuat q)
    //However, this cann only be done in class Quaternion. So, I define a static function that does something similar:
    static Quaternion Quaternion(MyQuat q)
    {
        Quaternion temp = new Quaternion(q.x, q.y, q.z, q.w);
        return temp;
    }









    [SerializeField]
    Transform[] joints; 



    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("testing communication with controller");

        Debug.Log(robot.Hi());
        j0 = new MyQuat();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("1"))
        {
            ex = Exercises.Exercise1;
        }
        else if (Input.GetKeyDown("2"))
        {
            ex = Exercises.Exercise2;
        }
        else if (Input.GetKeyDown("3"))
        {
            ex = Exercises.Exercise3;
        }
        else if (Input.GetKeyDown("0"))
        {
            ex = Exercises.Exercise0;
            stud.LeaveMe();


        }
        else if (Input.GetKeyDown("r"))
        {
            stud.Reset();

        }


        switch (ex)
        {
            case Exercises.Exercise1:

                //Debug.Log("Exercise 1 running");
                TextCanvas.text = "Exercise 1 running";

                robot.PutRobotStraight(out j0, out j1, out j2, out j3);


                joints[0].rotation = Quaternion(j0);
                joints[1].rotation = Quaternion(j1);
                joints[2].rotation = Quaternion(j2);
                joints[3].rotation = Quaternion(j3);
                break;




            case Exercises.Exercise2:
                //Debug.Log("Exercise 2 running");
                TextCanvas.text = "Exercise 2 running";



                if (robot.PickStudAnim(out j0, out j1, out j2, out j3))
                {
                    //Debug.Log("moving the robot to pick and leave the Stud");
                    joints[0].rotation = Quaternion(j0);
                    joints[1].rotation = Quaternion(j1);
                    joints[2].rotation = Quaternion(j2);
                    joints[3].rotation = Quaternion(j3);

                }
                else
                {
                    //once we are done, we drop the stud
                    stud.LeaveMe();
                }
                break;
            case Exercises.Exercise3:
                //Debug.Log("Exercise 3 running");
                TextCanvas.text = "Exercise 3 running";



                if (robot.PickStudAnimVertical(out j0, out j1, out j2, out j3))
                {
                    //Debug.Log("moving the robot to pick and leave the Stud horizontal");
                    joints[0].rotation = Quaternion(j0);
                    joints[1].rotation = Quaternion(j1);
                    joints[2].rotation = Quaternion(j2);
                    joints[3].rotation = Quaternion(MyRobotController.GetSwing(j3));
                    joints[4].rotation = Quaternion(MyRobotController.GetTwist(j3));

                }
                else
                {
                  
                    //once we are done, we drop the stud
                    stud.LeaveMe();
                }
                break;

            case Exercises.Exercise0:
                TextCanvas.text = "Exercise 0 running";


                break;





        }

    }
}

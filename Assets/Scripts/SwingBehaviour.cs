using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehaviour : MonoBehaviour
{
    public HingeJoint2D joint;
    private JointMotor2D motor;
    public int swingSpeed;
    public int maxAngle;

    void Awake()
    {
        motor = joint.motor;
        SetMotor(swingSpeed);
    }

    void Update()
    {
        //Checking the joint angle manually in a script is the only way to make this work so far
        //The "angle limit" option in the inspector gets the joint stuck when it hits the limit, refusing to move back the other way
        if (joint.jointAngle > maxAngle)
        {
            SetMotor(-swingSpeed);
        } 
        else if (joint.jointAngle < -maxAngle)
        {
            SetMotor(swingSpeed);
        }
        print(motor.motorSpeed);
    }

    private void SetMotor(int speed)
    {
        motor.motorSpeed = speed;
        joint.motor = motor;
    }
}

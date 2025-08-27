using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AracnoControl : MonoBehaviour 
{
    public HingeJoint2D[] joints;
    JointMotor2D motor;

	void Start ()
    {
        motor.maxMotorTorque = 10000;
    }
	
	void FixedUpdate () {
        motor.motorSpeed = Input.GetAxis("Horizontal")*100;

        foreach (HingeJoint2D joint in joints)
        {
            joint.motor = motor;
        }
	}
}

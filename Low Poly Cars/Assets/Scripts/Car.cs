using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderRightBack;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    public float motorTorque = 100f;
    public float maxSteer = 20f;

    void FixedUpdate()
    {
        wheelColliderLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        wheelColliderRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
     
        wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;

        Debug.Log(Input.GetAxis("Horizontal"));

    }

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;
        wheelColliderLeftFront.GetWorldPose(out pos, out rot);
        //wheelLeftFront.localPosition = pos;
        wheelLeftFront.rotation = rot * Quaternion.Euler(0, 180, 0);

        wheelColliderRightFront.GetWorldPose(out pos, out rot);
        //wheelRightFront.position = pos;
        wheelRightFront.rotation = rot * Quaternion.Euler(0,180, 0);

        wheelColliderLeftBack.GetWorldPose(out pos, out rot);
        //wheelLeftBack.position = pos;
        wheelLeftBack.rotation = rot * Quaternion.Euler(0, 180, 0);

        wheelColliderRightBack.GetWorldPose(out pos, out rot);
        //wheelRightBack.position = pos;
        wheelRightBack.rotation = rot * Quaternion.Euler(0, 180, 0);
    }
}

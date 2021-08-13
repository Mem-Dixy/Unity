using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform centerOfMass;
    
    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderLeftBack;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderRightBack;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftBack;
    public Transform wheelRightBack;

    public Transform gun;
    public Transform view;
    public Transform aim;
    public Transform car;

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {


        var gamepad = UnityEngine.InputSystem.Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.rightTrigger.wasPressedThisFrame) {
            // 'Use' code here
        }

        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();

        float lx = UnityEngine.InputSystem.Gamepad.current.leftStick.x.ReadValue();
        float ly = UnityEngine.InputSystem.Gamepad.current.leftStick.y.ReadValue();
        float rx = UnityEngine.InputSystem.Gamepad.current.rightStick.x.ReadValue();
        float ry = UnityEngine.InputSystem.Gamepad.current.rightStick.y.ReadValue();


        Quaternion forward = Quaternion.identity;
        forward.eulerAngles = Vector3.forward;



        Vector3 zero = new Vector3(10 * lx , 0 , 10 * ly);
        Vector3 one = car.position + zero;
        Vector3 point = car.InverseTransformVector(zero);
        aim.position = one;
        float turn = Mathf.Atan2(point.x , point.z);
        turn = Mathf.Sign(turn) * Mathf.Min(Mathf.Abs(turn), Mathf.Deg2Rad * maxSteer);

        Vector3 face = car.TransformVector(aim.position - car.position);
        float forwardy = Mathf.Clamp01(face.z);

        wheelColliderLeftBack.motorTorque = forwardy * motorTorque;
        wheelColliderRightBack.motorTorque = forwardy * motorTorque;
        wheelColliderLeftFront.steerAngle = turn * maxSteer;
        wheelColliderRightFront.steerAngle = turn * maxSteer;

        Vector3 relativePos = new Vector3(rx, 0, ry);
        Quaternion rotation = Quaternion.LookRotation(relativePos , Vector3.up);
        gun.localRotation = rotation;
    }

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelColliderLeftFront.GetWorldPose(out pos, out rot);
        wheelLeftFront.position = pos;
        wheelLeftFront.rotation = rot * Quaternion.Euler(0, 180, -90);

        wheelColliderRightFront.GetWorldPose(out pos, out rot);
        wheelRightFront.position = pos;
        wheelRightFront.rotation = rot * Quaternion.Euler(0,180,90);

        wheelColliderLeftBack.GetWorldPose(out pos, out rot);
        wheelLeftBack.position = pos;
        wheelLeftBack.rotation = rot * Quaternion.Euler(0, 180, -90);

        wheelColliderRightBack.GetWorldPose(out pos, out rot);
        wheelRightBack.position = pos;
        wheelRightBack.rotation = rot * Quaternion.Euler(0, 180,90);
    }
}

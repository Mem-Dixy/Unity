using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Jeep : MonoBehaviour {
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

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    private Rigidbody _rigidbody;

    public float driveForce {
        get => _driveForce;
        set {
            _driveForce = Mathf.Clamp(value , -1 , 1);
         }
    }
    private float _driveForce;

    public float turnForce {
        get => _turnForce;
        set {
            _turnForce = Mathf.Clamp(value , -1 , 1);
        }
    }
    private float _turnForce;



    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate() {
        wheelColliderLeftBack.motorTorque = driveForce * motorTorque;
        wheelColliderRightBack.motorTorque = driveForce * motorTorque;
        wheelColliderLeftFront.steerAngle = turnForce * maxSteer;
        wheelColliderRightFront.steerAngle = turnForce * maxSteer;
    }

    public void Update() {
        TurnWheel(wheelColliderLeftFront , wheelLeftFront);
        TurnWheel(wheelColliderRightFront , wheelRightFront);
        TurnWheel(wheelColliderLeftBack , wheelLeftBack);
        TurnWheel(wheelColliderRightBack , wheelRightBack);
    }

    private void TurnWheel(WheelCollider collider , Transform transform) {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos , out quat);
        transform.position = pos;
        transform.rotation = quat;
    }
}
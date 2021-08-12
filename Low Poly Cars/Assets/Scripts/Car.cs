using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Car : MonoBehaviour {
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

	void FixedUpdate() {
		wheelColliderLeftBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
		wheelColliderRightBack.motorTorque = Input.GetAxis("Vertical") * motorTorque;
		wheelColliderLeftFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
		wheelColliderRightFront.steerAngle = Input.GetAxis("Horizontal") * maxSteer;
	}

	void Update() {
        WheelTurn(wheelColliderLeftFront, wheelLeftFront);
        WheelTurn(wheelColliderRightFront, wheelRightFront);
        WheelTurn(wheelColliderLeftBack, wheelLeftBack);
        WheelTurn(wheelColliderRightBack, wheelRightBack);
	}

	private void WheelTurn(UnityEngine.WheelCollider collider , UnityEngine.Transform wheel) {
		UnityEngine.Vector3 pos = Vector3.zero;
		UnityEngine.Quaternion quat = Quaternion.identity;
		collider.GetWorldPose(out pos , out quat);
		wheel.position = pos;/
		wheel.rotation = quat;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeepController : MonoBehaviour {
    public Transform playerCamera;

    // the rotation of this object defines the world up direction
    public Transform virtualGroundOrientationReference;
    private Vector3 virtualGroundNormal;

    // the x y z axis of the world calculated from the ground reference and the camera
    private Vector3 worldAxisUp;
    private Vector3 worldAxisForward;
    private Vector3 worldAxisRight;

    // would be nice to remove the dependency of this empty object for vector calculations
    private Transform inputHelper;

    public Transform aimCar;
    public Transform aimGun;

    public Jeep jeep;


    void Start() {
        jeep = GetComponent<Jeep>();
        
        // make input helper
        inputHelper = new GameObject("Input Helper").transform;
    }

    void FixedUpdate() {


        var gamepad = UnityEngine.InputSystem.Gamepad.current;
        if (gamepad == null) {
            return; // No gamepad connected.
        }
        Vector2 leftStick = gamepad.leftStick.ReadValue();
        Vector2 rightStick = gamepad.rightStick.ReadValue();

        float lx = gamepad.leftStick.x.ReadValue();
        float ly = gamepad.leftStick.y.ReadValue();
        float rx = gamepad.rightStick.x.ReadValue();
        float ry = gamepad.rightStick.y.ReadValue();


        Vector3 direction = new Vector3(10 * lx , 0 , 10 * ly);
        Vector3 targetPoint = inputHelper.TransformDirection(direction);
        Vector3 offsetPoint = targetPoint + transform.position;
        //
        Vector3 point = transform.InverseTransformPoint(offsetPoint);
        float turn = Mathf.Atan2(point.x , point.z);

        float face = Vector3.Dot(Vector3.forward , point);
        float forwardy = Mathf.Clamp01(face);

        //
        Vector3 direction2 = new Vector3(8 * rx , 0 , 8 * ry);
        Vector3 targetPoint2 = inputHelper.TransformDirection(direction2);
        //
        jeep.turnForce = turn;
        jeep.driveForce = forwardy;
    /*
    Vector3 offsetPoint2 = targetPoint2 + gun.position;
    Vector3 relativeUp = transform.TransformDirection(Vector3.up);
    gun.LookAt(offsetPoint2 , relativeUp);
    */


    aimCar.position = offsetPoint;
        //aimGun.position = offsetPoint2;

    }

    public void Update() {
        // find plane normal
        virtualGroundNormal = virtualGroundOrientationReference.TransformDirection(Vector3.up);
        // fix camera rotation
        Vector3 worldPosition = playerCamera.TransformPoint(Vector3.forward);
        Vector3 worldUp = virtualGroundNormal;
        playerCamera.LookAt(worldPosition , worldUp);
        // set axis
        worldAxisUp = virtualGroundNormal;
        worldAxisForward = playerCamera.TransformVector(Vector3.forward);
        Vector3.OrthoNormalize(ref worldAxisUp , ref worldAxisForward , ref worldAxisRight);
        // adjust input helper
        inputHelper.LookAt(worldAxisForward , worldAxisUp);
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// Vector3.ProjectOnPlane - example

// Generate a random plane in xy. Show the position of a random
// vector and a connection to the plane. The example shows nothing
// in the Game view but uses Update(). The script reference example
// uses Gizmos to show the positions and axes in the Scene.

public class WorldEnd : UnityEngine.MonoBehaviour {
    public Transform playerCamera;
    public Transform point;
    public Transform drop;
    public Transform spot;

    // the rotation of this object defines the world up direction
    public Transform virtualGroundOrientationReference;


    // the x y z axis of the world calculated from the ground reference and the camera
    public Vector3 worldAxisUp;
    public Vector3 worldAxisForward;
    public Vector3 worldAxisRight;

    public Vector3 virtualGroundNormal;

    // would be nice to remove the dependency of this empty object for vector calculations
    private Transform inputHelper;

    private void Start() {
        virtualGroundNormal = virtualGroundOrientationReference.TransformDirection(Vector3.up);
        inputHelper = new GameObject("Input Helper").transform;
    }


    private void Update() {


        Vector3 camera_position = playerCamera.position;
        Vector3 camera_shadow = new Vector3(camera_position.x , 0 , camera_position.z); // project?

        Vector3 cam_ray1 = camera_shadow - camera_position;
        Vector3 cam_ray = new Vector3(0 , -playerCamera.position.y , 0);

        drop.position = virtualGroundOrientationReference.TransformVector(Vector3.up);
        spot.position = virtualGroundOrientationReference.TransformPoint(Vector3.up);
        point.position = virtualGroundOrientationReference.TransformDirection(Vector3.up); 

        Vector3 aforwardian = playerCamera.TransformVector(Vector3.forward);
        Vector3 forwardian = aforwardian = playerCamera.position;
        Vector3 putt = Vector3.Project(cam_ray , forwardian);
        Vector3 final = putt - camera_shadow;
        point.rotation = Quaternion.LookRotation(final , Vector3.up);

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
        //
    }

    // Show a Scene view example.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Vector3.zero , virtualGroundNormal);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector3.zero , 0.05f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero , worldAxisUp);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero , worldAxisForward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero , worldAxisRight);

    }
}

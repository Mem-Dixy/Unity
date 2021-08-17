using UnityEngine;

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
        this.virtualGroundNormal = this.virtualGroundOrientationReference.TransformDirection(Vector3.up);
		this.inputHelper = new GameObject("Input Helper").transform;
    }

    private void Update() {

        Vector3 camera_position = this.playerCamera.position;
        Vector3 camera_shadow = new Vector3(camera_position.x , 0 , camera_position.z); // project?

        Vector3 cam_ray = new Vector3(0 , -this.playerCamera.position.y , 0);

		this.drop.position = this.virtualGroundOrientationReference.TransformVector(Vector3.up);
		this.spot.position = this.virtualGroundOrientationReference.TransformPoint(Vector3.up);
		this.point.position = this.virtualGroundOrientationReference.TransformDirection(Vector3.up); 

        Vector3 aforwardian = this.playerCamera.TransformVector(Vector3.forward);
        Vector3 forwardian = aforwardian - this.playerCamera.position;
        Vector3 putt = Vector3.Project(cam_ray , forwardian);
        Vector3 final = putt - camera_shadow;
		this.point.rotation = Quaternion.LookRotation(final , Vector3.up);

		// find plane normal
		this.virtualGroundNormal = this.virtualGroundOrientationReference.TransformDirection(Vector3.up);
        // fix camera rotation
        Vector3 worldPosition = this.playerCamera.TransformPoint(Vector3.forward);
        Vector3 worldUp = this.virtualGroundNormal;
		this.playerCamera.LookAt(worldPosition , worldUp);
		// set axis
		this.worldAxisUp = this.virtualGroundNormal;
		this.worldAxisForward = this.playerCamera.TransformVector(Vector3.forward);
        Vector3.OrthoNormalize(ref this.worldAxisUp , ref this.worldAxisForward , ref this.worldAxisRight);
		// adjust input helper
		this.inputHelper.LookAt(this.worldAxisForward , this.worldAxisUp);
        //
    }

    // Show a Scene view example.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(Vector3.zero , this.virtualGroundNormal);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector3.zero , 0.05f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector3.zero , this.worldAxisUp);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero , this.worldAxisForward);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero , this.worldAxisRight);

    }
}

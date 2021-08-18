namespace game {
	public class WorldEnd : UnityEngine.MonoBehaviour {
		public UnityEngine.Transform playerCamera;
		public UnityEngine.Transform point;
		public UnityEngine.Transform drop;
		public UnityEngine.Transform spot;

		// the rotation of this object defines the world up direction
		public UnityEngine.Transform virtualGroundOrientationReference;

		// the x y z axis of the world calculated from the ground reference and the camera
		public UnityEngine.Vector3 worldAxisUp;
		public UnityEngine.Vector3 worldAxisForward;
		public UnityEngine.Vector3 worldAxisRight;

		public UnityEngine.Vector3 virtualGroundNormal;

		// would be nice to remove the dependency of this empty object for vector calculations
		private UnityEngine.Transform inputHelper;

		private void Start() {
			this.virtualGroundNormal = this.virtualGroundOrientationReference.TransformDirection(UnityEngine.Vector3.up);
			this.inputHelper = new UnityEngine.GameObject("Input Helper").transform;
		}

		private void Update() {

			UnityEngine.Vector3 camera_position = this.playerCamera.position;
			UnityEngine.Vector3 camera_shadow = new UnityEngine.Vector3(camera_position.x, 0, camera_position.z); // project?

			UnityEngine.Vector3 cam_ray = new UnityEngine.Vector3(0, -this.playerCamera.position.y, 0);

			this.drop.position = this.virtualGroundOrientationReference.TransformVector(UnityEngine.Vector3.up);
			this.spot.position = this.virtualGroundOrientationReference.TransformPoint(UnityEngine.Vector3.up);
			this.point.position = this.virtualGroundOrientationReference.TransformDirection(UnityEngine.Vector3.up);

			UnityEngine.Vector3 aforwardian = this.playerCamera.TransformVector(UnityEngine.Vector3.forward);
			UnityEngine.Vector3 forwardian = aforwardian - this.playerCamera.position;
			UnityEngine.Vector3 putt = UnityEngine.Vector3.Project(cam_ray, forwardian);
			UnityEngine.Vector3 final = putt - camera_shadow;
			this.point.rotation = UnityEngine.Quaternion.LookRotation(final, UnityEngine.Vector3.up);

			// find plane normal
			this.virtualGroundNormal = this.virtualGroundOrientationReference.TransformDirection(UnityEngine.Vector3.up);
			// fix camera rotation
			UnityEngine.Vector3 worldPosition = this.playerCamera.TransformPoint(UnityEngine.Vector3.forward);
			UnityEngine.Vector3 worldUp = this.virtualGroundNormal;
			this.playerCamera.LookAt(worldPosition, worldUp);
			// set axis
			this.worldAxisUp = this.virtualGroundNormal;
			this.worldAxisForward = this.playerCamera.TransformVector(UnityEngine.Vector3.forward);
			UnityEngine.Vector3.OrthoNormalize(ref this.worldAxisUp, ref this.worldAxisForward, ref this.worldAxisRight);
			// adjust input helper
			this.inputHelper.LookAt(this.worldAxisForward, this.worldAxisUp);
		}

		// Show a Scene view example.
		private void OnDrawGizmosSelected() {
			UnityEngine.Gizmos.color = UnityEngine.Color.white;
			UnityEngine.Gizmos.DrawLine(UnityEngine.Vector3.zero, this.virtualGroundNormal);

			UnityEngine.Gizmos.color = UnityEngine.Color.blue;
			UnityEngine.Gizmos.DrawSphere(UnityEngine.Vector3.zero, 0.05f);

			UnityEngine.Gizmos.color = UnityEngine.Color.green;
			UnityEngine.Gizmos.DrawLine(UnityEngine.Vector3.zero, this.worldAxisUp);
			UnityEngine.Gizmos.color = UnityEngine.Color.blue;
			UnityEngine.Gizmos.DrawLine(UnityEngine.Vector3.zero, this.worldAxisForward);
			UnityEngine.Gizmos.color = UnityEngine.Color.red;
			UnityEngine.Gizmos.DrawLine(UnityEngine.Vector3.zero, this.worldAxisRight);
		}
	}
}
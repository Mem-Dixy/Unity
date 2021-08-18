namespace game {
	public class JeepController : UnityEngine.MonoBehaviour {
		public UnityEngine.Transform playerCamera;

		// the rotation of this object defines the world up direction
		public UnityEngine.Transform virtualGroundOrientationReference;
		private UnityEngine.Vector3 virtualGroundNormal;

		// the x y z axis of the world calculated from the ground reference and the camera
		private UnityEngine.Vector3 worldAxisUp;
		private UnityEngine.Vector3 worldAxisForward;
		private UnityEngine.Vector3 worldAxisRight;

		// would be nice to remove the dependency of this empty object for vector calculations
		private UnityEngine.Transform inputHelper;

		public UnityEngine.Transform aimCar;
		public UnityEngine.Transform aimGun;

		public Jeep jeep;
		public System.Single fireDeadZone = 0.2f;

		private void Start() {
			this.jeep = this.GetComponent<Jeep>();

			// make input helper
			this.inputHelper = new UnityEngine.GameObject("Input Helper").transform;
		}

		private void FixedUpdate() {

			UnityEngine.InputSystem.Gamepad gamepad = UnityEngine.InputSystem.Gamepad.current;

			if (gamepad == null) {
				return;
			}

			System.Boolean fire = gamepad.triangleButton.isPressed;
			this.jeep._rigidbody.isKinematic = fire;
			if (fire) {
				this.transform.position = new UnityEngine.Vector3(0, 15, 0);
				this.transform.rotation = UnityEngine.Quaternion.identity;

			}

			System.Single lx = gamepad.leftStick.x.ReadValue();
			System.Single ly = gamepad.leftStick.y.ReadValue();
			System.Single rx = gamepad.rightStick.x.ReadValue();
			System.Single ry = gamepad.rightStick.y.ReadValue();

			UnityEngine.Vector3 direction = new UnityEngine.Vector3(10 * lx, 0, 10 * ly);
			UnityEngine.Vector3 targetPoint = this.inputHelper.TransformDirection(direction);
			UnityEngine.Vector3 offsetPoint = targetPoint + this.transform.position;
			//
			UnityEngine.Vector3 point = this.transform.InverseTransformPoint(offsetPoint);
			System.Single turn = UnityEngine.Mathf.Atan2(point.x, point.z);

			System.Single face = UnityEngine.Vector3.Dot(UnityEngine.Vector3.forward, point);
			System.Single forwardy = UnityEngine.Mathf.Clamp01(face);

			//
			UnityEngine.Vector3 direction2 = new UnityEngine.Vector3(8 * rx, 0, 8 * ry);
			UnityEngine.Vector3 targetPoint2 = this.inputHelper.TransformDirection(direction2);
			UnityEngine.Vector3 offsetPoint2 = targetPoint2 + this.jeep.gun.position;
			//
			this.jeep.TurnForce = turn;
			this.jeep.DriveForce = forwardy;
			this.jeep.gunAim = offsetPoint2;

			this.aimCar.position = offsetPoint;
			this.aimGun.position = offsetPoint2;

			if (new UnityEngine.Vector3(rx, 0, ry).magnitude > 0.2) {
				this.jeep.Fire();
			}
		}

		public void Update() {
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
	}
}
namespace game {
	public class JeepController : UnityEngine.MonoBehaviour {
		private UnityEngine.Rigidbody Rigidbody;
		private UnityEngine.Transform Transform;
		public Jeep jeep;

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

		public System.Single fireDeadZone = 0.2f;
		public System.Single backupAngle = 10;

		public void Awake() {
			this.Rigidbody = this.GetComponent<UnityEngine.Rigidbody>();
			this.Transform = this.GetComponent<UnityEngine.Transform>();
			this.jeep = this.GetComponentInChildren<Jeep>();

			// make input helper
			this.inputHelper = new UnityEngine.GameObject("Input Helper").transform;
		}

		public void FixedUpdate() {

			UnityEngine.InputSystem.Gamepad gamepad = UnityEngine.InputSystem.Gamepad.current;

			if (gamepad == null) {
				return;
			}

			if (gamepad.triangleButton.isPressed) {
				this.Rigidbody.angularVelocity = UnityEngine.Vector3.zero;
				this.Rigidbody.velocity = UnityEngine.Vector3.zero;
				this.Transform.SetPositionAndRotation(new UnityEngine.Vector3(0, 15, 0), UnityEngine.Quaternion.identity);
			}

			System.Single lx = gamepad.leftStick.x.ReadValue();
			System.Single ly = gamepad.leftStick.y.ReadValue();
			System.Single rx = gamepad.rightStick.x.ReadValue();
			System.Single ry = gamepad.rightStick.y.ReadValue();

			UnityEngine.Vector3 direction = UnityEngine.Vector3.zero;
			if (new UnityEngine.Vector3(lx, 0, ly).magnitude > this.fireDeadZone) {
				direction = new UnityEngine.Vector3(10 * lx, 0, 10 * ly);
			}
			/*
			else {

				UnityEngine.Vector3 velocity = this.Rigidbody.velocity;
				UnityEngine.Vector3 reverseVelocity = new UnityEngine.Vector3(-velocity.x, -velocity.y, -velocity.z);
				reverseVelocity.Normalize();
				direction = new UnityEngine.Vector3(reverseVelocity.x, 0, reverseVelocity.y);
			}*/
			UnityEngine.Vector3 targetPoint = this.inputHelper.TransformDirection(direction);
			UnityEngine.Vector3 offsetPoint = targetPoint + this.Transform.position;
			//
			UnityEngine.Vector3 point = this.Transform.InverseTransformPoint(offsetPoint);
			System.Single turn = UnityEngine.Mathf.Atan2(point.x, point.z);

			System.Single face = UnityEngine.Vector3.Dot(UnityEngine.Vector3.forward, point);
			//System.Single forwardy = UnityEngine.Mathf.Clamp01(face);
			System.Single forwardy = 0;
			if (UnityEngine.Mathf.Abs(turn) < UnityEngine.Mathf.Deg2Rad * 90) {
				forwardy = face;
			}
			if (UnityEngine.Mathf.Abs(turn) > UnityEngine.Mathf.Deg2Rad * (180 - this.backupAngle)) {
				forwardy = face;
			}

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

			if (new UnityEngine.Vector3(rx, 0, ry).magnitude > this.fireDeadZone) {
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
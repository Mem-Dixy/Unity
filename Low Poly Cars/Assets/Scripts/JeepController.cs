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

		public Gamepad gamepad;
		public System.Single backupAngle = 10;

		public void Awake() {
			this.Rigidbody = this.GetComponent<UnityEngine.Rigidbody>();
			this.Transform = this.GetComponent<UnityEngine.Transform>();
			this.jeep = this.GetComponentInChildren<Jeep>();

			// make input helper
			this.inputHelper = new UnityEngine.GameObject("Input Helper").transform;
		}

		public void FixedUpdate() {

			if (this.gamepad.triangleButtonIsOn) {
				this.Rigidbody.angularVelocity = UnityEngine.Vector3.zero;
				this.Rigidbody.velocity = UnityEngine.Vector3.zero;
				this.Transform.SetPositionAndRotation(new UnityEngine.Vector3(0, 15, 0), UnityEngine.Quaternion.identity);
			}

			UnityEngine.Vector3 pointCar = this.InputMagic(this.gamepad.leftStickValue, this.aimCar);
			System.Single turn = UnityEngine.Mathf.Atan2(pointCar.x, UnityEngine.Mathf.Abs(pointCar.z));

			System.Single face = UnityEngine.Vector3.Dot(UnityEngine.Vector3.forward, pointCar);
			//System.Single forwardy = UnityEngine.Mathf.Clamp01(face);
			System.Single forwardy = 0;
			if (UnityEngine.Mathf.Abs(turn) < UnityEngine.Mathf.Deg2Rad * 90) {
				forwardy = face;
			}
			if (UnityEngine.Mathf.Abs(turn) > UnityEngine.Mathf.Deg2Rad * (180 - this.backupAngle)) {
				forwardy = face;
			}
			forwardy = UnityEngine.Vector3.Dot(UnityEngine.Vector3.forward, pointCar);
			//
			UnityEngine.Vector3 pointGun = this.InputMagic(this.gamepad.rightStickValue, this.aimGun);
			//
			this.jeep.SetTurn(pointCar);
			this.jeep.SetAimTurn(pointGun);
			this.jeep.DriveForce = forwardy;

			if (this.gamepad.rightStickIsOn) {
				this.jeep.Fire();
			}
		}

		public UnityEngine.Vector3 InputMagic(UnityEngine.Vector2 stick, UnityEngine.Transform aimer) {
			UnityEngine.Vector3 direction = new UnityEngine.Vector3(stick.x, 0, stick.y);
			UnityEngine.Vector3 offset = this.inputHelper.TransformDirection(direction);
			UnityEngine.Vector3 position = this.Transform.position + offset;
			//
			UnityEngine.Vector3 aimOffset = new UnityEngine.Vector3(offset.x * 10, offset.y * 10, offset.z * 10);
			aimer.position = aimOffset + this.Transform.position;
			//
			return position;
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
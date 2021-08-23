namespace game {
	public class Jeep : UnityEngine.MonoBehaviour {
		private UnityEngine.Rigidbody Rigidbody;

		public UnityEngine.Transform centerOfMass;

		public UnityEngine.Transform gun;
		public UnityEngine.Transform fire;
		public UnityEngine.Transform mordor;

		public System.Single motorTorque = 100f;
		public System.Single maxSteer = 6;
		public UnityEngine.Vector3 gunAim;

		public System.Single rateOfFire = 0.2f;
		private System.Single timeSinceFired = 0.0f;

		public System.Single turnnny;
		public System.Single turnnny2;
		public System.Single turnnny3;

		private System.Single driveForce;
		public System.Single DriveForce {
			get { return this.driveForce; }
			set {
				this.driveForce = UnityEngine.Mathf.Clamp(value, -1, 1);
			}
		}

		private System.Single turnForce;

		private Wheel[] wheel;

		private System.Single Turn(UnityEngine.Vector3 localPosition) {
			System.Single y = localPosition.x;
			System.Single f = localPosition.z;
			System.Single x = UnityEngine.Mathf.Abs(f);
			System.Single radians = UnityEngine.Mathf.Atan2(y, x);
			// maybe I want this in radians after all?
			System.Single value = UnityEngine.Mathf.Rad2Deg * radians;
			System.Single min = -this.maxSteer;
			System.Single max = +this.maxSteer;
			return UnityEngine.Mathf.Clamp(value, min, max);
		}
		public void SetTurn(UnityEngine.Vector3 localPosition) {
			this.turnForce = this.Turn(localPosition);
		}
		public void SetAimTurn(UnityEngine.Vector3 localPosition) {
			this.turnForce = this.Turn(localPosition);
		}
		public void Awake() {
			this.centerOfMass = this.transform;
			this.wheel = this.GetComponentsInChildren<Wheel>();
			this.Rigidbody = this.transform.GetComponent<UnityEngine.Rigidbody>();
			//UnityEngine.GameObject gameObject = new UnityEngine.GameObject("Center Of Mass");
			//this.centerOfMass = Instantiate(gameObject, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, this.transform).transform;
			this.Rigidbody.centerOfMass = this.centerOfMass.localPosition;
		}
		public void Start() {
			this.maxSteer = UnityEngine.Mathf.Clamp(UnityEngine.Mathf.Abs(this.maxSteer), 0, 90);
			this.motorTorque = UnityEngine.Mathf.Abs(this.motorTorque);
		}

		public void FixedUpdate() {
			System.Single a = this.turnnny;
			System.Single b = this.turnnny2;
			System.Single t = this.turnnny3;
			System.Single d = UnityEngine.Mathf.LerpAngle(a, b, t);
			//UnityEngine.Debug.Log(d);
			foreach (Wheel wheel in this.wheel) {
				if (wheel.drive) {
				}
				if (wheel.turn) {
					wheel.WheelCollider.steerAngle = this.turnForce * this.maxSteer;
					// -90 to 0 to +90
					// -360 to 0 to 360
					wheel.WheelCollider.steerAngle = this.turnnny;
				}
			}
			UnityEngine.Vector3 aimGun = this.transform.InverseTransformDirection(this.gunAim);
			UnityEngine.Vector3 relativeUp = this.transform.TransformDirection(UnityEngine.Vector3.up);
			this.gun.LookAt(aimGun, relativeUp);
		}

		public void Update() {
			this.timeSinceFired -= UnityEngine.Time.deltaTime;
		}
		public void Fire() {
			if (this.timeSinceFired <= 0) {
				this.timeSinceFired = this.rateOfFire;
				_ = Instantiate(this.mordor, this.fire.position, this.fire.rotation);
			}
		}
	}
}
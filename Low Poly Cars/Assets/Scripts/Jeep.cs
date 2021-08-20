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

		private System.Single driveForce;
		public System.Single DriveForce {
			get { return this.driveForce; }
			set {
				this.driveForce = UnityEngine.Mathf.Clamp(value, -1, 1);
			}
		}

		private System.Single turnForce;

		private Wheel[] wheel;
		public System.Single TurnForce {
			get { return this.turnForce; }
			set {
				this.turnForce = UnityEngine.Mathf.Clamp(value, -1, 1);
			}
		}

		public void Awake() {
			this.centerOfMass = this.transform;
			this.wheel = this.GetComponentsInChildren<Wheel>();
			this.Rigidbody = this.transform.GetComponent<UnityEngine.Rigidbody>();
			//UnityEngine.GameObject gameObject = new UnityEngine.GameObject("Center Of Mass");
			//this.centerOfMass = Instantiate(gameObject, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, this.transform).transform;
			this.Rigidbody.centerOfMass = this.centerOfMass.localPosition;
		}
		public void FixedUpdate() {
			foreach (Wheel wheel in this.wheel) {
				if (wheel.drive) {
					wheel.WheelCollider.motorTorque = this.driveForce * this.motorTorque;
				}
				if (wheel.turn) {
					wheel.WheelCollider.steerAngle = this.turnForce * this.maxSteer;
				}
			}
			UnityEngine.Vector3 relativeUp = this.transform.TransformDirection(UnityEngine.Vector3.up);
			this.gun.LookAt(this.gunAim, relativeUp);
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
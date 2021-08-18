namespace game {
	public class Jeep : UnityEngine.MonoBehaviour {
		public UnityEngine.Transform centerOfMass;

		public UnityEngine.WheelCollider wheelColliderLeftFront;
		public UnityEngine.WheelCollider wheelColliderLeftBack;
		public UnityEngine.WheelCollider wheelColliderRightFront;
		public UnityEngine.WheelCollider wheelColliderRightBack;

		public UnityEngine.Transform wheelLeftFront;
		public UnityEngine.Transform wheelRightFront;
		public UnityEngine.Transform wheelLeftBack;
		public UnityEngine.Transform wheelRightBack;
		public UnityEngine.Transform gun;
		public UnityEngine.Transform mordor;

		public System.Single motorTorque = 100f;
		public System.Single maxSteer = 6;
		public UnityEngine.Vector3 gunAim;

		public UnityEngine.Rigidbody _rigidbody;

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
		public System.Single TurnForce {
			get { return this.turnForce; }
			set {
				this.turnForce = UnityEngine.Mathf.Clamp(value, -1, 1);
			}
		}

		private void Start() {
			this._rigidbody = this.GetComponent<UnityEngine.Rigidbody>();
			this._rigidbody.centerOfMass = this.centerOfMass.localPosition;
		}

		private void FixedUpdate() {
			this.wheelColliderLeftBack.motorTorque = this.driveForce * this.motorTorque;
			this.wheelColliderRightBack.motorTorque = this.driveForce * this.motorTorque;
			this.wheelColliderLeftFront.steerAngle = this.turnForce * this.maxSteer;
			this.wheelColliderRightFront.steerAngle = this.turnForce * this.maxSteer;
			UnityEngine.Vector3 relativeUp = this.transform.TransformDirection(UnityEngine.Vector3.up);
			this.gun.LookAt(this.gunAim, relativeUp);
		}

		public void Update() {
			this.TurnWheel(this.wheelColliderLeftFront, this.wheelLeftFront);
			this.TurnWheel(this.wheelColliderRightFront, this.wheelRightFront);
			this.TurnWheel(this.wheelColliderLeftBack, this.wheelLeftBack);
			this.TurnWheel(this.wheelColliderRightBack, this.wheelRightBack);
			this.timeSinceFired -= UnityEngine.Time.deltaTime;
		}

		private void TurnWheel(UnityEngine.WheelCollider collider, UnityEngine.Transform transform) {
			collider.GetWorldPose(out UnityEngine.Vector3 pos, out UnityEngine.Quaternion quat);
			transform.position = pos;
			transform.rotation = quat;
		}

		public void Fire() {
			if (this.timeSinceFired <= 0) {
				this.timeSinceFired = this.rateOfFire;
				_ = UnityEngine.Object.Instantiate(this.mordor, this.gun.position, this.gun.rotation);
			}
		}
	}
}
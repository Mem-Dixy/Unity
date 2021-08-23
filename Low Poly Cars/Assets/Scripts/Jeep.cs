namespace game {
	public class Jeep : UnityEngine.MonoBehaviour {
		private UnityEngine.Rigidbody Rigidbody;

		public UnityEngine.Transform centerOfMass;

		public UnityEngine.Transform gun;
		public UnityEngine.Transform fire;
		public UnityEngine.Transform mordor;

		public System.Single motorTorque = 100f;

		public System.Single turnAngle = 40.0f;
		public System.Single turnRate = 1.0f;
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

		private System.Single carTurn;
		private System.Single gunTurn;

		private Wheel[] wheel;

		public void SetTurn(UnityEngine.Vector3 position) {
			UnityEngine.Vector3 localPosition = this.transform.InverseTransformPoint(position);
			System.Single y = localPosition.x;
			System.Single f = localPosition.z;
			System.Single x = UnityEngine.Mathf.Abs(f);
			System.Single radians = UnityEngine.Mathf.Atan2(y, x);
			// maybe I want this in radians after all?
			System.Single value = UnityEngine.Mathf.Rad2Deg * radians;
			System.Single min = -this.turnAngle;
			System.Single max = +this.turnAngle;
			System.Single result = UnityEngine.Mathf.Clamp(value, min, max);
			this.carTurn = result;
		}
		public void SetAimTurn(UnityEngine.Vector3 position) {
			UnityEngine.Vector3 localPosition = this.transform.InverseTransformPoint(position);
			System.Single y = localPosition.x;
			System.Single x = localPosition.z;
			System.Single radians = UnityEngine.Mathf.Atan2(y, x);
			// maybe I want this in radians after all?
			System.Single value = UnityEngine.Mathf.Rad2Deg * radians;
			System.Single min = -180;
			System.Single max = +180;
			System.Single result = UnityEngine.Mathf.Clamp(value, min, max);
			this.gunTurn = result;
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
			this.turnAngle = UnityEngine.Mathf.Clamp(UnityEngine.Mathf.Abs(this.turnAngle), 0, 90);
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
					System.Single currentAngle = wheel.WheelCollider.steerAngle;
					System.Single deltaAngle = UnityEngine.Mathf.DeltaAngle(currentAngle, this.carTurn);
					System.Single nextAngle = UnityEngine.Mathf.Clamp(deltaAngle, -this.turnRate, +this.turnRate);
					System.Single putAngle = currentAngle + nextAngle;
					wheel.WheelCollider.steerAngle = putAngle;
				}
			}
			this.gun.eulerAngles = new UnityEngine.Vector3(0, this.gunTurn, 0);
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
namespace game {
	public class Jeep : UnityEngine.MonoBehaviour {
		private UnityEngine.Rigidbody Rigidbody;

		public UnityEngine.Transform centerOfMass;

		public UnityEngine.Transform gun;
		public UnityEngine.Transform fire;
		public UnityEngine.Transform mordor;

		public System.Single motorTorque = 2000.0f;
		public System.Single motorAcceleration = 1000.0f;

		public System.Single turnAngle = 40.0f;
		public System.Single turnRate = 90.0f;
		public UnityEngine.Vector3 gunAim;

		public System.Single rateOfFire = 0.2f;
		private System.Single timeSinceFired = 0.0f;

		public System.Single turnnny;
		public System.Single turnnny2;
		public System.Single turnnny3;

		private System.Single driveForce;
		private System.Single carTurn;
		private System.Single gunTurn;

		private Wheel[] wheel;

		public void SetDrive(UnityEngine.Vector3 position) {
			UnityEngine.Vector3 localPosition = this.transform.InverseTransformPoint(position);
			System.Single dot = UnityEngine.Vector3.Dot(UnityEngine.Vector3.forward, localPosition);
			System.Single value = UnityEngine.Mathf.Clamp(dot, -1, +1) * this.motorTorque;
			System.Single min = -this.motorTorque;
			System.Single max = +this.motorTorque;
			System.Single result = UnityEngine.Mathf.Clamp(value, min, max);
			this.driveForce = result;
		}
		public void SetTurn(UnityEngine.Vector3 position) {
			UnityEngine.Vector3 localPosition = this.transform.InverseTransformPoint(position);
			System.Single y = localPosition.x;
			System.Single f = localPosition.z;
			System.Single x = UnityEngine.Mathf.Abs(f);
			System.Single radians = UnityEngine.Mathf.Atan2(y, x);
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
			foreach (Wheel wheel in this.wheel) {
				if (wheel.drive) {
					System.Single current = wheel.WheelCollider.motorTorque;
					System.Single target = this.driveForce;
					System.Single maxDelta = this.motorAcceleration * UnityEngine.Time.fixedDeltaTime;
					System.Single result = UnityEngine.Mathf.MoveTowards(current, target, maxDelta);
					wheel.WheelCollider.motorTorque = result;
				}
				if (wheel.turn) {
					System.Single current = wheel.WheelCollider.steerAngle;
					System.Single target = this.carTurn;
					System.Single maxDelta = this.turnRate * UnityEngine.Time.fixedDeltaTime;
					System.Single angle = UnityEngine.Mathf.MoveTowardsAngle(current, target, maxDelta);
					wheel.WheelCollider.steerAngle = angle;
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
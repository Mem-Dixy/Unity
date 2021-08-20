namespace game {
	public class Wheel : UnityEngine.MonoBehaviour {
		private UnityEngine.Transform Transform;
		public UnityEngine.WheelCollider WheelCollider { get; private set; }
		[UnityEngine.SerializeField]
		private UnityEngine.Transform wheelMesh;
		public System.Boolean drive;
		public System.Boolean turn;
		public void Awake() {
			this.Transform = this.GetComponent<UnityEngine.Transform>();
			this.WheelCollider = this.GetComponent<UnityEngine.WheelCollider>();
		}
		public void Start() {
			this.Transform.SetPositionAndRotation(this.wheelMesh.position, this.wheelMesh.rotation);
		}
		public void Update() {
			this.WheelCollider.GetWorldPose(out UnityEngine.Vector3 pos, out UnityEngine.Quaternion quat);
			this.wheelMesh.SetPositionAndRotation(pos, quat);
		}
	}
}
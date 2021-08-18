namespace game {
	public class Wheel : UnityEngine.MonoBehaviour {
		public UnityEngine.WheelCollider WheelCollider { get; private set; }
		[UnityEngine.SerializeField]
		private UnityEngine.Transform wheelMesh;
		public void Awake() {
			this.WheelCollider = this.GetComponent<UnityEngine.WheelCollider>();
		}
		public void Update() {
			this.WheelCollider.GetWorldPose(out UnityEngine.Vector3 pos, out UnityEngine.Quaternion quat);
			this.wheelMesh.SetPositionAndRotation(pos, quat);
		}
	}
}
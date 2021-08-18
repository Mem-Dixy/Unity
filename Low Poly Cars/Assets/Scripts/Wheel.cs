namespace game {
	public class Wheel : UnityEngine.MonoBehaviour {
		[UnityEngine.SerializeField]
		private UnityEngine.Transform wheel;

		public UnityEngine.WheelCollider WheelCollider { get; private set; }

		public void Start() {
			this.WheelCollider = this.GetComponent<UnityEngine.WheelCollider>() as UnityEngine.WheelCollider;
		}
		public void Update() {
			this.WheelCollider.GetWorldPose(out UnityEngine.Vector3 pos, out UnityEngine.Quaternion quat);
			this.wheel.transform.position = pos;
			this.wheel.transform.rotation = quat;
		}
	}
}
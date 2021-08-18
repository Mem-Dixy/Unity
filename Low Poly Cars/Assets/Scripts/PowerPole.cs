namespace game {
	public class PowerPole : UnityEngine.MonoBehaviour {
		public void OnCollisionEnter() {
			UnityEngine.Rigidbody rigidbody = this.GetComponent<UnityEngine.Rigidbody>() as UnityEngine.Rigidbody;
			rigidbody.isKinematic = false;
		}
	}
}
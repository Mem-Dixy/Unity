public class PowerPole : UnityEngine.MonoBehaviour {
	private void OnCollisionEnter() {
		UnityEngine.Rigidbody rigidbody = this.GetComponent<UnityEngine.Rigidbody>() as UnityEngine.Rigidbody;
		rigidbody.isKinematic = false;
	}
}

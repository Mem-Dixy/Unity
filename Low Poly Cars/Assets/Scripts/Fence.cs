public class Fence : UnityEngine.MonoBehaviour {
	public UnityEngine.GameObject destroyedVersion;

	private void OnCollisionEnter() {
		_ = UnityEngine.Object.Instantiate(this.destroyedVersion, this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);

	}
}

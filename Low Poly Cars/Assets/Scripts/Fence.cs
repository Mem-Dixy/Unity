namespace game {
	public class Fence : UnityEngine.MonoBehaviour {
		private System.Boolean failsafe = false;
		public UnityEngine.GameObject destroyedVersion;
		public void OnCollisionEnter(UnityEngine.Collision Collision) {
			if (this.failsafe) {
				return;
			}
			this.failsafe = true;
			UnityEngine.Vector3 force = new UnityEngine.Vector3(Collision.relativeVelocity.x * 5, Collision.relativeVelocity.y * 5, Collision.relativeVelocity.z * 5);
			UnityEngine.GameObject GameObject = Instantiate(this.destroyedVersion, this.transform.position, this.transform.rotation);
			UnityEngine.Rigidbody[] Rigidbodys = GameObject.GetComponentsInChildren<UnityEngine.Rigidbody>();
			foreach (UnityEngine.Rigidbody Rigidbody in Rigidbodys) {
				Rigidbody.velocity = force;
			}
			Destroy(this.gameObject);
		}
	}
}
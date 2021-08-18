public class Bullet : UnityEngine.MonoBehaviour {
	public System.Single timedLife = 20;
	public System.Single speed = 25;

	private void FixedUpdate() {
		this.transform.Translate(0, 0, this.speed * UnityEngine.Time.deltaTime);
	}
	// Update is called once per frame
	private void Update() {
		this.timedLife -= UnityEngine.Time.deltaTime;
		if (this.timedLife < 0) {
			Destroy(this.gameObject);
		}
	}
}

public class CameraFollow : UnityEngine.MonoBehaviour {

	public UnityEngine.Transform Target;

	public UnityEngine.Vector3 offset;
	public UnityEngine.Vector3 eulerRotation;
	public System.Int32 damper;

	private void Start() {
		this.transform.eulerAngles = this.eulerRotation;
	}

	// Update is called once per frame
	private void Update() {
		if (this.Target == null) {
			return;
		}
		this.transform.position = UnityEngine.Vector3.Lerp(this.transform.position, this.Target.position + this.offset, this.damper * (UnityEngine.Time.deltaTime * 4));
	}
}

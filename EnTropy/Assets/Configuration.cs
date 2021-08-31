namespace EnTropy {
	public class Configuration : UnityEngine.MonoBehaviour {
		public static Configuration self;
		public static UnityEngine.Vector2Int ButtonPosition;
		public static UnityEngine.Vector2Int ButtonSize;
		public UnityEngine.Vector2Int buttonPosition;
		public UnityEngine.Vector2Int buttonSize;
		public void Awake() {
			this.buttonPosition = new UnityEngine.Vector2Int(100, 40);
			this.buttonSize = new UnityEngine.Vector2Int(100, 30);
			self = this;
			ButtonPosition = this.buttonPosition;
			ButtonSize = this.buttonSize;
		}
	}
}
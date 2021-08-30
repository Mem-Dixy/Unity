namespace EnTropy {
	public class Damage : UnityEngine.MonoBehaviour {
		public System.Int32[] damage;
		public System.Int32[] range;
		public System.Int32 index;
		public UnityEngine.Vector2Int position;
		public UnityEngine.Vector2Int size;
		public void Start() {
			this.position = new UnityEngine.Vector2Int(100, 40);
			this.size = new UnityEngine.Vector2Int(100, 30);
			System.Int32 index = 31;
			this.damage = new System.Int32[index];
			while (index-- > 0) {
				this.damage[index] = index * 10;
			}
			index = 9;
			this.range = new System.Int32[index];
			while (index-- > 0) {
				this.range[index] = index * 10;
			}
		}

		private void OnGUI() {
			this.ButtonArray(this.damage, 0, 0);
			this.ButtonArray(this.range, 1, 0);
		}
		public void Update() {

		}

		private delegate void Empty();

		private void Button(Empty empty) {
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(0, 0, 120, 240), "Unpause")) {
				empty.Invoke();
			}
		}
		private void ButtonArray(System.Int32[] array, System.Int32 offset_x = 0, System.Int32 offset_y = 0) {
			System.Int32 index = array.Length;
			while (index-- > 0) {
				if (UnityEngine.GUI.Button(new UnityEngine.Rect(offset_x * this.size.x, (offset_y + index) * this.size.y, this.size.x, this.size.y), array[index].ToString())) {
					this.index = index;
				}
			}
		}
	}
}
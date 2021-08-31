namespace EnTropy {
	public class Damage : UnityEngine.MonoBehaviour {
		public System.Int32[] damage;
		public System.Int32[] range;
		public System.Int32 damageIndex;
		public System.Int32 rangeIndex;
		public TowerData[] towers;
		private Configuration skin;
		public void Start() {
			this.skin = Configuration.self;
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
			this.towers = TowerMaker.Make();
		}
		private void OnGUI() {
			this.ButtonArray(this.damage, 0, 0, ref this.damageIndex);
			this.ButtonArray(this.range, 1, 0, ref this.rangeIndex);
			System.Int32 index = this.towers.Length;
			while (index-- > 0) {
				if (UnityEngine.GUI.Button(new UnityEngine.Rect(2 * this.skin.buttonSize.x, (0 + index) * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), this.towers[index].name)) {
					this.MakeTower(this.towers[index]);
				}
			}
		}
		public void Update() {

		}

		private delegate void Empty();

		private void Button(Empty empty) {
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(0, 0, 120, 240), "Unpause")) {
				empty.Invoke();
			}
		}
		private void ButtonArray(System.Int32[] array, System.Int32 offset_x, System.Int32 offset_y, ref System.Int32 arrayIndex) {
			System.Int32 index = array.Length;
			while (index-- > 0) {
				if (UnityEngine.GUI.Button(new UnityEngine.Rect(offset_x * this.skin.buttonSize.x, (offset_y + index) * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), array[index].ToString())) {
					arrayIndex = index;
				}
			}
		}
		private void MakeTower(TowerData towerData) {
			UnityEngine.GameObject tower = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
			tower.name = towerData.name;
			TowerObject towerObject = tower.AddComponent(typeof(TowerObject)) as TowerObject;
			towerObject.damage = this.damage[this.damageIndex];
			towerObject.range = this.range[this.rangeIndex];
			tower.transform.position = new UnityEngine.Vector3(0, 1, 2);
		}
	}
}
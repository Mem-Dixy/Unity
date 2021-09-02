namespace EnTropy {
	public class Damage : UnityEngine.MonoBehaviour {
		public System.Int32 maxDamageUpgrade = 41 * 4;
		public System.Int32 maxSpeedUpgrade = 41;

		public System.Int32[] damage;
		public System.Int32[] range;
		public System.Int32 damageIndex;
		public System.Int32 rangeIndex;
		public TowerData[] towers;
		public TowerObject[,] towerObject;
		private Configuration skin;
		public void Start() {
			this.towerObject = new TowerObject[0, 0];
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
			//
			this.towerObject = new TowerObject[this.maxDamageUpgrade, this.maxSpeedUpgrade];
			System.Int32 index_x = this.maxDamageUpgrade;
			while (index_x-- > 0) {
				System.Int32 index_y = this.maxSpeedUpgrade;
				while (index_y-- > 0) {
					this.towerObject[index_x, index_y] = this.NewTowerObject(index_x, index_y);
				}
			}
		}
		private void OnGUI() {
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(8 * this.skin.buttonSize.x, 1 * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), "Show Ratio")) {
				foreach (TowerObject towerObject in this.towerObject) {
					towerObject.ShowRatio();
				}
			}
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(8 * this.skin.buttonSize.x, 2 * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), "Show Damage")) {
				foreach (TowerObject towerObject in this.towerObject) {
					towerObject.ShowDamage();
				}
			}
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(8 * this.skin.buttonSize.x, 3 * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), "Show Cost")) {
				foreach (TowerObject towerObject in this.towerObject) {
					towerObject.ShowCost();
				}
			}
			this.ButtonArray(this.damage, 0, 0, ref this.damageIndex);
			this.ButtonArray(this.range, 1, 0, ref this.rangeIndex);
			System.Int32 index = this.towers.Length;
			while (index-- > 0) {
				if (UnityEngine.GUI.Button(new UnityEngine.Rect(2 * this.skin.buttonSize.x, (0 + index) * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), this.towers[index].name)) {
					System.Int32 x = 41*4;
					while (x-->0) {
						System.Int32 y = 41;
						while (y-- > 0) {
							this.towerObject[x, y].SetTower(this.towers[index]);
						}
					}
				}
			}
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
		private TowerObject NewTowerObject(System.Int32 damageUpgrade, System.Int32 speedUpgrade) {
			UnityEngine.GameObject tower = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
			TowerObject towerObject = tower.AddComponent(typeof(TowerObject)) as TowerObject;
			towerObject.Constructor(damageUpgrade, speedUpgrade);
			return towerObject;
		}
	}
}
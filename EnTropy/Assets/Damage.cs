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
		}
		private void OnGUI() {
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(8 * this.skin.buttonSize.x, 0 * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), "Spawn Tower")) {
				this.SpawnTower();
			}
			//
			this.ButtonArray(this.damage, 0, 0, ref this.damageIndex);
			this.ButtonArray(this.range, 1, 0, ref this.rangeIndex);
			System.Int32 index = this.towers.Length;
			while (index-- > 0) {
				if (UnityEngine.GUI.Button(new UnityEngine.Rect(2 * this.skin.buttonSize.x, (0 + index) * this.skin.buttonSize.y, this.skin.buttonSize.x, this.skin.buttonSize.y), this.towers[index].name)) {
					System.Int32 x = 41*4;
					while (x-->0) {
						System.Int32 y = 41;
						while (y-- > 0) {
							this.MakeTower(this.towers[index], new UnityEngine.Vector2Int(x, y));
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
		private void MakeTower(TowerData towerData, UnityEngine.Vector2Int spot) {
			UnityEngine.GameObject tower = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
			tower.name = towerData.name;
			TowerObject towerObject = tower.AddComponent(typeof(TowerObject)) as TowerObject;
			towerObject.damage = this.damage[this.damageIndex];
			towerObject.range = this.range[this.rangeIndex];
			towerObject.damageUpgrade = spot.x;
			towerObject.speedUpgrade = spot.y;
			towerObject.towerData = towerData;
			tower.transform.position = new UnityEngine.Vector3(spot.x, 0, spot.y);
			towerObject.Work();
		}
		private void SpawnTower() {
			this.KillTower();
			this.towerObject = new TowerObject[this.maxDamageUpgrade, this.maxSpeedUpgrade];
			System.Int32 index_x = this.maxDamageUpgrade;
			while (index_x-- > 0) {
				System.Int32 index_y = this.maxSpeedUpgrade;
				while (index_y-- > 0) {
					this.towerObject[index_x, index_y] = this.NewTowerObject();
				}
			}
		}
		private void KillTower() {
			foreach (TowerObject tower in this.towerObject) {
				Destroy(tower.gameObject);
			}
		}
		private TowerObject NewTowerObject() {
			UnityEngine.GameObject tower = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
			TowerObject towerObject = tower.AddComponent(typeof(TowerObject)) as TowerObject;
			towerObject.Reset();
			return towerObject;
		}
	}
}
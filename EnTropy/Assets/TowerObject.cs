namespace EnTropy {
	public class TowerObject : UnityEngine.MonoBehaviour {
		public System.Int32 damage;
		public System.Int32 range;
		private System.Int32 damageUpgrade;
		private System.Int32 speedUpgrade;
		private TowerData towerData;

		private System.Single cost;
		private System.Single firedamage;

		private UnityEngine.Transform Transform;
		private UnityEngine.Material m_Material;

		public void Start() {
			this.m_Material = this.GetComponent<UnityEngine.Renderer>().material;
			this.SetColor(0.0f);
			this.Transform = this.GetComponent<UnityEngine.Transform>();
		}

		public void Constructor(System.Int32 damageUpgrade, System.Int32 speedUpgrade) {
			this.damageUpgrade = damageUpgrade;
			this.speedUpgrade = speedUpgrade;
			//
			this.m_Material = this.GetComponent<UnityEngine.Renderer>().material;
			this.SetColor(0.0f);
			this.Transform = this.GetComponent<UnityEngine.Transform>();
			this.Transform.position = new UnityEngine.Vector3(this.damageUpgrade, 0, this.speedUpgrade);
			this.SetColor(0.0f);
			//
			System.Single cost = 1;
			System.Int32 index = this.damageUpgrade;
			while (index-- > 0) {
				cost += index + 1;
			}
			index = this.speedUpgrade;
			while (index-- > 0) {
				cost += index + 1;
			}
			this.cost = cost;
		}
		public void SetTower(TowerData towerData) {
			this.towerData = towerData;
			this.name = towerData.name;
			System.Single ratio = -0.058186f;
			System.Single firerate = UnityEngine.Mathf.Exp(this.speedUpgrade * ratio) * this.towerData.weaponSpeed;
			System.Single firedamage = this.damageUpgrade * this.towerData.damage / firerate;
			this.firedamage = firedamage;
		}

		public void ShowRatio() {
			this.Transform.position = new UnityEngine.Vector3(this.damageUpgrade, this.firedamage / this.cost * 1000, this.speedUpgrade);
			this.SetColor(this.firedamage / 255.0f);
		}
		public void ShowDamage() {
			this.Transform.position = new UnityEngine.Vector3(this.damageUpgrade, this.firedamage / 10, this.speedUpgrade);
			this.SetColor(this.firedamage / 255.0f);
		}
		public void ShowCost() {
			this.Transform.position = new UnityEngine.Vector3(this.damageUpgrade, this.cost / 100, this.speedUpgrade);
		}

		private void SetColor(System.Single green) {
			System.Single r = this.damageUpgrade / 163.0f;
			System.Single g = green;
			System.Single b = this.speedUpgrade / 40.0f;
			System.Single a = 1.0f;
			this.m_Material.color = new UnityEngine.Color(r, g, b, a);
		}

		void OnDestroy() {
			Destroy(this.m_Material);
		}
	}
}
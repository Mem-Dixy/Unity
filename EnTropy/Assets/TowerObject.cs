namespace EnTropy {
	public class TowerObject : UnityEngine.MonoBehaviour {
		public System.Int32 damage;
		public System.Int32 range;
		public System.Int32 damageUpgrade;
		public System.Int32 speedUpgrade;
		public TowerData towerData;
		private Configuration skin;
		public void Work() {
			System.Single cost = 1;
			System.Int32 index = this.damageUpgrade;
			while (index-- > 0) {
				cost += index + 1;
			}
			index = this.speedUpgrade;
			while (index-- > 0) {
				cost += index + 1;
			}
			this.skin = Configuration.self;
			System.Single ratio = -0.058186f;
			System.Single firerate = UnityEngine.Mathf.Exp(this.speedUpgrade * ratio) * this.towerData.weaponSpeed;
			System.Single firedamage = this.damageUpgrade * this.towerData.damage / firerate;
			this.transform.position = new UnityEngine.Vector3(this.damageUpgrade, firedamage/cost * 1000, this.speedUpgrade);

			UnityEngine.GameObject tower = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere);
			tower.name = this.towerData.name;
			tower.transform.position = new UnityEngine.Vector3(this.damageUpgrade, firedamage / 10, this.speedUpgrade);

			UnityEngine.GameObject tower1 = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cylinder);
			tower1.name = this.towerData.name;
			tower1.transform.position = new UnityEngine.Vector3(this.damageUpgrade, cost / 100, this.speedUpgrade);
		}
	}
}
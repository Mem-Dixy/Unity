namespace EnTropy {
	public class TowerObject : UnityEngine.MonoBehaviour {
		public System.Int32 damage;
		public System.Int32 range;
		private Configuration skin;
		public void Start() {
			this.skin = Configuration.self;
		}
	}
}
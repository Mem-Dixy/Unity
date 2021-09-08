namespace EnTropy {
	public class UpgradeLife : Upgrade {
		private const System.String NAME = "Wave Life Upgrade";
		private const System.Byte LIMIT = 30;
		public System.Int32[] array;
		public UpgradeLife() : base(NAME, LIMIT) {
			this.array = new System.Int32[LIMIT];
		}
	}
}
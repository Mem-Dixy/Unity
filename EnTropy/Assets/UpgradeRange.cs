namespace EnTropy {
	public class UpgradeRange : Upgrade {
		private const System.String NAME = "Wave Range Upgrade";
		private const System.Byte LIMIT = 120;
		public System.Int32[] array;
		public UpgradeRange() : base(NAME, LIMIT) {
			this.array = new System.Int32[LIMIT];
		}
	}
}
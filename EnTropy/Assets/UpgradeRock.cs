namespace EnTropy {
	public class UpgradeRock : Upgrade {
		private const System.String NAME = "Rock Break";
		private const System.Byte LIMIT = 255;
		public System.Int32[] array;
		public UpgradeRock() : base(NAME, LIMIT) {
			this.array = new System.Int32[LIMIT];
		}
	}
}
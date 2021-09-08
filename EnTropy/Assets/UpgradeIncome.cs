namespace EnTropy {
	public class UpgradeIncome : Upgrade {
		private const System.String NAME = "Bonus Income Upgrade";
		private const System.Byte LIMIT = 120;
		public System.Int32[] array;
		public UpgradeIncome() : base(NAME, LIMIT) {
			this.array = new System.Int32[LIMIT];
		}
	}
}
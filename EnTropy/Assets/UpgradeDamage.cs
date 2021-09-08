namespace EnTropy {
	public class UpgradeDamage : Upgrade {
		private const System.String NAME = "Wave Damage Upgrade";
		private const System.Byte LIMIT = 120;
		public System.Int32[] array;
		public UpgradeDamage() : base(NAME, LIMIT) {
			this.array = new System.Int32[LIMIT];
		}
	}
}
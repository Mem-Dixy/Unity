namespace EnTropy {
	public abstract class Upgrade {
		protected readonly System.String name;
		protected readonly System.Byte limit;

		public System.UInt16 level;
		public System.Single addition;
		public System.Single multiplication;
		public System.Single exponentiation;

		public Upgrade(System.String name) {
			this.name = name;
		}
		public Upgrade(System.String name, System.Byte limit) {
			this.name = name;
			this.limit = limit;
		}
		public void Constructor(System.Single addition, System.Single multiplication, System.Single exponentiation) {
			this.level = 0;
			this.addition = addition;
			this.multiplication = multiplication;
			this.exponentiation = exponentiation;
		}
		public void Convert(System.String addition, System.String multiplication, System.String exponentiation) {
			this.level = 0;
			_ = System.Single.TryParse(addition, out this.addition);
			_ = System.Single.TryParse(multiplication, out this.multiplication);
			_ = System.Single.TryParse(exponentiation, out this.exponentiation);
		}
		public System.Single Calculate(System.Int32 level) {
			return (System.Single)System.Math.Pow((level + this.addition) * this.multiplication, this.exponentiation);
		}
	}
}
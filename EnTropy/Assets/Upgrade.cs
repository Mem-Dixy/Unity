namespace EnTropy {
	public abstract class Upgrade {
		protected readonly System.String name;
		protected readonly System.Byte limit;

		public System.UInt16 level;
		public System.Single addition;
		public System.Single multiplication;
		public System.Single exponentiation;

		public System.UInt32[] array;
		public System.UInt32[] array2;

		public Upgrade(System.String name, System.Byte limit) {
			this.name = name;
			this.limit = (System.Byte)(limit + 1);
			this.array = new System.UInt32[this.limit];
			this.array2 = new System.UInt32[this.limit];
		}
		private void Array() {
			this.array[0] = 0;
			this.array2[0] = 0;
			for (System.Int32 index = 1; index < this.limit; index += 1) {
				System.UInt32 item = this.Calculate(index - 1);
				this.array[index - 1] = item;
				this.array2[index] = this.array2[index - 1] + item;
			}
		}
		public void Constructor(System.Single addition, System.Single multiplication, System.Single exponentiation) {
			this.level = 0;
			this.addition = addition;
			this.multiplication = multiplication;
			this.exponentiation = exponentiation;
			this.Array();
		}
		public void Convert(System.String addition, System.String multiplication, System.String exponentiation) {
			this.level = 0;
			_ = System.Single.TryParse(addition, out this.addition);
			_ = System.Single.TryParse(multiplication, out this.multiplication);
			_ = System.Single.TryParse(exponentiation, out this.exponentiation);
			this.Array();
		}
		public System.UInt32 Calculate(System.Int32 level) {
			System.Double pow = System.Math.Pow((level + this.addition) * this.multiplication, this.exponentiation);
			System.Double round = System.Math.Round(pow);
			System.UInt32 result = (System.UInt32)round;
			return result;
		}
	}
}
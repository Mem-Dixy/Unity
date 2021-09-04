namespace EnTropy {
	public class Upgrade {
		public readonly System.String name;
		public readonly System.Int32 limit;

		public System.Single currentUpgrade;
		public System.Single plusThisMuch;
		public System.Single timesThisMuch;
		public System.Single toThisPower;

		private System.String value0;
		private System.String value1;
		private System.String value2;
		private System.String value3;
		private System.String value4;
		public Upgrade(System.String name, System.Int32 limit) {
			this.name = name;
			this.limit = limit;
		}
		public void Constructor(System.Single addition, System.Single multiplication, System.Single exponentiation) {
			this.plusThisMuch = addition;
			this.timesThisMuch = multiplication;
			this.toThisPower = exponentiation;
		}
		public static Upgrade WaveDamage() {
			return new Upgrade("Wave Damage Upgrade", 120);
		}
		public static Upgrade WaveRange() {
			return new Upgrade("Range Upgrade", 120);
		}
		public static Upgrade WaveLife() {
			return new Upgrade("Wave Life Upgrade", 30);
		}
		public static Upgrade WaveIncome() {
			return new Upgrade("Income", 120);
		}
		public void OnGUI(UnityEngine.Rect position) {
			UnityEngine.GUI.BeginGroup(position);
			UnityEngine.GUI.Label(new UnityEngine.Rect(0, 0, 250, 25), this.name);
			this.value0 = UnityEngine.GUI.TextField(new UnityEngine.Rect(150, 0, 50, 25), this.value0);
			this.value1 = UnityEngine.GUI.TextField(new UnityEngine.Rect(200, 0, 50, 25), this.value1);
			this.value2 = UnityEngine.GUI.TextField(new UnityEngine.Rect(250, 0, 50, 25), this.value2);
			this.value3 = UnityEngine.GUI.TextField(new UnityEngine.Rect(300, 0, 50, 25), this.value3);
			if (UnityEngine.GUI.Button(new UnityEngine.Rect(350, 0, 100, 25), "Calculate")) {
				_ = System.Single.TryParse(this.value0, out this.currentUpgrade);
				_ = System.Single.TryParse(this.value1, out this.plusThisMuch);
				_ = System.Single.TryParse(this.value2, out this.timesThisMuch);
				_ = System.Single.TryParse(this.value3, out this.toThisPower);
				System.Double answer = System.Math.Pow((this.currentUpgrade + this.plusThisMuch)*this.timesThisMuch, this.toThisPower);
				this.value4 = System.Math.Round(answer).ToString();
			}
			UnityEngine.GUI.Label(new UnityEngine.Rect(450, 0, 350, 25), this.value4);
			UnityEngine.GUI.EndGroup();
		}
	}
}
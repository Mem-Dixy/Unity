namespace EnTropy {
	public struct TowerData {
		public readonly System.String name;
		public readonly System.Single range;
		public readonly System.Single damage;
		public readonly Difficulty difficulty;
		public readonly Targets targets;
		public readonly System.Byte attacks;
		public readonly System.Byte weaponSpeed;
		public readonly System.String description;
		public TowerData(
			System.String name,
			System.Single range,
			System.Single damage,
			Difficulty difficulty,
			Targets targets,
			System.Byte attacks,
			System.Byte weaponSpeed,
			System.String description
		) {
			this.name = name;
			this.range = range;
			this.damage = damage;
			this.difficulty = difficulty;
			this.targets = targets;
			this.attacks = attacks;
			this.weaponSpeed = weaponSpeed;
			this.description = description;
		}
	}
	public enum Targets {
		none,
		ground,
		air,
		both
	}
	public enum Difficulty {
		noob,
		easy,
		medium,
		hard,
		pro
	}
}
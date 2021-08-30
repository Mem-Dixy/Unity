namespace EnTropy {
	public class TowerMaker {
		public static TowerData[] Make() {
			return new TowerData[] {
				new TowerData(
					"Tower",
					7,
					1.00f,
					Difficulty.noob,
					Targets.both,
					1,
					2,
					"Most basic weapon."
				),
				new TowerData(
					"High damage tower",
					7,
					1.90f,
					Difficulty.easy,
					Targets.both,
					1,
					2,
					"Same as basic tower's weapon but it has projectile and higher damage."
				),
				new TowerData(
					"Bounce tower",
					7,
					0.70f,
					Difficulty.easy,
					Targets.both,
					2,
					2,
					"This weapon attack bounces 2 times."
				),
				new TowerData(
					"Big multi shot tower",
					8,
					0.36f,
					Difficulty.easy,
					Targets.both,
					6,
					2,
					"Attacks up to 6 units at the same time."
				),
				new TowerData(
					"Multi shot tower",
					5,
					0.90f,
					Difficulty.medium,
					Targets.both,
					3,
					2,
					"Attacks up to 3 units at the same time."
				),
				new TowerData(
					"Big Splash tower",
					5,
					0.78f,
					Difficulty.medium,
					Targets.ground,
					1,
					2,
					"Has 3.8 splash radius."
				),
				new TowerData(
					"Splash tower",
					7,
					1.15f,
					Difficulty.medium,
					Targets.ground,
					1,
					2,
					"Has 2.0 splash radius."
				),
				new TowerData(
					"Chain reaction tower",
					6,
					0.37f,
					Difficulty.medium,
					Targets.both,
					7,
					2,
					"This weapon attack bounces 2 times, at each bounce it doubles."
				),
				new TowerData(
					"Rage Tower",
					5,
					1.58f,
					Difficulty.medium,
					Targets.both,
					1,
					2,
					"This tower cn gets angry, but it turns calm really fast."
				),
				new TowerData(
					"Torus tower",
					8,
					0.40f,
					Difficulty.medium,
					Targets.ground,
					1,
					2,
					"Has huge 5 range splash, but doesn’t hurts it's target."
				),
				new TowerData(
					"Charge tower",
					8,
					2.02f,
					Difficulty.medium,
					Targets.both,
					1,
					4,
					"Turret which doubles its speed under 20 seconds."
				),
				new TowerData(
					"Sniper tower",
					10,
					1.47f,
					Difficulty.medium,
					Targets.both,
					2,
					4,
					"Looooong range. Attacks another target in 20 range of its primary target!"
				),
				new TowerData(
					"Anti Air tower",
					8,
					4.20f,
					Difficulty.medium,
					Targets.air,
					1,
					2,
					"Attacks only air units but with high damage."
				),
				new TowerData(
					"Melee tower",
					1.01615f,
					2.13f,
					Difficulty.hard,
					Targets.both,
					2,
					2,
					"Attacks unit only nearby of the tower."
				),
				new TowerData(
					"Bombardment tower",
					1,
					2.70f,
					Difficulty.hard,
					Targets.ground,
					1,
					2,
					"Has splash, but only attacks units under the tower."
				),
				new TowerData(
					"Damage field tower",
					3,
					0.66f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Attacks all units within range."
				),
				new TowerData(
					"Discharge tower",
					5,
					3.30f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Turret which halves its speed under 15 seconds."
				),
				new TowerData(
					"Flame tower",
					7,
					0.86f,
					Difficulty.pro,
					Targets.both,
					1,
					2,
					"Damages all units which pass into its flames."
				),
				new TowerData(
					"Line splash tower",
					7,
					0.85f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Does damage near the target in right angel compared to the tower."
				),
				new TowerData(
					"Angle tower",
					8,
					2.18f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Only attacks in 90* angle, but you can change the direction of that 90*."
				),
				new TowerData(
					"Minimum range tower",
					8,
					2.18f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Doesn’t attack unit's within 5 range."
				),
				new TowerData(
					"Infection tower",
					7,
					0.83f,
					Difficulty.hard,
					Targets.ground,
					7,
					4,
					"This weapon looks really cool, use it carefully!"
				),
				new TowerData(
					"Trigger tower",
					7,
					2.45f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"This weapon has deep dreams, so units has to come within 1 range to wake it up. After its awake, it keeps attacking while it finds units within 7 range (+range bonuses). But it losts interest as soon it finds no units within 7 range and falls asleep."
				),
				new TowerData(
					"Two Sided tower",
					8,
					1.22f,
					Difficulty.hard,
					Targets.both,
					2,
					2,
					"Only shoots to the left/righ side in a 180* angle range. Cooldowns displayed to make it easier to differentiate beteen the 2 weapon."
				),
				new TowerData(
					"Long Minimum rang tower",
					11,
					1.62f,
					Difficulty.hard,
					Targets.both,
					1,
					2,
					"Doesn’t attack units within 8 range."
				)
			};
		}
	}
}
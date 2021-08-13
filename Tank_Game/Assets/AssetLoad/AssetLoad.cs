public class AssetLoad : UnityEngine.MonoBehaviour {
	public static AssetLoad instance;
	public UnityEngine.GameObject newGameObject;
	public UnityEngine.Transform targetingInner;
	public UnityEngine.Transform targetingOuter;
	public UnityEngine.Transform healthBar;
	public UnityEngine.Transform[] mapIcon;
	private void Awake() {
		instance = this;
	}
	public Weapons[] weapons;
	public Weapon[] weapon;
	public enum Weapons {
		Unarmed,
		Rocket,
		Missile
	}
	public enum MapIcon {
		square,
		arrow
	}
	public static UnityEngine.Vector3 DimensionLimiter(UnityEngine.Vector3 input , UnityEngine.Vector3 axes) {
		return UnityEngine.Vector3.Scale(
			input ,
			new UnityEngine.Vector3(
				UnityEngine.Mathf.Round(UnityEngine.Mathf.Clamp01(axes.x)) ,
				UnityEngine.Mathf.Round(UnityEngine.Mathf.Clamp01(axes.y)) ,
				UnityEngine.Mathf.Round(UnityEngine.Mathf.Clamp01(axes.z))
			)
		);
	}
}
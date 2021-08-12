using UnityEngine;
using System.Collections;

public class AssetLoad : MonoBehaviour {
	
	public static AssetLoad assetLoad;
	
	public GameObject newGameObject;
	public Transform targetingInner;
	public Transform targetingOuter;
	public Transform healthBar;
	public Transform[] mapIcon;
		
	private void Awake () {
		assetLoad = this;
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
	
	public static Vector3 DimensionLimiter (Vector3 input, Vector3 axes) {
		return Vector3.Scale(input, new Vector3(Mathf.Round(Mathf.Clamp01(axes.x)), Mathf.Round(Mathf.Clamp01(axes.y)), Mathf.Round(Mathf.Clamp01(axes.z))));
	}

}
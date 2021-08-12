using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	
	public bool canFire;
	public float fireRate = 1; // >0
	public float ammo = Mathf.Infinity;		// Make sure number is greater that 0.1, = 10 fps. 
	public Transform prefab;

	private float nextFire;
//	private LayerMask teamLayer;
//	private PlayerBasic basic;
	
	private bool fired;
	
	private void Start () {
//		basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic;
//		teamLayer = ~(1<<LayerMask.NameToLayer(transform.root.name));
	}
	private void Update () {
		if (!GameController.paused && canFire && ammo>0) {
			if (Time.time>nextFire) {
				if (Time.time-Time.deltaTime<nextFire-fireRate) {
					
				}
				if (Time.deltaTime>fireRate) {
				}
				for (int i=0; i<Mathf.Max(1, Mathf.Floor(Time.deltaTime/Mathf.Max(0.01f, fireRate))); i++) {
					nextFire = Time.time + fireRate;
					ammo--;
					Transform shot = Instantiate(prefab, transform.position, transform.rotation) as Transform;
					if (GetComponent<Collider>()) {
						Physics.IgnoreCollision(GetComponent<Collider>(), shot.GetComponent<Collider>(), true);
					}
				}
			}
		}
		else {
			nextFire += Time.deltaTime;
		}
	}
//	private void Update () {
//		if (basic.canFire && ammo>0) {
//			if (Time.time>nextFire) {
//				nextFire = Time.time + fireRate;
//				ammo--;
//				Transform shot = Instantiate(prefab, transform.position, transform.rotation) as Transform;
//				if (collider) {
//					Physics.IgnoreCollision(collider, shot.collider, true);
//				}
//			}
//		}
//		else {
//			nextFire += Time.deltaTime;
//		}
//	}
}
using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public Weapon weapon;
	public int rounds = 0;
	public float reloadTime;
	public float reloadRandomness;
	public AudioClip shotSound;

	private float timeOfNextShot = 0;

	public bool Fire (Collider ignore) {		// Retun: Did we fire?
		if (rounds>0 && Time.time>=timeOfNextShot) {
			timeOfNextShot = Time.time+reloadTime+Random.Range(-reloadRandomness, reloadRandomness);		// Makes it so EVEY bullet is not being fired at once.
//			Instantiate(weapon.transform, transform.position, transform.rotation);
//			weapon = (Instantiate(weapon.transform, transform.position, transform.rotation) as Transform).GetComponent(typeof(Weapon)) as Weapon;
			Transform shot = Instantiate(weapon.transform, transform.position, transform.rotation) as Transform;
			Physics.IgnoreCollision(ignore, shot.GetComponent<Collider>(), true);
			if (shotSound) {
				GetComponent<AudioSource>().PlayOneShot(shotSound);
			}
			return true;
		}
		return false;
	}
}
#pragma strict
#pragma implicit
#pragma downcast

private var bulletDamage : int;
private var teamObj : Transform;
private var soundFire : AudioClip;
private var basic : PlayerBasic;
private var fireTime : float;
private var waitTime : float;

private var teamLayer : LayerMask;

function Start () {
	basic = transform.parent.parent.parent.GetComponent(PlayerBasic);
	soundFire = basic.soundFire;
	bulletDamage = basic.bulletDamage;
	teamObj = basic.transform.parent;
	fireTime = basic.fireTime;
	teamLayer = ~(1<<LayerMask.NameToLayer(transform.root.name));
}

function Fire () {
	if (waitTime<fireTime) {
		waitTime += Time.deltaTime;
	}
	else {
		waitTime -= (fireTime+Random.Range(fireTime-(fireTime/10), fireTime+(fireTime/10)));
		// Bullet.
		var hit : RaycastHit;
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hit, Mathf.Infinity)) {
			Debug.DrawLine(transform.position, hit.point, Color.magenta);
			if (hit.transform.parent&&hit.transform.gameObject.layer!=basic.gameObject.layer) {
				var found : PlayerBasic = hit.transform.GetComponent(PlayerBasic);
				if (found) {
					if (transform.parent!=hit.transform.parent) {	// See if what we hit is on our teamObj.
						found.SendMessage("ChangeHealth", bulletDamage);			// Tells it to update the TextMesh.
					}
				}
			}
		}
		//
		audio.PlayOneShot(soundFire);
	}
}

@script RequireComponent(AudioSource)
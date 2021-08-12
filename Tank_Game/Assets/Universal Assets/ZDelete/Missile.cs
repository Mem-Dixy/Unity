using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Missile : MonoBehaviour {
	
	public bool homing = true;
	public float damage = 1;
	public float initialSpeed = 1;
	public float fuel = Mathf.Infinity;		// How many seconds can we move before we stop?
	public float fuse = Mathf.Infinity;		// How many seconds before we blow up?
	
	private Transform targetingInner;
	private Transform targetingOuter;
	private PlayerBasic basic;

	private void Start () {
		homing = true;
		(targetingInner = (Instantiate(AssetLoad.assetLoad.targetingInner, transform.position, transform.rotation) as Transform)).parent = transform;
		(targetingOuter = (Instantiate(AssetLoad.assetLoad.targetingOuter, transform.position, transform.rotation) as Transform)).parent = transform;
		(basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic).autopilot = true;
		float ratio = (float)Screen.width/(float)Screen.height;
		Camera missileCamera = (Instantiate(AssetLoad.assetLoad.newGameObject, transform.position, transform.rotation) as GameObject).AddComponent(typeof(Camera)) as Camera;
		missileCamera.transform.parent = transform;
		missileCamera.orthographicSize = 128;
		missileCamera.rect = new Rect(1-(0.4F/ratio), 0.6F, 0.4F/ratio, 0.4F);		// new Rect(0.8, 0.8, 0.2 0.2)?
		missileCamera.name = "Camera";
	}
	private void Update () {
		if ((fuse -= Time.deltaTime)<=0) {
			Explode(transform.position, null);
		}
		Vector3 rotate = Vector3.zero;
//		Vector3 translate = Vector3.zero;
		if (Mathf.Abs(basic.moveSpeed)==Mathf.Infinity) {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
				Debug.DrawLine(transform.position, hit.point, Color.magenta);
				PlayerBasic found = hit.transform.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
				if (found) {
					DoDamage(transform.position);
					//found.SendMessage("ChangeHealth", bulletDamage);			// Tells it to update the TextMesh.
				}
			}
		}
		if (basic.autopilot) {
			Vector3 destination = (homing ? basic.GetDestination() : transform.position+transform.forward);
			bool visible = (destination!=Vector3.zero);
			targetingInner.position = destination;
			targetingOuter.position = destination;
			targetingInner.Rotate(0, 0, -180*Time.deltaTime);
			targetingOuter.Rotate(0, 0, 180*Time.deltaTime);
			targetingInner.GetComponent<Renderer>().enabled = visible;
			targetingOuter.GetComponent<Renderer>().enabled = visible;
			rotate = basic.RelativeRotationToWorldPoint((destination!=Vector3.zero ? destination : Vector3.zero), Vector3.up);
//			translate = transform.InverseTransformPoint(destination!=Vector3.zero ? destination : transform.forward);
		}
		else {
			rotate = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll"))*basic.turnSpeed;
//			translate = new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge"))*basic.moveSpeed;
		}
		if (fuel>0) {
			fuel -= Time.deltaTime;
			basic.InitiateCommand(rotate, Vector3.forward);
		}
	}
	private void OnCollisionEnter (Collision collision) {
		Explode(collision.contacts[0].point, collision);
	}	
	private void DoDamage (Vector3 location) {
//		other.health-damage;		// Do Damage.
		Destroy(gameObject);
	//	Instantiate(this, location, transform.rotation);
	}

	private float power = 1;
	private float damageRange = 1;
//	private float damageMax = 1;

//	private void OnCollisionEnter (Collision collision) {		// Advanced explosion that starts at missile contact point and decreases damage the farther away object is which is determined by shooting a raycast all at it.
//	// finding the object we want, then using that point to put into damage vs distance formula.		// NOTE. I don't realy know how well this works but it seams to work fairly well.
//		ContactPoint contactPoint = collision.contacts[0];
//		Vector3 contactPosition = contactPoint.point;
//		//Instantiate(explosionPrefab, contactPosition, contactRotation);
//	 	Destroy(gameObject);
//	}
	
	
	private void Explode (Vector3 explodeOrigin, Collision collision) {
		if (damageRange<=0) {		// Does not explode.
			// Get other and do damage.
		}
		else {
			//Instantiate(explosionPrefab, contactPosition, contactRotation);
//			//Instantiate(prettyParticleExplodSphere);
			Collider[] colliders = Physics.OverlapSphere(explodeOrigin, damageRange);
			for (int j=0; j<colliders.Length; j++) {
				Collider impact = colliders[j];
				if (impact.GetComponent<Rigidbody>()) {
					impact.GetComponent<Rigidbody>().AddExplosionForce(power, explodeOrigin, damageRange);	// Exposive force.
				}
				PlayerBasic found = impact.transform.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
				if (found) {
//					found.SendMessage("ChangeHealth", Mathf.Clamp01(1-Vector3.Distance(explodeOrigin, impact.transform.position)/damageRange)*damageMax);		// Objects recive less damage the farther away they are.
					//(found ? found : find).SendMessage("ChangeHealth", Mathf.Lerp(0, damageMax, (damageRange-Vector3.Distance(hit.point, contactPosition)/damageRange))
					////		other.health-damage;		// Do Damage.
				}
			}
		}
		Destroy(gameObject);
	}

}
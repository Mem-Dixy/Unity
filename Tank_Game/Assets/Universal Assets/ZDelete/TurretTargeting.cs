using UnityEngine;
using System.Collections;

//@script RequireComponent(AudioSource)
//[RequireComponent(typeof(PlayerBasic))]

public class TurretTargeting : MonoBehaviour {
/*	
//var lookAtSpot : Vector3;

	private int targetPriority = 0;
	private float sightRange = 512;
	private string teamSide;	// Make this Enum/int?
	private Transform tracking;
	private PlayerBasic basic;
	
	
	Vector3 ourPosition = Vector3.zero;
	float objectDistance = Mathf.Infinity;
	int otherDamage = 0;
	float otherHealth = Mathf.Infinity;
	float yRotate = 180;

	//
	private float bulletDamage;
	private float bulletSpeed;
	private float bulletReload;
	
	private AudioClip soundFire;
	private float waitTime;
	private float fireRandomness = 0.1f;

	private void Start () {
		basic = transform.parent.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		sightRange = basic.sightRange;
		teamSide = "Team1";
		//
		soundFire = basic.soundFire;
		bulletDamage = basic.bulletDamage;
		bulletSpeed = basic.bulletSpeed;
		bulletReload = basic.bulletReload;
	}
	private void Update () {
		RaycastHit hit;
		if (tracking) {
			Vector3 face = tracking.position-transform.position;
			if (basic.human) {
				face = new Vector3(face.x, 0, face.z);		// Restrict to 2D plane.
			}
			if (Physics.Raycast(transform.position, face, out hit, sightRange)) {
				Debug.DrawRay(transform.position, face, Color.blue);
				if (hit.transform!=tracking) {		// If we are looking at a friend then don't fire.
					tracking = null;
				}
			}
		}
		if (!tracking) {		// If no target then reset variables.
			ourPosition = transform.position;
			objectDistance = Mathf.Infinity;
			otherDamage = 0;
			otherHealth = Mathf.Infinity;
			yRotate = 180;
			targetPriority = 0;
		}
		Collider[] colliderList = Physics.OverlapSphere(ourPosition, sightRange);		// Continually check for (better) targets.
		for (int i=0; i<colliderList.Length; i++) {															// Finds all objects near us.
			PlayerBasic other = colliderList[i].GetComponent(typeof(PlayerBasic)) as PlayerBasic;
			if (other && other.teamSide!=teamSide && Physics.Raycast(ourPosition, other.transform.position-ourPosition, out hit, Mathf.Infinity) && hit.transform==other.transform) {		// If not on the same team and if we can see them.
				int worth = (other.CompareTag("Defense") ? 4 : (other.CompareTag("Character") ? 3 : (other.CompareTag("Objective") ? 2 : (other.CompareTag("Building") ? 1 : 0))));
				if (worth>=targetPriority) {
					targetPriority = worth;		// Weights targets so it shoots at threats before buildings.
					float newY = Vector3.Angle(other.transform.position-ourPosition, transform.forward);
					if (newY+10<yRotate) {				// Target the one in front of us.
						yRotate = newY;
						float difference = Vector3.Distance(other.transform.position, ourPosition);
						if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
							objectDistance = (difference<objectDistance ? difference : objectDistance);									// Have the one that is the closest be the reference.
							tracking = other.transform;						// If it makes it this far then assign the object to the target.
						}
					}
				}
			}
		}
	}
	public void Fire (Transform source, Transform weapon) {
		if (Time.time>=waitTime) {
			waitTime = Time.time+bulletReload+Random.Range(-fireRandomness, fireRandomness);		// Makes it so EVEY bullet is not being fired at once.
			Weapon thing = (Instantiate(weapon, source.position, source.rotation) as Transform).GetComponent(typeof(Weapon)) as Weapon;
			print(thing);
			audio.PlayOneShot(soundFire);
		}
//			RaycastHit hit;		// Bullet.
//			if (Physics.Raycast(source.position, source.forward, out hit, Mathf.Infinity)) {
//				Debug.DrawLine(source.position, hit.point, Color.magenta);
//				PlayerBasic found = hit.transform.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
//				if (found) {
//					found.SendMessage("ChangeHealth", bulletDamage);			// Tells it to update the TextMesh.
//				}
//			}
	}
	*/
}
/*
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Helicopter : MonoBehaviour {
	
	private PlayerBasic basic;

	private void Start () {
		basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		basic.teamSide = transform.root.name;		// Set what team we are on.
		// Set controls availability.
	}
	private void Update () {
//		Vector3 rotate = Vector3.zero;
//		Vector3 translate = Vector3.zero;
		if (basic.autopilot) {
//			Vector3 destination = basic.GetDestination();
//			rotate = basic.RelativeRotationToWorldPoint((destination!=Vector3.zero ? destination : Vector3.zero), Vector3.up);
//			translate = transform.InverseTransformPoint(destination!=Vector3.zero ? destination : transform.forward);

//			Vector3 destination = basic.GetDestination();
//			rotate = basic.RelativeRotationToWorldPoint(destination, Vector3.up);
//			translate = transform.InverseTransformPoint(destination);

			Vector3 destination = basic.GetDestination();
			basic.RotateTo(destination, transform, Vector3.one);
			basic.TranslateTo(destination, transform, Vector3.one);

//			Vector3 destination = basic.GetDestination();
//			rotate = basic.RelativeRotationToWorldPoint(destination, Vector3.up);
//			transform.Rotate((rotate.magnitude>=basic.turnSpeed*Time.deltaTime ? rotate.normalized*basic.turnSpeed*Time.deltaTime : rotate));
//			translate = transform.InverseTransformPoint(destination);
//			transform.Translate((translate.magnitude>=basic.moveSpeed*Time.deltaTime ? translate.normalized*basic.moveSpeed*Time.deltaTime : translate));
//
//			print(rotate);
//			print(translate);


		}
		else {//if (canControll) {		// Is the player in this vehicle?
//			rotate = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll"))*basic.turnSpeed;
//			translate = new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge"))*basic.moveSpeed;
//			rotate = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll"))*4000;//0.25f;
//			translate = new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge"));
//			print(rotate.magnitude>rotate.normalized.magnitude);
			basic.PlayerInput(transform, new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll")), new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge")));
		}
//		basic.InitiateCommand(rotate, translate);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Tank : MonoBehaviour {

	private PlayerBasic basic;

	private void Start () {
		basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		basic.teamSide = transform.root.name;		// Set what team we are on.
		// Set controls availability.
	}
	private void Update () {
//		Vector3 rotate = Vector3.zero;
//		Vector3 translate = Vector3.zero;
//		Vector3 rotate2 = Vector3.zero;
		if (basic.autopilot) {
//			Vector3 destination = basic.GetDestination();
//			rotate = basic.RelativeRotationToWorldPoint((destination!=Vector3.zero ? destination : Vector3.zero), Vector3.up);
//			translate = transform.InverseTransformPoint(destination!=Vector3.zero ? destination : transform.forward);			
			Vector3 destination = basic.GetDestination();
			basic.RotateTo(destination, transform, Vector3.up);
			basic.TranslateTo(destination, transform, Vector3.forward);
			basic.RotateTo(basic.tracking.position, basic.laser, Vector3.one);
		}
		else {

//			rotate = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll"))*basic.turnSpeed;
//			translate = new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge"))*basic.moveSpeed;
//			rotate2 = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), 0)*basic.turnSpeed;

//			rotate = new Vector3(0, Input.GetAxisRaw("Slip"), 0)*basic.turnSpeed;
//			translate = new Vector3(0, 0, Input.GetAxisRaw("Surge"))*basic.moveSpeed;
//			rotate2 = basic.RelativeRotationToWorldPoint(basic.laser.position+new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), 0), basic.laser);

			basic.PlayerInput(transform, new Vector3(0, Input.GetAxisRaw("Slip"), 0), new Vector3(0, 0, Input.GetAxisRaw("Surge")));
			basic.PlayerInput(basic.laser, new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), 0), Vector3.zero);

		}
//		basic.InitiateCommand(rotate, translate);
//		basic.InitiateCommand(rotate2, Vector3.zero, basic.laser, basic.turnSpeed);
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Turret : MonoBehaviour {

	private PlayerBasic basic;
	
	private void Start () {
		(basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic).autopilot = true;
	}	
	private void Update () {
		Vector3 rotate = Vector3.zero;
//		Vector3 translate = Vector3.zero;
		if (basic.autopilot) {
			Vector3 destination = basic.GetDestination();
			rotate = basic.RelativeRotationToWorldPoint((destination!=Vector3.zero ? destination : transform.position+transform.forward*10), basic.laser);
//			translate = transform.InverseTransformPoint(destination!=Vector3.zero ? destination : transform.forward);
		Debug.DrawLine(basic.laser.position, destination, Color.blue);
		}
		else {
			rotate = new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll"))*basic.turnSpeed;
//			translate = new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge"))*basic.moveSpeed;
		}		
		basic.InitiateCommand(rotate, Vector3.zero, basic.laser, basic.turnSpeed);
	}
}
*/
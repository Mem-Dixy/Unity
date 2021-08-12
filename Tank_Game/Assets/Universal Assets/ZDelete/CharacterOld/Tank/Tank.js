#pragma strict
#pragma implicit
#pragma downcast

var speedTranslate : int = 10;
var speedRotate : int = 100;

private var degre : float;
private var canControl : boolean;		// Custom controlls options.
private var basic : PlayerBasic;

var target : Transform;
var sightRange = 512;		// NEED? Maybe we should just do a global search and get closest one.
private var team;
// AI variables.
private var wait : float;
var waitTime : int;
private var oldPosition : Vector3;
private var nowPosition : Vector3;
private var newPosition : Vector3;
private var isBackingUp : boolean;
private var backUpWait : float;
var backUpTime : int;
var wallSpace : int;
private var delay : float;

var smooth = 0.3;
private var yVelocity = 0.0;

function Start () {
	basic = GetComponent(PlayerBasic);
//	basic.SendMessage("SetValue", Vector2(speedRotate, speedTranslate));
//	var useInput = new boolean[6];
//	useInput[0] = false;
//	useInput[1] = true;
//	useInput[2] = false;
//	useInput[3] = false;
//	useInput[4] = false;
//	useInput[5] = true;
//	basic.SendMessage("SetControls", useInput);
//	var find = GetComponentsInChildren(Transform);
	team = transform.parent;
}

function OnTriggerEnter (other : Collider) {
	if (other.transform==target) {		// If we find node then go to next one in the list.
		target = other.transform.parent;
	}
}

function Update () {
	if (!GameController.paused) {
		var xRotate : float;
		var yRotate : float;
		var zRotate : float;
		var xTransform : float;
		var yTransform : float;
		var zTransform : float;
		if (!canControl) {
			if (!target) {		// Find Path to enemy base.
				var colliderList = Physics.OverlapSphere(transform.position, sightRange*2);
				var objectDistance = Mathf.Infinity;
				var ourPosition = transform.position;
				for (i=0; i<colliderList.length; i++) {															// Finds all objects near us.
					var other : Collider = colliderList[i];
					if (other.CompareTag("Road")) {
						if (team!=other.transform.root) {
							var difference = (other.transform.position - ourPosition);
							var curentDistance = difference.sqrMagnitude; 
							if (curentDistance<objectDistance) {
								objectDistance = curentDistance;									// Have the one that is the closest be the reference.
								target = other.transform;
							}
						}
					}
				}
			}
			if (target) {
				var oldAngleY = transform.eulerAngles.y;				// Save current rotation.
				transform.LookAt(target);
				var newAngleY = transform.eulerAngles.y;						// Save new rotation.
				var yAngle = Mathf.SmoothDampAngle(oldAngleY, newAngleY, yVelocity, smooth, speedRotate);
				transform.rotation = Quaternion.Euler(0, yAngle, 0);
				zTransform = 1;
			}
			else {
				if (!isBackingUp&&(wait<waitTime)) {
					wait += Time.deltaTime;		// This will equal 1 in one second, it will trigger function when it reaches the time we set for it.
				}
				else {
					wait = 0;
					oldPosition = nowPosition;
					newPosition = transform.localPosition;
					if (!isBackingUp&&((Mathf.Abs((newPosition.x-nowPosition.x)+(nowPosition.x-oldPosition.x))<1)||(Mathf.Abs((newPosition.x-nowPosition.x)+(nowPosition.x-oldPosition.x))<1))) {		// If we havent moved more then 1 unit.
						isBackingUp = true;
					}
				}
				if (isBackingUp) {
					zTransform = -1;
					if (backUpWait<backUpTime) {
						backUpWait += Time.deltaTime;
					}
					else {
						backUpWait = 0;
						isBackingUp = false;
					}
				}
				else {
					zTransform = 1;
				}
				var hit : RaycastHit;
				var hitL = Mathf.Infinity;
				var hitR = Mathf.Infinity;
				if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), hit, 512)) {	// Is Mathf.Infinity too expensive? to be safe use 512.
					Debug.DrawLine (transform.position, hit.point, Color.white);
					if (hit.distance<wallSpace) {
						transform.Rotate(0, -15, 0);
						if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), hit, 512)) {
							Debug.DrawLine (transform.position, hit.point, Color.gray);
							hitL = hit.distance;
						}
						transform.Rotate(0, 30, 0);
						if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), hit, 512)) {
							Debug.DrawLine (transform.position, hit.point, Color.gray);
							hitR = hit.distance;
						}
						transform.Rotate(0, -15, 0);
						if (Mathf.Abs(hitL-hitR)<5) {
							yTransform = (hitL<hitR ? 1 : -1);
						}
						else if (delay>1) {	// Delay limit at 1 in this part.
							yTransform = (Random.value<0.5 ? 1 : -1);
							delay = 0;
						}
						else {
							delay += Time.deltaTime;
						}
					}
					else {			// If nothing is in front of us then see if they are on the side of us.
					}
				}
			}
		}
		else if (basic.human&&basic.canControl) {
			yRotate = Input.GetAxis("XTransform");
			zTransform = Input.GetAxis("ZTransform");
		}
		transform.Rotate(0, yRotate*Time.deltaTime*speedRotate, 0);
		transform.Translate(0, 0, zTransform*Time.deltaTime*speedTranslate);
	}
}
// Sent to us by GameController
function CanControl (incoming : boolean) {
	canControl = incoming;
}

@script RequireComponent(PlayerBasic)
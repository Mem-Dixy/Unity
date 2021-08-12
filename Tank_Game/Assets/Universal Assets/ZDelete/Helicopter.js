#pragma strict
#pragma implicit
#pragma downcast

var speedTranslate : int = 10;
var speedRotate : int = 100;
var hoverHeight : float;		// Value goten from HelicopterHover Script
var tiltLimit : int;

var compass : Transform;

private var clampMin : float;
private var clampMax : float;

var missileItem : Transform;
var missileCount : int;
private var missileArray : Transform[];
private var missileFire = true;

var rocketItem : Transform;
var rocketCount : int;
private var rocketArray : Transform[];
private var rocketSelect : int;

private var fired : CharacterTargetFind1;		// Builds an array of all children with this scrip.

var sightRange = 512;		// NEED? Maybe we should just do a global search and get closest one.

var canControl : boolean;

private var basic : PlayerBasic;

private var team;

var inputX : float;
var inputY : float;
var inputZ : float;
var bla : float;
function Start () {
	team = transform.parent;
	basic = GetComponent(PlayerBasic);
	basic.SendMessage("SetValue", Vector2(speedRotate, speedTranslate));
	var useInput = new boolean[6];
//	useInput[0] = true;
//	useInput[1] = true;
//	useInput[2] = true;
	useInput[0] = false;
	useInput[1] = false;
	useInput[2] = false;
	useInput[3] = true;
	useInput[4] = true;
	useInput[5] = true;
	basic.SendMessage("SetControls", useInput);
	var tempArray = GetComponentsInChildren(Transform);
	for (i=0; i<tempArray.length; i++) {
		if (tempArray[i].CompareTag("Missile")) {
			missileCount++;
		}
		if (tempArray[i].CompareTag("Rocket")) {
			rocketCount++;
		}
	}
	missileArray = new Transform[missileCount];
	rocketArray = new Transform[rocketCount];
	missileCount = 0;
	rocketCount = 0;
	for (i=0; i<tempArray.length; i++) {
		if (tempArray[i].CompareTag("Missile")) {
			missileArray[missileCount] = tempArray[i].transform;
			missileCount++;
		}
		if (tempArray[i].CompareTag("Rocket")) {
			rocketArray[rocketCount] = tempArray[i].transform;
			rocketCount++;
		}
	}
	rocketCount *= 19;	
	tempFired = GetComponentInChildren(CharacterTargetFind1);
	fired = tempFired;
}

function OnTriggerEnter (other : Collider) {
	if (other.transform==basic.goPath) {		// If we find node then go to next one in the list.
		basic.goPath = other.transform.parent;
	}
}

function Update () {
	if (!GameController.paused) {
		// Input variables.
		var xTransform : float;
		var yTransform : float;
		var zTransform : float;
		var fireMissile : boolean;
		var fireRocket : boolean;
		if (!basic.human) {
			if (!basic.goPath) {		// Find Path to enemy base.
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
								basic.goPath = other.transform;
							}
						}
					}
				}
			}
			if (basic.goPath) {
//				var awayness = 10;
//				var oldLocationX = transform.position.x;		// Save current position.
//				var oldLocationY = transform.position.y;		// Save current position.
//				var oldLocationZ = transform.position.z;		// Save current position.
//				var newLocationX = basic.goPath.transform.position.x;
//				var newLocationY = basic.goPath.transform.position.y;
//				var newLocationZ = basic.goPath.transform.position.y;
//				var inputX = (oldLocationX<newLocationX ? 1 : -1);															// This tests to see which direction to turn to reach newAngle fastest.
//				var inputY = (oldLocationY<newLocationY ? 1 : -1);															// This tests to see which direction to turn to reach newAngle fastest.
//				var inputZ = (oldLocationZ<newLocationZ ? -1 : 1);															// This tests to see which direction to turn to reach newAngle fastest.
//				var turnX = oldLocationX+(inputX * Time.deltaTime * (Mathf.Abs(newLocationX-oldLocationX)<awayness ? Mathf.Abs(newLocationX-oldLocationX)*(speedTranslate/awayness) : speedTranslate));		// Get oldAngle and add speedTranslate and direction to get a new angle.
//				var turnY = oldLocationY+(inputY * Time.deltaTime * (Mathf.Abs(newLocationY-oldLocationY)<awayness ? Mathf.Abs(newLocationY-oldLocationY)*(speedTranslate/awayness) : speedTranslate));		// Get oldAngle and add speedTranslate and direction to get a new angle.
//				var turnZ = oldLocationZ+(inputZ * Time.deltaTime * (Mathf.Abs(newLocationZ-oldLocationZ)<awayness ? Mathf.Abs(newLocationZ-oldLocationZ)*(speedTranslate/awayness) : speedTranslate));		// Get oldAngle and add speedTranslate and direction to get a new angle.
//				var dir = Vector3(turnX, turnY, turnZ);
//				transform.position = dir.normalized;
//				var destination = Vector3.Distance(basic.goPath.position, transform.position);
//				//if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
				xTransform = (transform.position.x<basic.goPath.transform.position.x ? 1 : -1);
				yTransform = (transform.position.y<basic.goPath.transform.position.y ? 1 : -1);
				zTransform = (transform.position.z<basic.goPath.transform.position.z ? 1 : -1);
//				//}
			}
			else {
				xTransform = 0;
				yTransform = 0;
				zTransform = 1;
			}
		}
		else if (basic.human&&basic.canControl) {
			xTransform = Input.GetAxisRaw("XTransform");
			yTransform = Input.GetAxisRaw("YTransform");
			zTransform = Input.GetAxisRaw("ZTransform");
			fireMissile = Input.GetButtonDown("Fire2");
			fireRocket = Input.GetButtonDown("Fire3");
		}
		var outputX = inputX;
		var outputY = inputY;
		var outputZ = inputZ;
		inputX += (xTransform==0 ? (inputX==0 ? 0 : (inputX<0 ? 3 : -3)) : (xTransform>0 ? 3 : -3))*Time.deltaTime;
		inputY += (yTransform==0 ? (inputY==0 ? 0 : (inputY<0 ? 3 : -3)) : (yTransform>0 ? 3 : -3))*Time.deltaTime;
		inputZ += (zTransform==0 ? (inputZ==0 ? 0 : (inputZ<0 ? 3 : -3)) : (zTransform>0 ? 3 : -3))*Time.deltaTime;
		inputX = Mathf.Clamp(inputX, (xTransform<0||outputX<0 ? -1.0 : 0), 1.0);
		inputY = Mathf.Clamp(inputY, (yTransform<0||outputY<0 ? -1.0 : 0), 1.0);
		inputZ = Mathf.Clamp(inputZ, (zTransform<0||outputZ<0 ? -1.0 : 0), 1.0);
		transform.Translate(Vector3(inputX, inputY, inputZ).normalized*Time.deltaTime*speedTranslate);
		if (fireMissile&&missileCount>0) {
			for (i=0; i<missileArray.length; i++) {
				if (missileArray[i].renderer.enabled&&missileFire) {
					var missileLaunch : Transform = Instantiate(missileItem, missileArray[i].transform.position, missileArray[i].transform.rotation);
					missileLaunch.parent = transform.parent;
					var missileFlame : Missile = missileLaunch.GetComponent(Missile);
					//missileFlame.target = fired.target;
					missileLaunch.SendMessage("IsHuman", false);
					missileArray[i].renderer.enabled = false;
					missileCount--;
					missileFire = false;
					Physics.IgnoreCollision(collider, missileLaunch.collider, true);
				}
			}
			missileFire = true;
		}
		if (fireRocket&&rocketCount>0) {
			var rocketLaunch : Transform = Instantiate(rocketItem, rocketArray[rocketSelect].transform.position, rocketArray[i].transform.rotation);
			//rocketLaunch.parent = transform.parent;
			rocketLaunch.transform.parent = transform.parent;
			var rocketFlame : Missile = rocketLaunch.GetComponent(Missile);
			//rocketFlame.target = fired.target;
			rocketCount--;
			rocketSelect = (rocketSelect>=rocketArray.length-1 ? 0 : rocketSelect+1);
			Physics.IgnoreCollision(collider, rocketLaunch.collider, true);
		}
	}
}

// Sent to us by GameController
function CanControl (incoming : boolean) {
	canControl = incoming;
}

@script RequireComponent(PlayerBasic)
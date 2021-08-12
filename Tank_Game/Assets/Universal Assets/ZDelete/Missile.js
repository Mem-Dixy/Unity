#pragma strict
#pragma implicit
#pragma downcast

var damageMax : int;
var speedTranslate : float;
var speedRotate : float;
var homing : boolean;
var damageRange = 0.5;
var power = 10.0;
var sightRange = 50;

private var team : Transform;
private var fly : ConstantForce;
var basic : PlayerBasic;		// Public so hlicopter can reach it.

var speedfly : Vector3;

function Start () {
	basic = GetComponent(PlayerBasic);
//	basic.SendMessage("SetValue", Vector2(speedRotate, speedTranslate));
//	var useInput = new boolean[6];
//	if (guiding||homing) {
//		useInput[0] = true;
//		useInput[1] = true;
//		useInput[2] = true;
//	}
//	useInput[3] = false;
//	useInput[4] = false;
//	useInput[5] = true;
//	basic.SendMessage("SetControls", useInput);
	basic = GetComponent(PlayerBasic);
	if (basic.canControl) {
		sightRange = basic.sightRange;
		team = basic.transform.parent;
		var ratio = (0.0+Screen.width)/Screen.height;
		var missileCamera : Camera = GetComponentInChildren(Camera);
		if (missileCamera) {
			missileCamera.orthographicSize = 128;
			missileCamera.rect = Rect(1-(0.4/ratio), 0.6, 0.4/ratio, 0.4);
		}
	}
	//SendMessage("CanControl", false);
	//fly = GetComponent(ConstantForce);
	//fly.relativeForce = speedfly;
	//rigidbody.velocity = (transform.TransformDirection(speedfly));
}
	
function OnCollisionEnter (other : Collision) {		// Advanced explosion that starts at missile contact point and decreases damage the farther away object is which is determined by shooting a raycast all at it,
// finding the object we want, then using that point to put into damage vs distance formula.		// NOTE. I don't realy know how well this works but it seams to work fairly well.
	var contactPoint = other.contacts[0];
	var contactRotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
	var contactPosition = contactPoint.point;
	var colliders : Collider[] = Physics.OverlapSphere(contactPosition, damageRange);
	var sendDamage : float;		// Objects recive less damage the farther away they are.
	for (var impact in colliders) {
		if (impact.rigidbody) {
			impact.rigidbody.AddExplosionForce(power, contactPosition, damageRange);	// Exposive force.
		}
		if (impact.transform.parent) {
			if (transform.parent!=impact.transform.parent) {	// See if what we impact is on our team.
				var found : PlayerBasic = impact.transform.GetComponent(PlayerBasic);
				if (found) {
					var hits : RaycastHit[];
					transform.LookAt(impact.transform.position);
					hits = Physics.RaycastAll (transform.position, transform.forward, damageRange);
					for (var i=0;i<hits.length;i++) {
						var hit : RaycastHit = hits[i];
						if (impact.transform==hit.transform) {
							found.SendMessage("ChangeHealth", Mathf.Clamp01((damageRange-Vector3.Distance(hit.point, contactPosition))/damageRange)*damageMax);		// Apply less damage the farther away hit point.
							//(found ? found : find).SendMessage("ChangeHealth", Mathf.Lerp(0, damageMax, (damageRange-Vector3.Distance(hit.point, contactPosition)/damageRange));
						}
					}
				}
			}
		}
	}
	//Instantiate(explosionPrefab, contactPosition, contactRotation);
 	Destroy(gameObject);
}

function Update () {
	if (!GameController.paused) {
		transform.Translate(0, 0, Time.deltaTime*speedTranslate);
//		var xRotate : float;
//		var yRotate : float;
//		var zRotate : float;
//		if (basic.canControl&&homing) {
//			var oldAngleX = transform.eulerAngles.x;		// Save current rotation.
//			var oldAngleY = transform.eulerAngles.y;		// Save current rotation.
//			var oldAngleZ = transform.eulerAngles.z;		// Save current rotation.
//			if (homing) {
//				if (!basic.target) {		// Merge with top? If no target then try to find one.
//					var colliderList = Physics.OverlapSphere(transform.position, sightRange);
//					var objectDistance = Mathf.Infinity;
//					var ourPosition = transform.position;
//					var otherDamage : int;
//					var otherHealth : int;
//					var RotateX : float;
//					var enemyRangeValue : int;		// 
//					for (i=0; i<colliderList.length; i++) {															// Finds all objects near us.
//						var other : Collider = colliderList[i];
//						if (other.CompareTag("Character")||other.CompareTag("Defense")||other.CompareTag("Building")) {		// If they can be damaged.
//							if (team!=other.transform.parent) {
//								var obj : PlayerBasic = other.GetComponent(PlayerBasic);
//								if (obj) {
//									var enemyNear : Collider[] = Physics.OverlapSphere(other.transform.position, damageRange);
//									var price : int;
//									for (near in enemyNear) {
//										if (near.transform.parent) {
//											if (transform.parent!=near.transform.parent) {	// See if what we impact is on our team.
//												price += (near.CompareTag("Objective") ? 4 : (near.CompareTag("Character") ? 3 : (near.CompareTag("Defense") ? 2 : (near.CompareTag("Building") ? 1 : 0))));
//											}
//										}
//									}
//									if (price>enemyRangeValue-(enemyRangeValue/20)) {
//										enemyRangeValue = (price>enemyRangeValue? price : enemyRangeValue);
//										if (obj.healthInt>otherHealth-(otherHealth/10)) {
//											otherHealth = (obj.healthInt>otherHealth ? obj.healthInt : otherHealth);		// Update max health thing.
//											var difference = Vector3.Distance(other.transform.position, transform.position);
//											if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
//												objectDistance = (difference<objectDistance ? difference : objectDistance);		// Have the one that is the closest be the reference.
//												transform.LookAt(other.transform);
//												var hity : RaycastHit;
//												if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hity, Mathf.Infinity)) {
//													if (hity.transform==other.transform) {					// If we are looking right at our potential target.
//														var newX = transform.eulerAngles.x;
//														if (Mathf.Abs((newX-oldAngleX)-180)+10>RotateX) {				// Target the one in front of us.
//															RotateX = Mathf.Abs(oldAngleX-180);
//															basic.target = other.transform;						// If it makes it this far then assign the object to the target.
//														}
//													}
//												}
//											}
//										}
//									}
//								}
//							}
//						}
//					}
//				}
//			}
//			if (basic.canControl) {
//				if (basic.human) {
//					xRotate = oldAngleX+(Input.GetAxis("SpinX")* Time.deltaTime * speedTranslate);
//					yRotate = oldAngleX+(Input.GetAxis("SpinY")* Time.deltaTime * speedTranslate);
//					zRotate = oldAngleX+(Input.GetAxis("SpinZ")* Time.deltaTime * speedTranslate);
//				}
//				else if (!basic.target) {
//					transform.rotation = Quaternion.Euler(oldAngleX, oldAngleY, 0);
//					transform.LookAt(basic.target);
//					var newAngleX = transform.eulerAngles.x;						// Save new rotation.
//					var newAngleY = transform.eulerAngles.y;						// Save new rotation.
//					var spinX = newAngleX+(180-oldAngleX);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
//					var spinY = newAngleY+(180-oldAngleY);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
//					spinX = (spinX>360 ? spinX-360 : (spinX<0 ? spinX+360 : spinX));
//					spinY = (spinY>360 ? spinY-360 : (spinY<0 ? spinY+360 : spinY));
//					spinX = Mathf.RoundToInt(spinX);	// Round value off to see if we are looking at it;
//					spinY = Mathf.RoundToInt(spinY);	// Round value off to see if we are looking at it;
//					var inputX = (spinX>180 ? 1 : (spinX<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
//					var inputY = (spinY>180 ? 1 : (spinY<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
//					var turnX = oldAngleX+(inputX * Time.deltaTime * (Mathf.Abs(180-spinX)<10 ? (Mathf.Abs(180-spinX)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
//					var turnY = oldAngleY+(inputY * Time.deltaTime * (Mathf.Abs(180-spinY)<10 ? (Mathf.Abs(180-spinY)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
//					turnX = Mathf.Clamp(turnX, (inputX>0 ? oldAngleX : (newAngleX>oldAngleX ? newAngleX-360 : newAngleX)), (inputX>0 ? (newAngleX<oldAngleX ? newAngleX+360 : newAngleX) : oldAngleX));		// Depending on which way we turn we clamp it.
//					turnY = Mathf.Clamp(turnY, (inputY>0 ? oldAngleY : (newAngleY>oldAngleY ? newAngleY-360 : newAngleY)), (inputY>0 ? (newAngleY<oldAngleY ? newAngleY+360 : newAngleY) : oldAngleY));		// Depending on which way we turn we clamp it.
//					xRotate = turnX;
//					yRotate = turnY;
//					zRotate = 0;
//				}
//				//transform.rotation = Quaternion.Euler(Vector3(xRotate, yRotate, zRotate));
//			}
//		}
	}
}

function OnDrawGizmos () {
	if (homing) {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireSphere(transform.position, sightRange);
	}
	Gizmos.color = Color(0.2, 0.95, 0.6);
	Gizmos.DrawWireSphere(transform.position, damageRange);
}

@script RequireComponent(ConstantForce)
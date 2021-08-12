#pragma strict
#pragma implicit
#pragma downcast

var sightRange : int;
var target : Transform;
var lookAtSpot : Vector3;

private var teamSide : Transform;
private var basic : PlayerBasic;

private var priority : int;		// Stores the value of the targest threat.

private var teamLayer : LayerMask;

function Start () {
	basic = transform.parent.GetComponent(PlayerBasic);
	sightRange = basic.sightRange;
	teamSide = transform.root;
	teamLayer = ~(1<<LayerMask.NameToLayer(teamSide.name));
}

function FirstCall (canFire : boolean) {
	var oldAngleX = transform.eulerAngles.x;		// Save current rotation.
	var oldAngleY = transform.eulerAngles.y;		// Save current rotation.
	var colliderArray = Physics.OverlapSphere(transform.position, sightRange);
	for (i=0; i<colliderArray.length; i++) {			// Finds all objects near us.
		var otter : Collider = colliderArray[i];
		if (otter.transform.parent) {
			if (otter.transform.parent) {
				if (teamSide!=otter.transform.parent) {			
					if (otter.CompareTag("Character")||otter.CompareTag("Defense")||otter.CompareTag("Building")||otter.CompareTag("Objective")) {		// If they can be damaged.
						var price = (otter.CompareTag("Objective") ? 4 : (otter.CompareTag("Character") ? 3 : (otter.CompareTag("Defense") ? 2 : (otter.CompareTag("Building") ? 1 : 0))));
						if (price>=priority) {
							target = null;
						}
					}
				}
			}
		}
	}
	var hit : RaycastHit;
	if (target) {
		transform.LookAt(target);
		if (basic.human) {
			var xx = transform.localEulerAngles.x;
			transform.localRotation = Quaternion.Euler(xx, 0, 0);
		}
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hit, Mathf.Infinity, teamLayer)) {
			Debug.DrawRay(transform.position, Vector3.forward*100, Color.blue);
			if (hit.transform!=target||hit.distance>sightRange) {		// If we are looking at a friend then don't fire.
				target = null;
			}
		}
	}
	if (!target) {		// Merge with top? If no target then try to find one.
		var colliderList = Physics.OverlapSphere(transform.position, sightRange);
		var other : Collider;
		var objectDistance = Mathf.Infinity;
		var ourPosition = transform.position;
		var otherDamage : int;
		var otherHealth = Mathf.Infinity;
		var yRotate : float;
		priority = 0;
		for (i=0; i<colliderList.length; i++) {															// Finds all objects near us.
			other = colliderList[i];
			if (other.CompareTag("Character")||other.CompareTag("Defense")||other.CompareTag("Objective")) {		// If they can be damaged.
				if (other.transform.root!=teamSide) {
					var difference = Vector3.Distance(other.transform.position, ourPosition);
					if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
						objectDistance = (difference<objectDistance ? difference : objectDistance);									// Have the one that is the closest be the reference.
						transform.LookAt(other.transform);
						var worth = (other.CompareTag("Objective") ? 4 : (other.CompareTag("Character") ? 3 : (other.CompareTag("Defense") ? 2 : (other.CompareTag("Building") ? 1 : 0))));
						if (worth>=priority) {
							priority = worth;		// Weights targets so it shoots at threats before buildings.
							if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hit, Mathf.Infinity, teamLayer)) {
								if (hit.transform==other.transform) {					// If we are looking right at our potential target.
									var newY = transform.localEulerAngles.y;
									if (Mathf.Abs((newY-oldAngleY)-180)+10>yRotate) {				// Target the one in front of us.
										yRotate = Mathf.Abs(oldAngleY-180);
										target = other.transform;						// If it makes it this far then assign the object to the target.
									}
								}
							}
						}
					}
				}
			}
		}
	}
	transform.localRotation = Quaternion.Euler(oldAngleX, oldAngleY, 0);							// Where should we point the target finder laser?
	var turnX = oldAngleX;
	var turnY = oldAngleY;
	if (target) {			// If not human and we have a target then look at it.
		speedTranslate = 120;
		oldAngleX = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Missile(Clone)" ? basic.transform.eulerAngles.x : transform.eulerAngles.x);		// Save current rotation.
		oldAngleY = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Missile(Clone)"||basic.name=="Human" ? basic.transform.eulerAngles.y : transform.eulerAngles.y);		// Save current rotation.
		transform.LookAt(target);
		var newAngleX = transform.eulerAngles.x;								// Save new rotation.
		var newAngleY = transform.eulerAngles.y;								// Save new rotation.
		var spinX = newAngleX+(180-oldAngleX);														// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
		var spinY = newAngleY+(180-oldAngleY);														// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
		spinX = (spinX>360 ? spinX-360 : (spinX<0 ? spinX+360 : spinX));
		spinY = (spinY>360 ? spinY-360 : (spinY<0 ? spinY+360 : spinY));
		var inputX = (spinX>=180 ? 1 : -1);															// This tests to see which direction to turn to reach newAngle fastest.
		var inputY = (spinY>=180 ? 1 : -1);															// This tests to see which direction to turn to reach newAngle fastest.
		turnX = oldAngleX+(inputX * Time.deltaTime * (Mathf.Abs(180-spinX)<10 ? (Mathf.Abs(180-spinX)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));		// Get oldAngle and add speedTranslate and direction to get a new angle.
		turnY = oldAngleY+(inputY * Time.deltaTime * (Mathf.Abs(180-spinY)<10 ? (Mathf.Abs(180-spinY)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));		// Get oldAngle and add speedTranslate and direction to get a new angle.
//		turnX = transform.localEulerAngles.x;
//		turnY = transform.localEulerAngles.y;
//		Debug.Log(turnX+" "+turnY, gameObject.transform.parent);
	}
	if (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Missile(Clone)") {
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		basic.transform.rotation = Quaternion.Euler(turnX, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
	}
	else {
		transform.localRotation = Quaternion.Euler(turnX, turnY, 0);
	}
	lookAtSpot = transform.position+transform.TransformDirection(Vector3(0, 0, 100));
	basic.BroadcastMessage("Call2", lookAtSpot, SendMessageOptions.DontRequireReceiver);
	if (target) {			// If not human and we have a target then look at it.
		basic.BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
	}

}


function SecondCall (canFire : boolean) {
	var oldAngleX = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)" ? basic.transform.eulerAngles.x : transform.localEulerAngles.x);		// Save current rotation.
	var oldAngleY = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Human" ? basic.transform.eulerAngles.y : transform.localEulerAngles.y);		// Save current rotation.
	var turnX = oldAngleX+(Input.GetAxisRaw("SpinX") * Time.deltaTime * 40);
	var turnY = oldAngleY+(Input.GetAxisRaw("SpinY") * Time.deltaTime * 40);
//	Debug.Log((Input.GetAxisRaw("SpinX") * Time.deltaTime * 40)+" "+(Input.GetAxisRaw("SpinY") * Time.deltaTime * 40), gameObject.transform.parent);
	if (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)") {
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		basic.transform.rotation = Quaternion.Euler(turnX, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
//		Debug.Log(""+turnX+" "+turnY, gameObject.transform.parent);
	}
	else if (basic.name=="Human") {
		basic.transform.rotation = Quaternion.Euler(0, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
		transform.localRotation = Quaternion.Euler(turnX, 0, 0);
	}
	else {
		transform.localRotation = Quaternion.Euler(turnX, turnY, 0);
	}
	lookAtSpot = transform.position+transform.TransformDirection(Vector3(0, 0, 100));
	basic.BroadcastMessage("Call2", lookAtSpot, SendMessageOptions.DontRequireReceiver);
	if (canFire) {
		basic.BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
	}
}

function OnDrawGizmos () {
	Gizmos.color = Color.yellow;
	Gizmos.DrawWireSphere(transform.position, sightRange);
}












/*


function FirstCall (canFire : boolean) {
	var oldAngleX = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)" ? basic.transform.eulerAngles.x : transform.localEulerAngles.x);		// Save current rotation.
	var oldAngleY = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Human" ? basic.transform.eulerAngles.y : transform.localEulerAngles.y);		// Save current rotation.
	var colliderArray = Physics.OverlapSphere(transform.position, sightRange);
	for (i=0; i<colliderArray.length; i++) {															// Finds all objects near us.
		var otter : Collider = colliderArray[i];
		if (otter.transform.parent) {
			if (otter.transform.parent) {
				if (teamSide!=otter.transform.parent) {			
					if (otter.CompareTag("Character")||otter.CompareTag("Defense")||otter.CompareTag("Building")||otter.CompareTag("Objective")) {		// If they can be damaged.
						var price = (otter.CompareTag("Objective") ? 4 : (otter.CompareTag("Character") ? 3 : (otter.CompareTag("Defense") ? 2 : (otter.CompareTag("Building") ? 1 : 0))));
						if (price>=priority) {
							target = null;
						}
					}
				}
			}
		}
	}
	var hit : RaycastHit;
	if (target) {
		transform.LookAt(target);
		if (basic.human) {
			var xx = transform.localEulerAngles.x;
			transform.localRotation = Quaternion.Euler(xx, 0, 0);
		}
		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hit, Mathf.Infinity, teamLayer)) {
			Debug.DrawRay(transform.position, Vector3.forward*100, Color.blue);
			if (hit.transform!=target||hit.distance>sightRange) {		// If we are looking at a friend then don't fire.
				target = null;
			}
		}
	}
	if (!target) {		// Merge with top? If no target then try to find one.
		var colliderList = Physics.OverlapSphere(transform.position, sightRange);
		var other : Collider;
		var objectDistance = Mathf.Infinity;
		var ourPosition = transform.position;
		var otherDamage : int;
		var otherHealth = Mathf.Infinity;
		var yRotate : float;
		priority = 0;
		for (i=0; i<colliderList.length; i++) {															// Finds all objects near us.
			other = colliderList[i];
			if (other.CompareTag("Character")||other.CompareTag("Defense")||other.CompareTag("Building")||other.CompareTag("Objective")) {		// If they can be damaged.
				if (other.transform.root!=teamSide) {
					var difference = Vector3.Distance(other.transform.position, ourPosition);
					if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
						objectDistance = (difference<objectDistance ? difference : objectDistance);									// Have the one that is the closest be the reference.
						transform.LookAt(other.transform);
						var worth = (other.CompareTag("Objective") ? 4 : (other.CompareTag("Character") ? 3 : (other.CompareTag("Defense") ? 2 : (other.CompareTag("Building") ? 1 : 0))));
						if (worth>=priority) {
							priority = worth;		// Weights targets so it shoots at threats before buildings.
							if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), hit, Mathf.Infinity, teamLayer)) {
								if (hit.transform==other.transform) {					// If we are looking right at our potential target.
									var newY = transform.localEulerAngles.y;
									if (Mathf.Abs((newY-oldAngleY)-180)+10>yRotate) {				// Target the one in front of us.
										yRotate = Mathf.Abs(oldAngleY-180);
										target = other.transform;						// If it makes it this far then assign the object to the target.
									}
								}
							}
						}
					}
				}
			}
		}
	}
	transform.localRotation = Quaternion.Euler(oldAngleX, oldAngleY, 0);		// Where should we point the target finder laser?
	lookAtSpot = transform.position+transform.TransformDirection(Vector3(0, 0, 100));
	basic.BroadcastMessage("Call2", lookAtSpot);
	var turnX : int;
	var turnY : int;
	var inputX : float;
	var inputY : float;
	if (target&&((!(basic.human)&&basic.canControl)||(basic.human&&!(basic.canControl)))) {			// If not human and we have a target then look at it.
		speedTranslate = 120;
		transform.LookAt(target);
		var newAngleX = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)" ? basic.transform.eulerAngles.x : transform.localEulerAngles.x);						// Save new rotation.
		var newAngleY = (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)"||basic.name=="Human" ? basic.transform.eulerAngles.y : transform.localEulerAngles.y);						// Save new rotation.
		var spinX = newAngleX+(180-oldAngleX);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
		var spinY = newAngleY+(180-oldAngleY);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
		spinX = (spinX>360 ? spinX-360 : (spinX<0 ? spinX+360 : spinX));
		spinY = (spinY>360 ? spinY-360 : (spinY<0 ? spinY+360 : spinY));
		spinX = Mathf.RoundToInt(spinX);	// Round value off to see if we are looking at it;
		spinY = Mathf.RoundToInt(spinY);	// Round value off to see if we are looking at it;
		inputX = (spinX>180 ? 1 : (spinX<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
		inputY = (spinY>180 ? 1 : (spinY<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
		turnX = oldAngleX+(inputX * Time.deltaTime * (Mathf.Abs(180-spinX)<10 ? (Mathf.Abs(180-spinX)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
		turnY = oldAngleY+(inputY * Time.deltaTime * (Mathf.Abs(180-spinY)<10 ? (Mathf.Abs(180-spinY)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
		//turnX = Mathf.Clamp(turnX, (inputX>0 ? oldAngleX : (newAngleX>oldAngleX ? newAngleX-360 : newAngleX)), (inputX>0 ? (newAngleX<oldAngleX ? newAngleX+360 : newAngleX) : oldAngleX));		// Depending on which way we turn we clamp it.
		//turnY = Mathf.Clamp(turnY, (inputY>0 ? oldAngleY : (newAngleY>oldAngleY ? newAngleY-360 : newAngleY)), (inputY>0 ? (newAngleY<oldAngleY ? newAngleY+360 : newAngleY) : oldAngleY));		// Depending on which way we turn we clamp it.
	}
	else if (basic.human&&basic.canControl) {
		turnX = oldAngleX+(Input.GetAxisRaw("SpinX") * Time.deltaTime * 40);
		turnY = oldAngleY+(Input.GetAxisRaw("SpinY") * Time.deltaTime * 40);
		Debug.Log((Input.GetAxisRaw("SpinX") * Time.deltaTime * 40)+" "+(Input.GetAxisRaw("SpinY") * Time.deltaTime * 40), gameObject.transform.parent);
	}
	
	//(basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)" ? basic.transform : transform).localRotation = Quaternion.Euler(turnX, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
	if (basic.name=="Helicopter(Clone)"||basic.name=="Helicopter2(Clone)") {
		transform.localRotation = Quaternion.Euler(0, 0, 0);
		basic.transform.rotation = Quaternion.Euler(turnX, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
//		Debug.Log(basic.transform.localRotation, gameObject.transform.parent);
		Debug.Log(""+turnX+" "+turnY, gameObject.transform.parent);
	}
	else if (basic.name=="Human") {
		basic.transform.rotation = Quaternion.Euler(0, turnY, 0);		// Set final rotation to be between oldAngle and newAngle.
		transform.localRotation = Quaternion.Euler(turnX, 0, 0);
	}
	else {
		transform.localRotation = Quaternion.Euler(turnX, turnY, 0);
	}

	BroadcastMessage("BCall", canFire, SendMessageOptions.DontRequireReceiver);
	if (canFire) {
		basic.BroadcastMessage("Fire");
	}
	basic.target = target;
}
*/

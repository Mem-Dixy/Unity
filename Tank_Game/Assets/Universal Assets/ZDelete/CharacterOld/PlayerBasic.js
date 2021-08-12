#pragma strict
#pragma implicit
#pragma downcast

var healthInt : int;
var sightRange : int;
var bulletDamage : int;
var fireTime : float;	// = 0.05;	.25 Tank
var cameraPositions : float[];	// Used by GameController Script to position camera. 7 numbers, y eulerAngles, thirdPerson camera(xrot, ypos zpos), firstPerson camera(xrot, ypos zpos).
var human : boolean;
var canControl : boolean;		// Public so CharacterTarget Script can reference it.
// Transforms.
var target : Transform;		// Asigned by target finder 1 Script.
var goPath : Transform;		// Asigned by target finder 1 Script.
var explodeFlame : Transform;
var explodeSmoke : Transform;
// AudioClips.
var soundHealth : AudioClip;
var soundPlayerHit : AudioClip;
var soundPlayerExplode : AudioClip;
var soundFire : AudioClip;

private var spinSpeed : int;
var moveSpeed : int;

private var healthMax : int;	// Remember our maximum health limit.
private var startPoint : Vector3;

private var paused : boolean;
private var saveTransformVelocity : Vector3;
private var saveRotateVelocity : Vector3;
private var mainCamera : Transform;

private var useInput : boolean[];


var smooth = 0.3;
private var xVelocity = 0.0;
private var yVelocity = 0.0;
private var zVelocity = 0.0;
var tiltLimit = 60;


var shoot : CharacterTargetFind1;

var inputMoveZ : int;

function Start () {
	useInput = new boolean[6];
	human = (transform.name=="Helicopter(Clone)"||transform.name=="Tank(Clone)"||transform.name=="Human");		// (transform.root.name=="AStartPoint00");
	canControl = (transform.name!="Helicopter(Clone)"&&transform.name!="Tank(Clone)");	// canControl = (transform.root.name!="AStartPoint00");
	healthMax = healthInt;
	startPoint = transform.position;
	mainCamera = camera.main.transform;
	gameObject.layer = transform.root.gameObject.layer;
	shoot = GetComponentInChildren(CharacterTargetFind1);
}

function Update () {
	if (!GameController.paused && canControl) {
		var oldAngleX = transform.eulerAngles.x;		// Save current rotationX.
		var oldAngleY = transform.eulerAngles.y;		// Save current rotationY.
		var oldAngleZ = transform.eulerAngles.z;		// Save current rotationY.

		var euler = transform.eulerAngles;
		var power = Time.deltaTime*spinSpeed;
//		transform.rotation = Quaternion.Euler(Vector3((useInput[0] ? euler.x+Input.GetAxis("SpinX")*power : 0), (useInput[1] ? euler.y+Input.GetAxis("SpinY")*power : 0), (useInput[2] ? euler.z+Input.GetAxis("SpinZ")*power : 0)));
//		transform.rotation = Quaternion.Euler(Vector3((useInput[0] ? euler.x+Input.GetAxis("SpinX") : 0), (useInput[1] ? euler.y+Input.GetAxis("SpinY") : 0), (useInput[2] ? euler.z+Input.GetAxis("SpinZ") : 0))*Time.deltaTime*spinSpeed);
//
//		transform.LookAt((canControl ? target : Vector3(transform.eulerAngles.x+90*Input.GetAxis("SpinX"), transform.eulerAngles.y+90*Input.GetAxis("SpinY"), transform.eulerAngles.z+90*Input.GetAxis("SpinZ"))));
//
//
//		if (canControl) {
//			transform.LookAt(target);
//		}
//		else {
//			transform.Rotate(90*Input.GetAxis("SpinX"), 90*Input.GetAxis("SpinY"), 90*Input.GetAxis("SpinZ"));
//		}
//
//		var xAngle = Mathf.SmoothDampAngle(oldAngleX, transform.eulerAngles.x, xVelocity, smooth, power);
//		var yAngle = Mathf.SmoothDampAngle(oldAngleY, transform.eulerAngles.y, yVelocity, smooth, power);
//		var zAngle = Mathf.SmoothDampAngle(oldAngleZ, transform.eulerAngles.z, zVelocity, smooth, power);
//		transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
//		
//
//

		if (human) {
			inputSpinX = Input.GetAxis("SpinX");
			inputSpinY = Input.GetAxis("SpinY");
			inputSpinZ = Input.GetAxis("SpinZ");
			inputMoveX = Input.GetAxis("XTransform");
			inputMoveY = Input.GetAxis("YTransform");
			inputMoveZ = Input.GetAxis("ZTransform");
		}
		else {
			if (target) {
				transform.LookAt(target);
				var spinX = transform.eulerAngles.x-oldAngleX;							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
				var spinY = transform.eulerAngles.y-oldAngleY;							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
				var spinZ = transform.eulerAngles.z-oldAngleZ;							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
				transform.rotation = Quaternion.Euler(oldAngleX, oldAngleY, oldAngleZ);
				inputSpinX = ((spinX>180 ? spinX-360 : (spinX<-180 ? spinX+360 : spinX))>0 ? 1 : -1);
				inputSpinY = ((spinY>180 ? spinY-360 : (spinY<-180 ? spinY+360 : spinY))>0 ? 1 : -1);
				inputSpinZ = ((spinZ>180 ? spinZ-360 : (spinZ<-180 ? spinZ+360 : spinZ))>0 ? 1 : -1);
			}
			inputMoveX = 0;
			inputMoveY = 0;
			inputMoveZ = 1;			
		}
		inputMoveX = Mathf.Clamp((inputMoveX>180 ? inputMoveX-360 : inputMoveX), (inputMoveX<0||transform.eulerAngles.x>180 ? -tiltLimit : 0), tiltLimit);
//		transform.rotation = Quaternion.Euler(Vector3((useInput[0] ? euler.x+inputSpinX : 0), (useInput[1] ? euler.y+inputSpinY : 0), (useInput[2] ? euler.z+inputSpinZ : 0)));
//		if (name=="Helicopter(Clone)"||name=="Helicopter2(Clone)") {
//			Debug.Log(useInput[2], gameObject.transform.parent);
//		}
//		else {
//			//transform.rotation = Quaternion.Euler(Vector3((useInput[0] ? euler.x+inputSpinX : euler.x), (useInput[1] ? euler.y+inputSpinY : euler.y), (useInput[2] ? euler.z+inputSpinZ : euler.z)));
//		}
		//transform.Translate(Vector3((useInput[3] ? inputMoveX : 0), (useInput[4] ? inputMoveY : 0), (useInput[5] ? inputMoveZ : 0)) * Time.deltaTime * moveSpeed);
//
//
//
//			var target = basic.target;
//			transform.LookAt(target);
//			var speedRotation = speedRotate * Time.deltaTime;
//			clampMin += (xTransform!=0 ? -speedRotation : speedRotation);
//			clampMax += (xTransform!=0 ? speedRotation : -speedRotation);
//			clampMin = Mathf.Clamp(clampMin, -tiltLimit, -tiltLimit/2);
//			clampMax = Mathf.Clamp(clampMax, tiltLimit/2, tiltLimit);
//			var specialClampX = (transform.eulerAngles.x<180);	// Used to see if our angle is above 0, is so then clamp value (done below in clamp statement).
//			var xAngle = Mathf.SmoothDampAngle(oldAngleX,  Mathf.Clamp(transform.eulerAngles.x, (zTransform<0||!specialClampX ? -tiltLimit : 0), tiltLimit), xVelocity, smooth, speedRotate);
//			var yAngle = Mathf.SmoothDampAngle(oldAngleY, transform.eulerAngles.y, yVelocity, smooth, speedRotate);
//			var zAngle = Mathf.SmoothDampAngle(oldAngleZ, transform.eulerAngles.z, zVelocity, smooth, speedRotate);
//			transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);
//		
//
//
//	
//		var newAngleX = transform.eulerAngles.x;						// Save new rotation.
//		var spinX = newAngleX+(180-oldAngleX);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
//		spinX = Mathf.RoundToInt(spinX>360 ? spinX-360 : (spinX<0 ? spinX+360 : spinX));
//		var inputX = (spinX>180 ? 1 : (spinX<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
//		
//		var turnX = oldAngleX+(inputX * Time.deltaTime * (Mathf.Abs(180-spinX)<10 ? (Mathf.Abs(180-spinX)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));
//		turnX = Mathf.Clamp(turnX, (inputX>0 ? oldAngleX : (newAngleX>oldAngleX ? newAngleX-360 : newAngleX)), (inputX>0 ? (newAngleX<oldAngleX ? newAngleX+360 : newAngleX) : oldAngleX));		// Depending on which way we turn we clamp it.
//		
//		
//		var newAngleX = transform.eulerAngles.x;						// Save new rotation.
//		var newAngleY = transform.eulerAngles.y;						// Save new rotation.
//		var spinX = newAngleX+(180-oldAngleX);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
//		var spinY = newAngleY+(180-oldAngleY);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
//		spinX = Mathf.RoundToInt(spinX>360 ? spinX-360 : (spinX<0 ? spinX+360 : spinX));
//		spinY = Mathf.RoundToInt(spinY>360 ? spinY-360 : (spinY<0 ? spinY+360 : spinY));
//	//	spinX = Mathf.RoundToInt(spinX);	// Round value off to see if we are looking at it;
//	//	spinY = Mathf.RoundToInt(spinY);	// Round value off to see if we are looking at it;
//		var inputX = (spinX>180 ? 1 : (spinX<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
//		var inputY = (spinY>180 ? 1 : (spinY<180 ? -1 : 0));		// This tests to see which direction to turn to reach newAngle fastest.
//		var turnX = oldAngleX+(inputX * Time.deltaTime * (Mathf.Abs(180-spinX)<10 ? (Mathf.Abs(180-spinX)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
//		var turnY = oldAngleY+(inputY * Time.deltaTime * (Mathf.Abs(180-spinY)<10 ? (Mathf.Abs(180-spinY)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));			// Get oldAngle and add speedTranslate and direction to get a new angle.
//		turnX = Mathf.Clamp(turnX, (inputX>0 ? oldAngleX : (newAngleX>oldAngleX ? newAngleX-360 : newAngleX)), (inputX>0 ? (newAngleX<oldAngleX ? newAngleX+360 : newAngleX) : oldAngleX));		// Depending on which way we turn we clamp it.
//		turnY = Mathf.Clamp(turnY, (inputY>0 ? oldAngleY : (newAngleY>oldAngleY ? newAngleY-360 : newAngleY)), (inputY>0 ? (newAngleY<oldAngleY ? newAngleY+360 : newAngleY) : oldAngleY));		// Depending on which way we turn we clamp it.
	
	}
}

function LateUpdate () {
	if (GameController.paused) {
		if (!paused&&rigidbody) {
			saveTransformVelocity = rigidbody.velocity;
			saveRotateVelocity = rigidbody.angularVelocity;
			rigidbody.isKinematic = true;
			paused = true;
		}
	}
	else {
		if (paused&&rigidbody) {
			rigidbody.isKinematic = false;
			rigidbody.velocity = saveTransformVelocity;
			rigidbody.angularVelocity = saveRotateVelocity;
			paused = false;
		}
		if ((!human&&canControl)||(human&&!canControl)) {			// If not human and we have a target then look at it.
			BroadcastMessage("FirstCall", true, SendMessageOptions.DontRequireReceiver);
		}
		else if (human&&canControl) {
			//BroadcastMessage("FirstCall", (human&&canControl ? Input.GetButton("Fire1") : true), SendMessageOptions.DontRequireReceiver);//(target ? true : false))		// If not player and have target then always try to shoot.
			BroadcastMessage("SecondCall", Input.GetButton("Fire1"));
		}
	}
}

function ResetPosition () {
	transform.rotation = Quaternion.identity;
	transform.position = startPoint;
}

function ChangeHealth (bulletHead : int) {		// Sent from Missile.
	healthInt -= bulletHead;			// TODO, Add fancy distance bulletHead thing.		// Take away health from player's health.
	if (healthInt>0) {
		audio.PlayOneShot(soundPlayerHit);
	}
	else {
		audio.PlayOneShot(soundPlayerExplode);
		Instantiate(explodeFlame, transform.position, transform.rotation);
		Instantiate(explodeSmoke, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}

function SetValue (incoming : Vector2) {
	spinSpeed = incoming.x;
	moveSpeed = incoming.y;
}

function SetControls (incoming : boolean[]) {
	if (incoming.length==6) {
		useInput = new boolean[6];
		for (i=0; i<useInput.length; i++) {
			useInput[i] = incoming[i];
		}
	}
	else {
		Debug.Log("This length must be 6.", gameObject);
	}
}
// Messages sent to us from GameController Script and are sent off to our partner script.
function CanControl (incoming : boolean) {
	canControl = incoming;
}

function IsHuman (incoming : boolean) {
	while (!human) {	// Waits for human to become true then changes back to false;
		yield;
	}
	human = incoming;
}

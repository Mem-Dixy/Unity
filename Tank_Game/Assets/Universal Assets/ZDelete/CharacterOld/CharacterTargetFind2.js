#pragma strict
#pragma implicit
#pragma downcast

function Call2 (lookAtSpot : Vector3) {
	var speedTranslate = 120;
	var oldAngle = transform.localEulerAngles.y;				// Save current rotation.
	transform.LookAt(lookAtSpot);								// Look at spot.
	var newAngle = transform.localEulerAngles.y;				// Save new rotation..
	var spin = newAngle+(180-oldAngle);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
	spin = (spin>360 ? spin-360 : (spin<0 ? spin+360 : spin));
	var input = (spin>180 ? 1 : (spin<180 ? -1 : 0));			// This tests to see which direction to turn to reach newAngle fastest.
	var turn = oldAngle+(input * Time.deltaTime * 120);			// Get oldAngle and add speed and direction to get a new angle.
	turn = oldAngle+(input * Time.deltaTime * (Mathf.Abs(180-spin)<10 ? (Mathf.Abs(180-spin)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));// Get oldAngle and add speedTranslate and direction to get a new angle.
//	turn = Mathf.Clamp(turn, (input>0 ? oldAngle : newAngle), (input>0 ? newAngle : oldAngle));		// Depending on which way we turn we clamp it.
	transform.localRotation = Quaternion.Euler(0, turn, 0);		// Set final rotation to be between oldAngle and newAngle.
	BroadcastMessage("Call3", lookAtSpot);
}

//var smooth = 0.3;
//private var xVelocity = 0.0;
//private var yVelocity = 0.0;
//private var zVelocity = 0.0;
//
//		if (!canControl) {/*
//			var target = basic.goPath;
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
//		*/}

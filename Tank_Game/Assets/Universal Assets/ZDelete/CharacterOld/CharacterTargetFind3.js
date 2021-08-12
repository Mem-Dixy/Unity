#pragma strict
#pragma implicit
#pragma downcast

function Call3 (lookAtSpot : Vector3) {
	var speedTranslate = 120;
	var oldAngle = transform.localEulerAngles.x;				// Save current rotation.
	transform.LookAt(lookAtSpot);								// Look at spot.
	var newAngle = transform.localEulerAngles.x;				// Save new rotation.
	var spin = newAngle+(180-oldAngle);							// Spin oldAngle to 180 and Convert newAngle to corisponding rotation.
	spin = (spin>360 ? spin-360 : (spin<0 ? spin+360 : spin));
	var input = (spin>180 ? 1 : (spin<180 ? -1 : 0));			// This tests to see which direction to turn to reach newAngle fastest.
	var turn = oldAngle+(input * Time.deltaTime * 120);			// Get oldAngle and add speed and direction to get a new angle.
	turn = oldAngle+(input * Time.deltaTime * (Mathf.Abs(180-spin)<10 ? (Mathf.Abs(180-spin)*(speedTranslate/10)*speedTranslate)/speedTranslate : speedTranslate*2));// Get oldAngle and add speedTranslate and direction to get a new angle.
//	turn = Mathf.Clamp(turn, (input>0 ? oldAngle : newAngle), (input>0 ? newAngle : oldAngle));		// Depending on which way we turn we clamp it.
	transform.localRotation = Quaternion.Euler(turn, 0, 0);		// Set final rotation to be between oldAngle and newAngle.
}

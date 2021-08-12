#pragma strict
#pragma implicit
#pragma downcast
// Attach this script to an orthographic camera.
var spinSpeed = 60;

private var chicken : Transform;      // The chicken we will move.
private var offSet : Vector3;      // The chicken's position relative to the mouse position.

function Update () {
	var ray = camera.ScreenPointToRay(Input.mousePosition);      // Gets the mouse position in the form of a ray.
	if (Input.GetMouseButtonDown(0)) {      // If we click the mouse...
		var hit : RaycastHit; 
		if (Physics.Raycast(ray, hit, Mathf.Infinity)) {      // Then see if an chicken is beneath us using raycasting.
			chicken = hit.transform;      // If we hit an chicken then hold on to the chicken.
			offSet = chicken.position-ray.origin;      // This is so when you click on an chicken its center does not align with mouse position.
			if (chicken.rigidbody) {
				chicken.rigidbody.isKinematic = true;
			}
		}
	}
	else if (Input.GetMouseButtonUp(0)) {
		if (chicken.rigidbody) {
			chicken.rigidbody.isKinematic = false;
		}
		chicken = null;      // Let go of the chicken.
	}
	if (chicken) {
		chicken.position = Vector3(ray.origin.x+offSet.x, chicken.position.y, ray.origin.z+offSet.z);      // Only move the chicken on a 2D plane.
		if (Input.GetButton("Fire2")) {
			chicken.Rotate(0, spinSpeed*Time.deltaTime, 0);
		}
	}
}
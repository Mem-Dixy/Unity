#pragma strict
#pragma implicit
#pragma downcast		
// Attach this script to an orthographic camera.

public var object : Transform;		// The object that we want to move.
public var offSet = Vector3(0, 0, 10);		// Move the center of the object away from the mouse pointer.

function Update () {
	if (object) {
//		object.position = camera.main.ScreenToWorldPoint(Input.mousePosition)+camera.main.transform.TransformDirection(offSet);
//		var ray = camera.main.ScreenPointToRay(Input.mousePosition);
//		object.position = ray.origin+camera.main.transform.TransformDirection(offSet);

		object.position = camera.ScreenPointToRay(Input.mousePosition).origin+camera.transform.TransformDirection(offSet);

//		object.position = camera.main.ViewportToWorldPoint(camera.main.ScreenToViewportPoint(Input.mousePosition))+camera.main.transform.TransformDirection(offSet);
	}
}
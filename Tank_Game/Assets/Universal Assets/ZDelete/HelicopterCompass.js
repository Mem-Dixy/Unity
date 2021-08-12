#pragma strict
#pragma implicit
#pragma downcast

private var target : Transform;
private var startPoint : Vector3;

function Start () {
	target = transform.parent.transform;
	startPoint = transform.localPosition;
}

function LateUpdate () {
	transform.localPosition = startPoint;
	transform.rotation = Quaternion.identity;
	var y = target.transform.eulerAngles.y;			// Get rotation value;
	transform.Rotate(0, y, 0);		// Do angles first //Move camera to correct angle.
}	

#pragma strict
#pragma implicit
#pragma downcast

var placeRed : Transform;		// Transparent red object.
var placeGreen : Transform;		// Transparent green object.

private var placement : boolean;	// Is there an open place underneeth us?
//private var condition : Transform;	// The transparent object.
private var array : Transform[];	// Shoot raycast from.
private var list : Transform[];		// ondition objects.

function Start () {
	Generated();
}

function Generated () : boolean {
	var found = GetComponentsInChildren(Transform);
	array = new Transform[found.length-1];
	var counter : int;		// Used to exclude our selfs in search.
	for (i=0; i<found.length; i++) {
		if (found[i]!=transform) {
			array[counter] = found[i];
			counter++;
		}
	}
	list = new Transform[array.length];
	print("H");
	return Check();
}

function Check () : boolean {
	placement = true;
	var hit : RaycastHit;
	for (i=0; i<array.length; i++) {
		if (Physics.Raycast(array[i].position+Vector3(0, 8, 0), -transform.up, hit, 100)) {//, teamLayer)) {
			Debug.DrawLine(array[i].position, hit.point, Color.yellow);
			if (hit.transform.name!="-0") {
				placement = false;
			}
		}
		else {
			placement = false;
		}
	}
	for (i=0; i<list.length; i++) {
		if (list[i]!=(placement ? placeGreen : placeRed)) {
			if (list[i]) {
				Destroy(list[i].gameObject);
			}
			list[i] = Instantiate((placement ? placeGreen : placeRed), array[i].position+Vector3(0, 4, 0), transform.rotation);
			list[i].parent = transform;
		}
	}
	return placement;
}

function Set (teamLayer : LayerMask, title : String) {
	yield;		// Wait for Start to finish.
	var hit : RaycastHit;
	for (i=0; i<list.length; i++) {
		if (list[i]) {
			Destroy(list[i].gameObject);
		}
		else {
			list[i] = Instantiate(placeGreen, array[i].position, transform.rotation);
			list[i].parent = transform;
		}
		if (Physics.Raycast(array[i].position+Vector3(0, 8, 0), -transform.up, hit, 100)) {//, teamLayer)) {
			hit.transform.name = title;
		}
	}
}

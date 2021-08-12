#pragma strict
#pragma implicit
#pragma downcast

var camMain : Camera;

function Start () {
	camMain = Camera.main;
}

function Update () {
	if (camMain) {
		transform.LookAt(camMain.transform);
	}
}

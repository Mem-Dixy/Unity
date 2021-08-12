#pragma strict
#pragma implicit
#pragma downcast

var big : float;
var small : float;

private var ratio : float;
private var found : GameController;

var isRadar : boolean;
var show : boolean;

function Start () {
	found = camera.main.GetComponent(GameController);
	if (!found) {
		Debug.Log("Radar could not find GameController script");
	}
	ratio = (0.0+Screen.width)/Screen.height;
	big = 512;	//120;
	small = 128;//40;
	if (isRadar) {
		camera.orthographicSize = small;	// 128.
		camera.rect = Rect(1-(0.4/ratio), 0, 0.4/ratio, 0.4);
	}
	else {
		camera.orthographicSize = big;	// 512.
		camera.rect = Rect((1-(0.9/ratio))/2, 0.05, 0.9/ratio, 0.9);
	}
}

function Update () {
	if (!isRadar) {
		show = (Input.GetButtonDown("Map") ? !show : show);
		camera.enabled = show;		// Potention for performence enhancement but unnecessary at the moment.
	}
}

function LateUpdate () {
	if (found.basic) {
		transform.rotation = Quaternion.Euler(90, (isRadar ? found.basic.transform.eulerAngles.y : 0), 0);
		transform.position = found.basic.transform.position+Vector3(0, 512, 0);
	}
	else {
			found = camera.main.GetComponent(GameController);
	}
}
#pragma strict
#pragma implicit
#pragma downcast

var speedTranslate : int = 10;
var speedRotate : int = 100;

private var basic : PlayerBasic;
private var game : GameController;
private var arrow : HelicopterCompass;

function Start () {
	arrow = GetComponentInChildren(HelicopterCompass);
	basic = GetComponent(PlayerBasic);
//	basic.SendMessage("SetValue", Vector2(speedRotate, speedTranslate));
//	var useInput = new boolean[6];
//	useInput[0] = false;
//	useInput[1] = true;
//	useInput[2] = false;
//	useInput[3] = true;
//	useInput[4] = false;
//	useInput[5] = true;
//	basic.SendMessage("SetControls", useInput);
	game = camera.main.GetComponent(GameController);
	SendMessage("CanControl", true);
}

function OnTriggerEnter (other : Collider) {
	if (other.CompareTag("Character")) {
		//if (other.name=="Helicopter(Clone)"||other.name=="Tank(Clone)") {
		//if (transform.root==other.transform.root) {
		var found : PlayerBasic = other.GetComponent(PlayerBasic);
		if (found.human) {
			game.SwitchPlayer(found);
		}
	}
}

function Update () {
	if (!GameController.paused) {
		//transform.rotation = Quaternion.Euler(Vector3(0, transform.eulerAngles.y+(Input.GetAxis("SpinY")*Time.deltaTime*speedRotate), 0));
		transform.Translate(Vector3(Input.GetAxis("XTransform"), 0, Input.GetAxis("ZTransform")).normalized * Time.deltaTime * speedTranslate, arrow.transform);
	}
}

@script RequireComponent(PlayerBasic)
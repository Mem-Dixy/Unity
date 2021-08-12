#pragma strict
#pragma implicit
#pragma downcast

private var healthMax : float;
private var basic : PlayerBasic;

function Start () {
	basic = transform.parent.parent.GetComponent(PlayerBasic);
	if (basic) {
		healthMax = basic.healthInt;
	}
}

function Update () {
	if (basic) {
		if (basic.healthInt>=0) {
			transform.localScale.x = (basic.healthInt/healthMax);
		}
	}
}
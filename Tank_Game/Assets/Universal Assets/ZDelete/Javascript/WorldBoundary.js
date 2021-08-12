#pragma strict
#pragma implicit
#pragma downcast

// If worried about it going so fast it flys out of level then add an array that stores objects that fly away
// and if they don't enter into sphere again then delete them after a short pause;
function OnTriggerExit (other : Collider) {
	var basic : PlayerBasic = other.GetComponent(PlayerBasic);
	if (basic) {
		basic.SendMessage("ResetPosition");
	}
	else {
		Destroy(other.gameObject);
	}
}

//#pragma strict
//
//var regenerateTime : int;
//var teamItem : Transform;
//var item : Transform;		// Public so GameController can build list of team members.
//
//var attack : Transform;
//var defense : Transform;
//	
//private var regenerateWait : float;
//private var guardian : Transform;
//
//var endless : boolean;
//private var once = true;
//
//var generateLimit : int;
//
//private var teamLayer : LayerMask;
//
//function Start () {
//	guardian = gameObject.transform.parent.parent;
//	regenerateWait = regenerateTime;
//	if (!teamItem) {
//		if (gameObject.tag=="Character") {
//			teamItem = attack;
//			regenerateTime = 4;
//		}
//		else if (gameObject.tag=="Defense") {
//			teamItem = defense;
//			regenerateTime = 10;
//		}
//	}
//	if (gameObject.tag=="Character"&&gameObject.name!="Helicopter"&&gameObject.name!="Player") {
//		generateLimit = 5;
//	}
//	else {
//		generateLimit = 1;
//	}
//	regenerateTime = 5;
//	regenerateWait = regenerateTime;
//	teamLayer = transform.root.gameObject.layer;
//}
//
//function Update () {
//	if (!GameController.paused) {
//		if (generateLimit>0) {
//			if (regenerateWait>=regenerateTime) {
//				generateLimit--;
//				item = Instantiate(teamItem, transform.position, transform.rotation);
//				item.parent = guardian;
//				item.gameObject.layer = teamLayer;
//				regenerateWait = 0;
//			}
//			else {
//				regenerateWait += Time.deltaTime;
//			}
//		}
//	}
//}
//
//function OnDrawGizmos () {
//	Gizmos.color = Color.green;
//	Gizmos.DrawWireCube(transform.position, Vector3(1, 1, 1));
//}
/*
private var savename  = "";

function Start () {
	savename = SaveName(transform, "pickup");  // Generate a Unique Savename.
	if (PlayerPrefs.GetInt(savename,1) == 0 ) {
		Destroy(this.gameObject);
	}
}

function OnTriggerEnter () {
	PlayerPrefs.SetInt(savename, 0);
	Destroy(gameObject);
}

static function SaveName (object : Transform, ilabel : String) {
	return ilabel + "." + Application.loadedLevelName + "." + parseInt(object.transform.position.x) + "." + parseInt(object.transform.position.y) + "." + parseInt(object.transform.position.z);
}

/////


private var savename  = "";

function Start () {
	savename = SaveName(transform, "pickup");  // Generate a Unique Savename.
	if (PlayerPrefs.GetInt(savename,1) == 0 ) {
		Destroy(this.gameObject);
	}
}

function OnTriggerEnter () {
	PlayerPrefs.SetInt(savename, 0);
	Destroy(gameObject);
}

static function SaveName (object : Transform, ilabel : String) {
	return ilabel + "." + Application.loadedLevelName + "." + parseInt(object.transform.position.x) + "." + parseInt(object.transform.position.y) + "." + parseInt(object.transform.position.z);
}*/
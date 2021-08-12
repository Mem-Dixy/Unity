using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


// Simple Pickup code -- .js -- for Unity 
/*
private var savename  = "";

function Start ()
{
savename = savename(this.gameObject, "pickup");  // Generate a Unique Savename.
if (PlayerPrefs.GetInt(savename,1) == 0 )
{   Destroy(this.gameObject); }
}


function OnTriggerEnter (hitby : Collider)
{

{// Put some code here to check to see if the item that triggered you can pick you up

PlayerPrefs.SetInt(savename, 0);
Destroy(this.gameObject);
// do some "you picked me up!" sound effects, etc
}

static function savename (object : GameObject, ilabel : String)
{ // Call this to get your unique save name!
// this builds a unique savename by stringing together the object's level name, X, Y, and Z locs (interegers of), and a prefix that is defined
// by the object itself. The idea is that this is a uniqueish enough key for saving the status of an in-game object.
// Ideally this function is a master function somewhere, rather than in every type of pick-up script you have.

return ilabel + "." + Application.loadedLevelName + "." + parseInt(object.transform.position.x) + "." + parseInt(object.transform.position.y) + "." + parseInt(object.transform.position.z); // this makes a unique name for this object. Kinda.
}

////////

private var savename  = "";

function Start () {
	savename = SaveName(this.gameObject, "pickup");  // Generate a Unique Savename.
	if (PlayerPrefs.GetInt(savename,1) == 0 ) {
		Destroy(this.gameObject); }
	}
}

function OnTriggerEnter (hitby : Collider) {
	PlayerPrefs.SetInt(savename, 0);
	Destroy(gameObject);
}

static function SaveName (object : GameObject, ilabel : String) {
	return ilabel + "." + Application.loadedLevelName + "." + parseInt(object.transform.position.x) + "." + parseInt(object.transform.position.y) + "." + parseInt(object.transform.position.z);
}*/
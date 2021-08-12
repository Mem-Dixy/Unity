#pragma strict
#pragma implicit
#pragma downcast
import System.IO; 	// Used for getting text files.
import System;		// Used for getting the date 


var gUISkin : GUISkin;								// The GUISkin we are using.

// Main Menu.
var buttonSize : Vector2;							// The size that every button will be.
var windowPadding : Vector2;						// How much spacing is between the buttons and the window.
var scrollViewSize : Vector4[];						// The sizes of all the ScrollViews in each menu and submenu.

private var scrollPosition = Vector2.zero;			// This is the curent ScrollView object being used.
private var scrollBoxSize : Vector4;				// The size of the current ScrollView being used. This also helps with the size of the window.
// Growing window box.
private var windowPropertiesValue : Vector4;		// Actual size and position of window.
private var windowPropertiesTarget : Vector4;		// Size and postion we want the window to be at.

private var menuHierarchyArray : int[];				// This stores all the info on what menus to show.
private var menuHierarchyActive : int;				// This tells us what menu we are currently using.



var moveSpeed : int;

var levelData : TextAsset;
var sampleLevel : TextAsset[];
// Loaded Level Items.
var grass : Transform;
var road : Transform;
var road1 : Transform;
var road2 : Transform;
var road3 : Transform;
var road4 : Transform;

var tank : Transform;
var turrent : Transform;
var heli : Transform;
var heli2 : Transform;
var person : Transform;

var house : Transform;
var fence1 : Transform;
var fence2 : Transform;


var lenghtX : int;
var lenghtY : int;
var lenghtZ : int;

var ground : GameObject[];

var edit : Transform;
private var check : Placer;		// Stores the tile we are holding.
private var test : boolean;		// The result of the test of able to drop tile.
private var remember : Transform;	// Keeps a check of tile under us for performace.

var thing : Transform;		// This is what follows the camera/current tile.

var camMain : Camera;
private var teamLayer : LayerMask;

private var open : boolean;
private var save : boolean;
var stringToEdit : String;
var title = "TestFile";

private var charType = "-";						// Public so players can get value.
private var charInt : int;						// Public so players can get value.
private var field : int;		// Tells us what floor we are on.

function Start () {
	PlayerPrefs.SetInt("Player Score", 10);
	print (PlayerPrefs.GetInt("Player Score"));
	var word : String;	
	for (i=0; i<1; i++) {
		for (j=0; j<4; j++) {
			for (k=0; k<4; k++) {
				word += "-0,";
			}
			word += "\n";
		}
		word += "*";
	}
	camMain.transform.rotation = Quaternion.Euler(Vector3.right*90);
	Open(word);

	word = "";	
	for (i=0; i<4; i++) {
		for (j=0; j<4; j++) {
			for (k=0; k<4; k++) {
				word += "-0,";
			}
			word += "\n";
		}
		word += "*";
	}
	PlayerPrefs.SetString("DI", word);

	word = "";
	for (i=0; i<2; i++) {
		for (j=0; j<8; j++) {
			for (k=0; k<4; k++) {
				word += "-0,";
			}
			word += "\n";
		}
		word += "*";
	}
	PlayerPrefs.SetString("HI", word);
	
	PlayerPrefs.SetString("Levels", "DI/HI");

}

function Update () {
	if (open) {		// This is what we see when we start
		scrollBoxSize.x = 350;
		scrollBoxSize.y = 300;
		scrollBoxSize.z = 300;
		scrollBoxSize.w = 200;
	}
	else if (save) {
		scrollBoxSize.x = 350;
		scrollBoxSize.y = 300;
		scrollBoxSize.z = 300;
		scrollBoxSize.w = 200;
	}
	windowPropertiesTarget.x = (Screen.width/2)+(-windowPropertiesValue.z/2);		// Center the window by taking the center of the screen minus window width.
	windowPropertiesTarget.y = (Screen.height/2)+(-windowPropertiesValue.w/2);		// Center the window by taking the center of the screen minus window width.
	windowPropertiesTarget.z = scrollBoxSize.x;		// Makes the window as big as content plus window padding.
	windowPropertiesTarget.w = scrollBoxSize.y;		// Makes the window as big as content plus window padding.
	windowPropertiesValue.x += (windowPropertiesValue.x==windowPropertiesTarget.x ? 0 : (windowPropertiesValue.x<windowPropertiesTarget.x ? 1 : -1));
	windowPropertiesValue.y += (windowPropertiesValue.y==windowPropertiesTarget.y ? 0 : (windowPropertiesValue.y<windowPropertiesTarget.y ? 1 : -1));
	windowPropertiesValue.z += (windowPropertiesValue.z==windowPropertiesTarget.z ? 0 : (windowPropertiesValue.z<windowPropertiesTarget.z ? 1 : -1));
	windowPropertiesValue.w += (windowPropertiesValue.w==windowPropertiesTarget.w ? 0 : (windowPropertiesValue.w<windowPropertiesTarget.w ? 1 : -1));
//}
//function Update () {
//		var ray = camera.ScreenPointToRay(Input.mousePosition);      // Gets the mouse position in the form of a ray.
//	if (Input.GetMouseButtonDown(0)) {      // If we click the mouse...
//		var hit : RaycastHit; 
//		if (Physics.Raycast(ray, hit, Mathf.Infinity)) {      // Then see if an chicken is beneath us using raycasting.
//			chicken = hit.transform;      // If we hit an chicken then hold on to the chicken.
//			offSet = chicken.position-ray.origin;      // This is so when you click on an chicken its center does not align with mouse position.
//			if (chicken.rigidbody) {
//				chicken.rigidbody.isKinematic = true;
//			}
//		}
//	}
//	else if (Input.GetMouseButtonUp(0)) {
//		if (chicken.rigidbody) {
//			chicken.rigidbody.isKinematic = false;
//		}
//		chicken = null;      // Let go of the chicken.
//	}
//	if (chicken) {
//		chicken.position = Vector3(ray.origin.x+offSet.x, chicken.position.y, ray.origin.z+offSet.z);      // Only move the chicken on a 2D plane.
//		if (Input.GetButton("Fire2")) {
//			chicken.Rotate(0, spinSpeed*Time.deltaTime, 0);
//		}
//	}

	var euler = transform.eulerAngles;
	var inputMoveX = Input.GetAxis("XTransform");
	var inputMoveY = Input.GetAxis("YTransform");
	var inputMoveZ = Input.GetAxis("ZTransform");
	//camMain.transform.rotation = Quaternion.Euler(Vector3(euler.x+inputSpinX, euler.y+inputSpinY, euler.z+inputSpinZ ));
	camMain.transform.Translate(Vector3(inputMoveX, inputMoveZ, 0) * Time.deltaTime * moveSpeed);
	camMain.orthographicSize += -inputMoveY * Time.deltaTime * moveSpeed;
	camMain.orthographicSize = Mathf.Clamp(camMain.orthographicSize, 8, 64);
	var hit : RaycastHit;
	if (thing) {
		if (Input.GetKeyDown(KeyCode.Backspace)||Input.GetKeyDown(KeyCode.Delete)) {
			Destroy(thing.gameObject);
		}
		if (Input.GetButtonDown("Fire2")) {
			charInt = (charInt<7 ? charInt+2 : 2);
			thing.transform.Rotate(0, 90, 0, Space.World);
			test = check.Check();//teamLayer);
		}
	}
	var ray = camMain.ScreenPointToRay(Input.mousePosition);      // Gets the mouse position in the form of a ray.
	if (Physics.Raycast(ray, hit, 100)) {//, teamLayer)) {
		Debug.DrawLine(camMain.ScreenToWorldPoint(Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height), 0)), hit.point, Color.red);
		//			Debug.DrawLine(cam.ScreenToWorldPoint(Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height), 0)), hit.point, Color.red);
		if (remember!=hit.transform) {
			remember = hit.transform;
			if (thing) {
				thing.transform.position = hit.transform.position+Vector3(0, 5, 0);		// Stick to grid.
				check = thing.transform.GetComponent(Placer);
				test = check.Check();//teamLayer);
			}
		}
		if (thing) {
			if (Input.GetMouseButtonDown(0)) {
				if (test) {
					check.Set(teamLayer, charType);
					thing.transform.parent = hit.transform;
					thing.transform.position = hit.transform.position+Vector3(0, 4, 0);
					thing = null;
				}
			}
		}
		else {
			if (Input.GetMouseButtonDown(0)&&hit.transform.name!="-0") {
				var pit : RaycastHit;
				if (Physics.Raycast(ray, pit, Mathf.Infinity, 13)) {
					thing = pit.transform;		// Get tile.
					Debug.Log("HI", pit.transform);
					charType = hit.transform.name;
					check = thing.transform.GetComponent(Placer);
					check.Set(teamLayer, "-0");
				}
			}
			if (Input.GetButtonDown("Fire3")&&hit.transform.name!="-0") {
				Tile(hit.transform.name, hit.transform.position, false);		// Get tile.
			}
		}
	}
	else if (thing) {
		thing.position = ray.origin+Vector3(0, -3, 0);
		test = false;
	}
}

function OnGUI () {
	GUI.skin = gUISkin;		// This tells how the text will display.	
	if (GUI.Button(Rect(700, 0, 150, 25), "New", "Plain")) {		//
		print("New");
		for (i=0; i<ground.length; i++) {
			print(i);
			Destroy(ground[i].gameObject);
		}
	}
	
	if (GUI.Button(Rect(700, 25, 150, 25), "Open", "Plain")) {		// Gets level data form player prefs.
		print("Open");
		open = !open;
	}
	if (GUI.Button(Rect(700, 50, 150, 25), "Save", "Plain")) {		// Stores level in player prefs.
		save = !save;
	}	
	if (save) {
		GUI.Box(Rect(windowPropertiesValue.x-windowPadding.x, windowPropertiesValue.y-windowPadding.y, windowPropertiesValue.z+(windowPadding.x*2), windowPropertiesValue.w+(windowPadding.y*2)), "");	// Empty 
	//	GUI.Box(Rect(windowPropertiesValue.x, windowPropertiesValue.y-5, scrollBoxSize.x+10, scrollBoxSize.y+10), "");		// Outline of selected option;
		GUI.Label(Rect(windowPropertiesValue.x, windowPropertiesValue.y, scrollBoxSize.x, 75), "Select a file to open it.\nMake sure you save your\nlevel befor you do.");
		stringToEdit = GUI.TextField(Rect(windowPropertiesValue.x, windowPropertiesValue.y+100, 235, 25), stringToEdit, 16);
		GUI.Label(Rect(windowPropertiesValue.x+235, windowPropertiesValue.y+100, scrollBoxSize.x, 25), ".txt");
		
		scrollPosition = GUI.BeginScrollView(Rect(windowPropertiesValue.x, windowPropertiesValue.y, scrollBoxSize.x, scrollBoxSize.y), scrollPosition, Rect(windowPropertiesValue.x, windowPropertiesValue.y, scrollBoxSize.z, scrollBoxSize.w));
		var levelS = PlayerPrefs.GetString("Levels", "Null").Split("/"[0]);
		if (levelS[0]!="Null") {
			for (i=0; i<levelS.length; i++) {
				if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+5+(25*i), 150, 25), levelS[i])) {		// Gets levelS data form player prefs.
					print(levelS[i]);
					Open(PlayerPrefs.GetString(levelS[i]));
				}
			}
			if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+5, 150, 25), "Push")) {
				print("Save");
				var output = "Tank Game Version 7 Saved On : "+DateTime.Now+"\n";
				for (i=0; i<lenghtX; i++) {		// Ground.
					output += "*";
					for (j=0; j<lenghtY; j++) {
						for (k=0; k<lenghtZ; k++) {
							output += ground[(i*lenghtY*lenghtZ+j*lenghtZ+k)].name+",";
						}
						output += "\n";
					}
				}
				title = stringToEdit;
				PlayerPrefs.SetString(title, output);
	
			}
		}
		GUI.EndScrollView();
	}

	if (open) {
		GUI.Box(Rect(windowPropertiesValue.x, windowPropertiesValue.y-105, scrollBoxSize.x+10, scrollBoxSize.y+120), "");		// Outline of selected option;
		GUI.Label(Rect(windowPropertiesValue.x, windowPropertiesValue.y-100, scrollBoxSize.x, 80), "Select a file to open it.\nMake sure you save your\nlevel befor you do.");
		title = GUI.TextField(Rect(windowPropertiesValue.x, windowPropertiesValue.y-20, 235, 25), title, 16);
		GUI.Label(Rect(windowPropertiesValue.x+235, windowPropertiesValue.y-20, scrollBoxSize.x, 25), ".txt");
		if (GUI.Button(Rect(windowPropertiesValue.x+250, windowPropertiesValue.y+10, 100, 25), "Import")) {		// Gets level on Desktop.
			print("Import");
			var word : String;
			try {
				var fileOpen = new StreamReader(title+".txt");		// Create an instance of StreamReader to read from a file.
			}
			catch (bug) {
				print("The file could not be Imported.");		// Let the user know what went wrong.
				print(bug.Message);
			}
			if (fileOpen) {
				var line = fileOpen.ReadLine();		// Read and display lines from the file until the end of the file is reached.
				while (line!=null) {
					word += line+"\n";		// Line returns are not read so we have to add them back in again.
					line = fileOpen.ReadLine();
					print(line);
				}
				print(word);
				Open(word);
			}
		}


		scrollPosition = GUI.BeginScrollView(Rect(windowPropertiesValue.x, windowPropertiesValue.y, scrollBoxSize.x, scrollBoxSize.y), scrollPosition, Rect(windowPropertiesValue.x, windowPropertiesValue.y, scrollBoxSize.z, scrollBoxSize.w));
		var level = PlayerPrefs.GetString("Levels", "Null").Split("/"[0]);
		var count : int;
		for (i=0; i<sampleLevel.length; i++) {
			if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+5+(25*i), 150, 25), sampleLevel[i].name)) {		// Gets level data form player prefs.
				print(sampleLevel[i]);
				Open(sampleLevel[i].text);
			}
			count++;
		}
		if (level[0]!="Null") {
			for (i=0; i<level.length; i++) {
				if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+5+(25*count), 150, 25), level[i])) {		// Gets level data form player prefs.
					print(level[i]);
					Open(PlayerPrefs.GetString(level[i]));
				}
				count++;
			}
		}
		GUI.EndScrollView();
	}

	
	if (GUI.Button(Rect(700, 100, 150, 25), "Export", "Plain")) {		// Stores level on Desktop.
		print("Export");
		try {												// Atempt to create a new file.
		var fileSave = new StreamWriter(title+".txt");		// Create an instance of StreamWriter to write text to a file.
		}
		catch (bug) {
			print("The fileSave could not be saved: ");			// Let the user know what went wrong.
			print(bug.Message);
		}
		if (fileSave) {											// If the fileSave was created then fill it with text.
			fileSave.WriteLine("Tank Game Version 7 Saved On : "+DateTime.Now);		// Arbitrary objects can also be written to the file.
			for (i=0; i<lenghtX; i++) {		// Ground.
				fileSave.Write("*");
				for (j=0; j<lenghtY; j++) {
					for (k=0; k<lenghtZ; k++) {
						fileSave.Write(ground[(i*lenghtY*lenghtZ+j*lenghtZ+k)].name+",");
					}
					fileSave.WriteLine();
				}
			}
			fileSave.Close();
		}
	}		

	// Camera Buttons.
	//GUI.Box(Rect(0, (charType*25)+5, 150, 25), "");		// Outline of selected option;
	var item : String;
	if (field==0) {
		if (GUI.Button(Rect(0, 0, 150, 25), "Grass", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
			item = "g";
		}
		if (GUI.Button(Rect(0, 25, 150, 25), "Road", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			item = "r";
		}
		if (GUI.Button(Rect(0, 50, 150, 25), "Tile1", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			item = "m";
		}
		if (GUI.Button(Rect(0, 75, 150, 25), "Tile12", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			item = "w";
		}
		if (GUI.Button(Rect(0, 100, 150, 25), "Tile13", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			item = "v";
		}
		if (GUI.Button(Rect(0, 125, 150, 25), "Tile4", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			item = "z";
		}
	}
	if (item) {		// If we click on something this becomes true.
		Tile(item+"0", camMain.ScreenToWorldPoint(Input.mousePosition), false);
	}

	teamLayer = (1<<LayerMask.NameToLayer("Editor1"));
	if (GUI.Button(Rect(0, 300, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
		field = (field>0 ? field-1 : 2);
	}
	GUI.Box(Rect(50, 300, 60, 50), ""+field);		// Outline of selected option;
	if (GUI.Button(Rect(100, 300, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
		field = (field<1 ? field+1 : 0);
	}
	teamLayer = (field==0 ? (1<<LayerMask.NameToLayer("Editor1")) : (field==1 ? (1<<LayerMask.NameToLayer("Editor2")) : (1<<LayerMask.NameToLayer("Editor3"))));
	camLayer = (field==0 ? ~(1<<LayerMask.NameToLayer("Editor2")) : ~(1<<LayerMask.NameToLayer("Editor1")));
	camera.main.cullingMask = camLayer;
}

function Open (line : String) {
	yield;
	for (i=0; i<ground.length; i++) {
		if (ground[i]) {
			Destroy(ground[i].gameObject);
		}
	}
	line = (!line ? " " : line);	// If no content in file then give it some to make it fit proper format.
	var save1 : String;				// Used to store data if we need to rebuild string.
	var save2 : String[];			// Used to store data if we need to rebuild string.
	var line1 = line.Split("*"[0]);
	if (line1.length==1) {			// If no "*" are in array then rebuild array to fit proper format.
		save1 = line1[0];
		line1 = new String[2];
		line1[1] = save1;			// On this one we skip the first line.
	}
	var line2 = line1[1].Split("\n"[0]);
	if (line2.length==1) {			// If no "\n" are in array then rebuild array to fit proper format.
		save1 = line2[0];
		line2 = new String[2];
		line2[0] = save1;
	}
	var line3 = line2[0].Split(","[0]);
	if (line3.length==1) {			// If no "," are in array then rebuild array to fit proper format.
		save1 = line3[0];
		line3 = new String[2];
		line3[0] = save1;
	}

	lenghtX = line1.length-1;
	lenghtY = line2.length-1;
	lenghtZ = line3.length-1;
	print(lenghtX);
	print(lenghtY);
	print(lenghtZ);

	var amount = lenghtX*lenghtY*lenghtZ;
	//var amount = (line1.length-1)*(line2.length-1)*(line3.length-1);
	print(amount);
	ground = new GameObject[amount];
	var create : Transform;
	for (i=1; i<line1.length; i++) {		// Ground.
		line2 = line1[i].Split("\n"[0]);
		if (line2.length!=lenghtY) {		// If no "," are in array then rebuild array to fit proper format.
			save2 = new String[line2.length];
			for (l=0; l<save2.length; l++) {
				save2[l] = line2[l];
			}
			line2 = new String[lenghtY];
			for (l=0; l<line2.length; l++) {
				line2[l] = (l<save2.length ? save2[l] : " ");
			}
		}
		for (j=0; j<lenghtY; j++) {
			line3 = line2[j].Split(","[0]);
			if (line3.length!=lenghtZ) {		// If no "," are in array then rebuild array to fit proper format.
				save2 = new String[line3.length];
				for (l=0; l<save2.length; l++) {
					save2[l] = line3[l];
				}
				line3 = new String[lenghtZ];
				for (l=0; l<line3.length; l++) {
					line3[l] = (l<save2.length ? save2[l] : " ");
				}
			}
			for (k=0; k<lenghtZ; k++) {
		//		print((i-1)+" "+j+" "+k);
				var turn = (i-1)*lenghtY*lenghtZ+j*lenghtZ+k;		// (i-1)*(line2.length-1)*(line3.length-1)+j*(line3.length-1)+k
				create = Instantiate(edit, Vector3(k*16, (i-1)*8, -j*16), Quaternion.Euler(0, 0, 0));
				ground[turn] = create.gameObject;
				ground[turn].name = line3[k].ToString();
				ground[turn].transform.parent = transform;
				ground[turn].gameObject.layer = 13;
				Tile(line3[k], Vector3(k*16, (i-1)*8, -j*16), true);
			}
		}
	}

}

function Tile (input : String, place : Vector3, drop : boolean) : boolean {
	charType = input;
	var item : Transform;
	if (input.length!=2) {	// Corects any errors.
		input = "-0";
	}
	if (input[0]=="g") {
		item = grass;
	}
	else if (input[0]=="r") {
		item = road;
	}
	else if (input[0]=="m") {
		item = road1;
	}
	else if (input[0]=="w") {
		item = road2;
	}
	else if (input[0]=="v") {
		item = road3;
	}
	else if (input[0]=="z") {
		item = road4;
	}
	if (item) {
		if (thing) {
			Destroy(thing.gameObject);
		}
		thing = Instantiate(item, item.position+place, Quaternion.Euler(0, 45*parseInt(input[1].ToString()), 0));
		check = thing.transform.GetComponent(Placer);
//		test = check.Check();

		if (drop) {		// If we want to set it down.
			var hit : RaycastHit;
			if (Physics.Raycast(thing.position+Vector3(0, 8, 0), -thing.up, hit, 100)) {
				thing.position = hit.transform.position;
				test = check.Generated();
				print(test);
			}
			if (test) {
				check.Set(teamLayer, charType);
				thing.transform.parent = hit.transform;
				thing.transform.position = hit.transform.position+Vector3(0, 4, 0);
			}
			thing = null;
		}
	}
}

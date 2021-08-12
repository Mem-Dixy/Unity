#pragma strict
#pragma implicit
#pragma downcast

static var paused = true;			// Controlls pause menu.		// Used to see if we are on main menu or playing game (we want BZFlag menu style).

private var text01 : String = "Tank Game Version 1.07\n\nObjective:\n\nBlow up the base of evil robotic vehicles before they blow up your base.\n\nStory:\n\nTank Game is a casual 3D Shooter about a tank and a helicopter saving the world from evil robotic vehicles (well, it's not quite that dramatic, but I need some sort of story line to go with the game). You control the helicopter and you go around blowing up the enemy bases. Your base will send out tanks to help defend against the enemy tanks which come after to blow up your base.\n\nControls:\n\nUp Arrow = Move Forward\nDown Arrow = Move Back\nLeft Arrow = Turn Left\nRight Arrow = Turn Right\nControl (Hold) = Fire\nOption (Hold) = Enable Strafe Mode\nCommand = Fire Missile\n[z] or [/] = Fire Rocket\nEscape = Toggle Pause Menu\nSpace = Toggle Map (Big/Small)\n\nFeedback:\n\nIf you want to provide feedback, comments, suggestions, ideas about new features, or if you find a bug, glitch, or error, then you can send me an email to Mem Dixy at mem_dixy_webpage@mac.com (Subject: Tank Game) by using the E-Mail button found on the main menu.";
private var text02 : String = "Click SEND to open up your email application and send a message to mem_dixy_webpage@mac.com";
private var text03 : String = "Click QUIT to quit the game.";

var gUISkin : GUISkin;					// The GUISkin we are using.
private var cameraType = 1;						// Public so players can get value.

var basic : PlayerBasic;			// This will be the character that we are in controll of. Public so Radar Script can get info.
private var helicopter : Helicopter;

private var winner : boolean;			// Makes it so we win. It only stays true if level loaded is 0.
private var human : PlayerBasic;				// This is the object we use to spawn in and out of veichels.
private var startUpScript : StartUpScript[];

// LoadedLevel
var cameraMain : Transform;
var cameraRadar : Transform;
var cameraMap : Transform;

var caption : String[];

// Main Menu
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

private var targetingInner : Transform;				// These are the targeting images that are shown on the screen.
private var targetingOuter : Transform;				// These are the targeting images that are shown on the screen.

var menu : String;		// This is what is displayed at the top of each menu.

private var fresh : Vector3;


var ground : GameObject[];

var edit : Transform;
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

var thing : Transform;		// This is what follows the camera/current tile.

private var check : Placer;		// Stores the tile we are holding.
private var test : boolean;		// The result of the test of able to drop tile.
private var remember : Transform;	// Keeps a check of tile under us for performace.

private var charType = "-";						// Public so players can get value.
private var charInt : int;						// Public so players can get value.

private var teamLayer : LayerMask;
//
var moveSpeed : int;		// How fast the camera moves.

private var open : boolean;
private var save : boolean;
var title = "TestFile";
var sampleLevel : TextAsset[];
private var lenghtX : int;
private var lenghtY : int;
private var lenghtZ : int;
private var field : int;		// Tells us what floor we are on.


private var toolbarInt : int = 0;
private var toolbarStrings : String[] = ["Audio", "Graphics", "Stats", "System"];
private var barInt = 0;


function Awake () {
	DontDestroyOnLoad(this);
}

function Start () {
	fresh = Vector3(2, 2, 1);						// New editor level.
	menuHierarchyArray = new int[5];				// This is how far down the submenus will go.
	// Actual growing box using window padding to offset it's position and size. We compinsate by altering the values we want with window padding so the two offsets cancel each other out.
	menu = "Main Menu";
	scrollBoxSize.x = buttonSize.x;
	scrollBoxSize.y = buttonSize.y*4;
	scrollBoxSize.z = buttonSize.x;
	scrollBoxSize.w = buttonSize.y*6;
	windowPropertiesValue.z = buttonSize.x;											// Growing box starts out at size of first menu.
	windowPropertiesValue.w = buttonSize.y*4;										// Growing box starts out at size of first menu.
	windowPropertiesValue.x = (Screen.width/2)+(-windowPropertiesValue.z/2);		// Growing box starts out in middile of screen.
	windowPropertiesValue.y = (Screen.height/2)+(-windowPropertiesValue.w/2);		// Growing box starts out in middile of screen.
	Application.LoadLevel(Application.levelCount-1);
	while (paused) {		// We start out paused but once level is loaded it unpauses so we wait for that and pause it again.
		yield;
	}
	paused = true;
}

function Update () {
	if (menuHierarchyArray[1]==2) {
		var euler = transform.eulerAngles;
		var inputMoveX = Input.GetAxis("XTransform");
		var inputMoveY = Input.GetAxis("YTransform");
		var inputMoveZ = Input.GetAxis("ZTransform");
		//camera.transform.rotation = Quaternion.Euler(Vector3(euler.x+inputSpinX, euler.y+inputSpinY, euler.z+inputSpinZ ));
		camera.transform.Translate(Vector3(inputMoveX, inputMoveZ, 0) * Time.deltaTime * moveSpeed);
		camera.orthographicSize += -inputMoveY * Time.deltaTime * moveSpeed;
		camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 8, 64);
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
		var ray = camera.ScreenPointToRay(Input.mousePosition);      // Gets the mouse position in the form of a ray.
		if (Physics.Raycast(ray, hit, 100)) {//, teamLayer)) {
			Debug.DrawLine(camera.ScreenToWorldPoint(Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height), 0)), hit.point, Color.red);
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
	if (Input.GetKeyDown("escape")) {		// Pause menu.
		paused = !paused;
	}
	if (Input.GetKeyDown(KeyCode.BackQuote)) {
		cameraType = (cameraType<2 ? cameraType+1 : 0);		// Cycle through cameras.
	}
}

function LateUpdate () {
	if (!paused) {
		if (menuHierarchyArray[1]==2) {
			
		}
		else {
			if (basic&&!Input.GetKeyDown(KeyCode.Backspace)) {		// This positions the camera behind the player.
				var x = basic.cameraPositions[1+(cameraType*3)];		// Get camera position.
				var y = basic.cameraPositions[2+(cameraType*3)];
				var z = basic.cameraPositions[3+(cameraType*3)];
				transform.rotation = Quaternion.Euler((basic.shoot ? basic.shoot : basic).transform.eulerAngles.x, (basic.shoot ? basic.shoot : basic).transform.eulerAngles.y, basic.transform.eulerAngles.z);
				transform.position = basic.transform.position+transform.TransformDirection(0, y, z);
				targetingInner.position = (basic.shoot ? basic.shoot.lookAtSpot : basic.transform.position);
				targetingOuter.position = (basic.shoot ? basic.shoot.lookAtSpot : basic.transform.position);
				if (basic!=human) {
					human.transform.rotation = Quaternion.Euler(0, basic.transform.eulerAngles.y, 0);
					human.transform.position = basic.transform.position;
				}
			}
			else {
				if (basic) {
					human.transform.position = human.transform.position+Vector3(0, 5, 0);
				}
				SwitchPlayer(human);		// Switch back to human.
			}
			for (i=0; i<startUpScript.length; i++) {		// Winner stuff.
				var remain : int;
				for (j=0; j<startUpScript[i].objectives.length; j++) {
					remain += (startUpScript[i].objectives[j] ? 1 : 0);
				}
				if (remain==0) {
					winner = true;
					paused = true;
					if (startUpScript[i].gameObject.name=="AStartPoint00") {
						//
					}
				}
			}
		}
	}
}
var style : GUIStyle;
function OnGUI () {
	GUI.skin = gUISkin;		// This tells how the text will display.
	if (menuHierarchyArray[1]==2) {
		camera.orthographic = true;
		transform.rotation = Quaternion.Euler(90, 0, 0);
//		transform.position.y = 10;
		// Edit Mode.
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
			Tile(item+"0", camera.ScreenToWorldPoint(Input.mousePosition), false);
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
		//
		//
		//
		
		// X.
		if (GUI.Button(Rect(400, 300, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
			fresh.x = (fresh.x>2 ? fresh.x/2 : 16);
		}
		GUI.Box(Rect(450, 300, 200, 50), ""+fresh.x);		// Outline of selected option;
		if (GUI.Button(Rect(650, 300, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
			fresh.x = (fresh.x<16 ? fresh.x*2 : 2);
		}
		// Y.
		if (GUI.Button(Rect(400, 350, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
			fresh.y = (fresh.y>2 ? fresh.y/2 : 16);
		}
		GUI.Box(Rect(450, 350, 200, 50), ""+fresh.y);		// Outline of selected option;
		if (GUI.Button(Rect(650, 350, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
			fresh.y = (fresh.y<16 ? fresh.y*2 : 2);
		}
		// Z.
		if (GUI.Button(Rect(400, 400, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
			fresh.z = (fresh.z>1 ? fresh.z-1 : 8);
		}
		GUI.Box(Rect(450, 400, 200, 50), ""+fresh.z);		// Outline of selected option;
		if (GUI.Button(Rect(650, 400, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
			fresh.z = (fresh.z<8 ? fresh.z+1 : 1);
		}
		//
	}
	else {
		camera.orthographic = false;
		// Camera Buttons.
		GUI.Box(Rect(0, (cameraType*25)+5, 150, 25), "");		// Outline of selected option;
		if (GUI.Button(Rect(0, 0, 150, 25), "Inside Camera", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
			cameraType = 0;
			//camera.fieldOfView = 40;
		}
		if (GUI.Button(Rect(0, 25, 150, 25), "Follow Camera", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
			cameraType = 1;
			//camera.fieldOfView = 60;
		}
		if (GUI.Button(Rect(0, 50, 150, 25), "Sky Camera", "Plain")||Input.GetKeyDown(KeyCode.Alpha3)) {
			cameraType = 2;
			//camera.fieldOfView = 60;
		}
		// Player Info.
		GUI.Box(Rect((Screen.width/2)+5, 0, 150, 25), (basic ? "Health : " + basic.healthInt.ToString() : "Regenerating"), "You Win");
		GUI.Box(Rect((Screen.width/2)+5, 25, 150, 25), "Level : "+Application.loadedLevel, "You Win");	// Currnet level number.		
		if (basic) {
			helicopter = basic.GetComponent(Helicopter);
		}
		if (helicopter) {	
			GUI.Box(Rect((Screen.width/2)+5, 50, 150, 25), "Missiles : "+(helicopter ? helicopter.missileCount : "0"), "You Win");
			GUI.Box(Rect((Screen.width/2)+5, 75, 150, 25), "Rockets : "+(helicopter ? helicopter.rocketCount : "0"), "You Win");
		}
		if (winner) {
			GUI.Label(Rect((Screen.width/2)-((Screen.width/2)/2), Screen.height-(Screen.height/2)-50, (Screen.width/2), (Screen.height/2)), "You Win\n\nYou saved the world from evil robotic vehicles.");	
			if (GUI.Button(Rect((Screen.width/2)-buttonSize.x, (Screen.height)-buttonSize.y, buttonSize.x*2, buttonSize.y), "Back To Main Menu")) {
				winner = false;
				paused = true;
			}
		}
	}
	if (paused) {		// We are on the main menu.
		GUI.Box(Rect(windowPropertiesValue.x-windowPadding.x, windowPropertiesValue.y-windowPadding.y, windowPropertiesValue.z+(windowPadding.x*2), windowPropertiesValue.w+(windowPadding.y*2)), "");	// Empty growing box.	// Fix issue when we win.
		if (windowPropertiesValue.x==windowPropertiesTarget.x && windowPropertiesValue.y==windowPropertiesTarget.y && windowPropertiesValue.z==windowPropertiesTarget.z && windowPropertiesValue.w==windowPropertiesTarget.w) {	// Only show buttons when window is done resizeing itself.
			GUI.BeginGroup(Rect(windowPropertiesValue.x, windowPropertiesValue.y, windowPropertiesValue.z, windowPropertiesValue.w), "MEOW", style);
			GUI.Label(Rect(0, 0, windowPropertiesValue.z, buttonSize.y), menu);
			//+(windowPropertiesValue.w-buttonSize.y*2<scrollBoxSize.w ? 16 : 0)
//			if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+windowPropertiesValue.w, buttonSize.x, buttonSize.y), (menuHierarchyArray[0]==0 ? "Resume Game" : "Back To Menu"))) {
			if (GUI.Button(Rect(0, windowPropertiesValue.w-buttonSize.y, buttonSize.x, buttonSize.y), (menuHierarchyArray[0]==0 ? "Resume Game" : "Back To Menu"))) {
				if (menuHierarchyActive==0) {
					paused = false;										// Return to the game.
				}
				else {
					menuHierarchyActive--;								// Move down one level.
					menuHierarchyArray[menuHierarchyActive] = 0;		// Then set current state to zero.
				}
			}
//			scrollPosition = GUI.BeginScrollView(Rect(0, buttonSize.y, scrollBoxSize.x, scrollBoxSize.y+buttonSize.y), scrollPosition, Rect(0, 0, scrollBoxSize.z, scrollBoxSize.w));
			scrollPosition = GUI.BeginScrollView(Rect(0, buttonSize.y, windowPropertiesValue.z+16, windowPropertiesValue.w-buttonSize.y*2), scrollPosition, Rect(0, 0, windowPropertiesValue.z, scrollBoxSize.w));
			if (menuHierarchyArray[0]==0) {		// This is what we see when we start.				// Main Menu.
				menu = "Main Menu";
				caption = ["Play", "Info", "Email", "Quit"];
				windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), scrollBoxSize.x, scrollBoxSize.y);
			//	windowPropertiesTarget = Vector4(100, 100, 400, 400);
				GUI.Box(Rect((Screen.width/2)-175, 25, 350, 50), "Tank Game");
				GUI.Box(Rect((Screen.width/2)-100, 75, 200, 25), "By: Mem Dixy", "Author");
				GUI.Box(Rect((Screen.width/2)-100, 125, 200, 25), Application.loadedLevelName, "Author");
			}
			else if (menuHierarchyArray[0]==1) {		// Select Level.
				if (menuHierarchyArray[1]==0) {
					menu = "Select Level";
					caption = ["Level01", "Editor"];
					windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), scrollBoxSize.x, scrollBoxSize.y);
				}
				else if (menuHierarchyArray[1]==1) {
					LoadThisLevel(1);
				}
				else if (menuHierarchyArray[1]==2) {
					if (menuHierarchyArray[2]==0) {		// Editor Menu.
						menu = "Editor Menu";
						caption = ["New", "Open", "Save"];
					}
					else if (menuHierarchyArray[2]==1) {			// New Menu.
						print("New");
						
						
						if (GUI.Button(Rect(400, 300, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
							fresh.x = (fresh.x>2 ? fresh.x/2 : 16);
						}
						GUI.Box(Rect(450, 300, 200, 50), ""+fresh.x);		// Outline of selected option;
						if (GUI.Button(Rect(650, 300, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
							fresh.x = (fresh.x<16 ? fresh.x*2 : 2);
						}
						// Y.
						if (GUI.Button(Rect(400, 350, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
							fresh.y = (fresh.y>2 ? fresh.y/2 : 16);
						}
						GUI.Box(Rect(450, 350, 200, 50), ""+fresh.y);		// Outline of selected option;
						if (GUI.Button(Rect(650, 350, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
							fresh.y = (fresh.y<16 ? fresh.y*2 : 2);
						}
						// Z.
						if (GUI.Button(Rect(400, 400, 50, 25), "<", "Plain")||Input.GetKeyDown(KeyCode.Alpha1)) {
							fresh.z = (fresh.z>1 ? fresh.z-1 : 8);
						}
						GUI.Box(Rect(450, 400, 200, 50), ""+fresh.z);		// Outline of selected option;
						if (GUI.Button(Rect(650, 400, 50, 25), ">", "Plain")||Input.GetKeyDown(KeyCode.Alpha2)) {
							fresh.z = (fresh.z<8 ? fresh.z+1 : 1);
						}


						
						PlayerPrefs.SetInt("Player Score", 10);
						print (PlayerPrefs.GetInt("Player Score"));
						var wood : String;	
						for (i=0; i<fresh.z; i++) {
							wood += "*";
							for (j=0; j<fresh.x; j++) {
								for (k=0; k<fresh.y; k++) {
									wood += "-0,";
								}
								wood += "\n";
							}
						}
						camera.transform.rotation = Quaternion.Euler(Vector3.right*90);
						Open(wood);			
						wood = "";	
						for (i=0; i<4; i++) {
							for (j=0; j<4; j++) {
								for (k=0; k<4; k++) {
									wood += "-0,";
								}
								wood += "\n";
							}
							wood += "*";
						}
						PlayerPrefs.SetString("DI", wood);
						wood = "";
						for (i=0; i<2; i++) {
							for (j=0; j<8; j++) {
								for (k=0; k<4; k++) {
									wood += "-0,";
								}
								wood += "\n";
							}
							wood += "*";
						}
						PlayerPrefs.SetString("HI", wood);
						PlayerPrefs.SetString("Levels", "DI/HI");
						Back();
					}
					else if (menuHierarchyArray[2]==2) {
						var level = PlayerPrefs.GetString("Levels", "Null").Split("/"[0]);
						if (menuHierarchyArray[3]==0) {		// Open Menu.
							menu = "Open Menu";
							caption = new String[0];
							windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), buttonSize.x, buttonSize.y*(sampleLevel.length+level.length+1));
				//scrollBoxSize = Vector4(buttonSize.x*1+16, buttonSize.y*4, buttonSize.x*1, buttonSize.y*(sampleLevel.length+level.length+1));
							caption = new String[sampleLevel.length+level.length+1];
							caption[0] = "Import";
							print(sampleLevel.length+" "+level.length);
							for (i=1; i<caption.length; i++) {
								caption[i] = (i-1<sampleLevel.length ? sampleLevel[i-1].name : level[i-1-sampleLevel.length]);
							}
						}
						else if (menuHierarchyArray[3]==1) {			// Import Menu.
							menu = "Import Menu";
							caption = new String[0];
							title = GUI.TextField(Rect(windowPropertiesValue.x, windowPropertiesValue.y+(0*buttonSize.y), 235, buttonSize.y), title, 16);
							GUI.Label(Rect(windowPropertiesValue.x+235, windowPropertiesValue.y+(0*buttonSize.y), scrollBoxSize.x, buttonSize.y), ".txt");
							if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+(2*buttonSize.y), buttonSize.x, buttonSize.y), "Import")) {		// Gets level on Desktop.
								print("Import");
								var word : String;
								try {
									var fileOpen = new System.IO.StreamReader(title+".txt");		// Create an instance of StreamReader to read from a file.
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
						}	
						else if (menuHierarchyArray[3]>=2) {			// Open Level.
							print("Open");
							print((menuHierarchyArray[3]-2<sampleLevel.length ? sampleLevel[menuHierarchyArray[3]-2].text : PlayerPrefs.GetString(level[menuHierarchyArray[3]-2-sampleLevel.length], "")));
							Open((menuHierarchyArray[3]-2<sampleLevel.length ? sampleLevel[menuHierarchyArray[3]-2].text : PlayerPrefs.GetString(level[menuHierarchyArray[3]-2-sampleLevel.length], "")));
							Back();
						}
					}
					else if (menuHierarchyArray[2]==3) {
						if (menuHierarchyArray[3]==0) {		// Save Menu.
							menu = "Save Menu";
							caption = ["Save", "Export"];
							scrollBoxSize = Vector4(buttonSize.x*2, buttonSize.y*4, buttonSize.x*1, buttonSize.y*1);
							title = GUI.TextField(Rect(windowPropertiesValue.x, windowPropertiesValue.y+100, 235, 25), title, 16);
							GUI.Label(Rect(windowPropertiesValue.x+235, windowPropertiesValue.y+100, scrollBoxSize.x, 25), ".txt");						
							title = GUI.TextField(Rect(windowPropertiesValue.x, windowPropertiesValue.y+(3*buttonSize.y), 235, buttonSize.y), title, 16);
//						}
//						if (menuHierarchyArray[3]==1) {		// Save Menu.
//
//							if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y, buttonSize.x, buttonSize.y), "Export")) {
//								NextMenu(2);
//							}
//							if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+(1*buttonSize.y), buttonSize.x, buttonSize.y), "Save")) {			// Gets level on Desktop.
//								menu = "Save Menu";
//								scrollBoxSize = Vector4(buttonSize.x*2, buttonSize.y*4, buttonSize.x*1, buttonSize.y*1);
//								NextMenu(2);
//							}
//
//							var levelS = PlayerPrefs.GetString("Levels", "Null").Split("/"[0]);
//							if (levelS[0]!="Null") {
//								for (i=0; i<levelS.length; i++) {
//									if (GUI.Button(Rect(windowPropertiesValue.x, windowPropertiesValue.y+((i+1)*buttonSize.y), buttonSize.x, buttonSize.y), levelS[i])) {		// Gets levelS data form player prefs.
//										print(levelS[i]);
//										Open(PlayerPrefs.GetString(levelS[i]));
//									}
//								}
//							}
						}
						else if (menuHierarchyArray[3]==1) {		// Save.
							print("Save");
							var output = "Tank Game Version 7 Saved On : "+System.DateTime.Now+"\n";
							for (i=0; i<lenghtX; i++) {		// Ground.
								output += "*";
								for (j=0; j<lenghtY; j++) {
									for (k=0; k<lenghtZ; k++) {
										output += ground[(i*lenghtY*lenghtZ+j*lenghtZ+k)].name+",";
									}
									output += "\n";
								}
							}
							PlayerPrefs.SetString(title, output);
							PlayerPrefs.SetString("Levels", PlayerPrefs.GetString("Levels", "Null")+"/"+title);
							Back();
						}
						else if (menuHierarchyArray[3]==2) {
							print("Export");
							try {												// Atempt to create a new file.
								print(title);
								var fileSave = new System.IO.StreamWriter(title+".txt");		// Create an instance of StreamWriter to write text to a file.
							}
							catch (bug) {
								print("The fileSave could not be saved: ");			// Let the user know what went wrong.
								print(bug.Message);
							}
							if (fileSave) {											// If the fileSave was created then fill it with text.
								fileSave.WriteLine("Tank Game Version 7 Saved On : "+System.DateTime.Now);		// Arbitrary objects can also be written to the file.
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
							Back();
						}
					}
				}
			}
			else if (menuHierarchyArray[0]==2) {		// Info Menu.
				menu = "Info Menu";
				caption = new String[0];
				windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), 500, 500);
				GUI.Label (Rect (0, 0, scrollBoxSize.z, scrollBoxSize.w), text01);
			}
			else if (menuHierarchyArray[0]==3) {		// Email Menu.
				menu = "Email Menu";
				caption = new String[0];
				windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), scrollBoxSize.x, scrollBoxSize.y);
				GUI.Label (Rect (0, 0, scrollBoxSize.z, scrollBoxSize.w), text02);
				if (GUI.Button(Rect(windowPropertiesValue.x+windowPropertiesValue.z+(-buttonSize.x)+(-windowPadding.x), windowPropertiesValue.y+windowPropertiesValue.w+(-buttonSize.y)+(-windowPadding.y), buttonSize.x, buttonSize.y), "SEND")) {
					Application.OpenURL("mailto:mem_dixy_webpage@mac.com");
				}
			}
			else if (menuHierarchyArray[0]==4) {		// Quit Menu.
				menu = "Quit Menu";
				caption = new String[0];
				windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), scrollBoxSize.x, scrollBoxSize.y);
				GUI.Label (Rect (0, 0, scrollBoxSize.z, scrollBoxSize.w), text03);
				if (GUI.Button (Rect(windowPropertiesValue.x+windowPropertiesValue.z+(-buttonSize.x)+(-windowPadding.x), windowPropertiesValue.y+windowPropertiesValue.w+(-buttonSize.y)+(-windowPadding.y), buttonSize.x, buttonSize.y), "QUIT")) {
					Application.Quit();
				}
			}
			
			for (i=0; i<caption.length; i++) {
				if (GUI.Button(Rect(0, buttonSize.y*i, buttonSize.x, buttonSize.y), caption[i])) {
					menuHierarchyArray[menuHierarchyActive] = i+1;	// i starts at 0 while menu starts at 1.				// Tell it what button we want.
					menuHierarchyActive++;										// Then move up one level.
				}
			}
			//scrollBoxSize = Vector4(buttonSize.x*1+16, buttonSize.y*4, buttonSize.x*1, buttonSize.y*(sampleLevel.length+level.length+1));
			//scrollBoxSize = Vector4(buttonSize.x*1+16, buttonSize.y*4, buttonSize.x*1, buttonSize.y*caption.length);

			GUI.EndScrollView();
			GUI.EndGroup();
		}
	}
//	windowPropertiesTarget.x = (Screen.width/2)+(-windowPropertiesValue.z/2);		// Makes the window center on window content.
//	windowPropertiesTarget.y = (Screen.height/2)+(-windowPropertiesValue.w/2);		// Makes the window center on window content.
//	windowPropertiesTarget.z = scrollBoxSize.x;		// Makes the window as big as content plus window padding.
//	windowPropertiesTarget.w = scrollBoxSize.y;		// Makes the window as big as content plus window padding.
//	windowPropertiesTarget = Vector4((Screen.width/2)+(-windowPropertiesValue.z/2), (Screen.height/2)+(-windowPropertiesValue.w/2), scrollBoxSize.x, scrollBoxSize.y);
	windowPropertiesValue.x += (windowPropertiesValue.x==windowPropertiesTarget.x ? 0 : Mathf.Sign(windowPropertiesTarget.x-windowPropertiesValue.x));
	windowPropertiesValue.y += (windowPropertiesValue.y==windowPropertiesTarget.y ? 0 : Mathf.Sign(windowPropertiesTarget.y-windowPropertiesValue.y));
	windowPropertiesValue.z += (windowPropertiesValue.z==windowPropertiesTarget.z ? 0 : Mathf.Sign(windowPropertiesTarget.z-windowPropertiesValue.z));
	windowPropertiesValue.w += (windowPropertiesValue.w==windowPropertiesTarget.w ? 0 : Mathf.Sign(windowPropertiesTarget.w-windowPropertiesValue.w));
}

function NextMenu (i : int) {
	menuHierarchyArray[menuHierarchyActive] = i;				// Tell it what button we want.
	menuHierarchyActive++;										// Then move up one level.
}

function Back () {
	menuHierarchyActive--;								// Move down one level.
	menuHierarchyArray[menuHierarchyActive] = 0;		// Then set current state to zero.
}

function SwitchPlayer (other : PlayerBasic) {
	human.gameObject.SetActiveRecursively(other==human);		// Hide human if we are not switching over to human.
	if (other==human&&basic) {
		basic.SendMessage("CanControl", false);
	}
	else {
		other.SendMessage("CanControl", true);
	}
	basic = other;
}

function LoadThisLevel (level : int) {
	yield WaitForEndOfFrame;
	if (!Application.isLoadingLevel) {
		transform.DetachChildren();
		Application.LoadLevel(level);
	}
}

function Open (line : String) {
	print(line);
	camera.transform.position = Vector3(0, 50, 0);
	yield;
	for (i=0; i<ground.length; i++) {
		print(i);
		Destroy(ground[i].gameObject);
	}
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
//				ground[turn].transform.parent = transform;
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

function OnLevelWasLoaded (level : int) {
	winner = false;											// Makes this false. If it where true and we load level 0 then we win.
	menuHierarchyActive = 0;							// Go back to the main menu.
	menuHierarchyArray[menuHierarchyActive] = 0;		// Then set current state to zero.
	var camMain : Transform = Instantiate(cameraMain, transform.position, transform.rotation);		// Create Camera.
	var	camMap : Transform = Instantiate(cameraRadar, transform.position, transform.rotation);	// Create Radar.
	var	camRadar : Transform = Instantiate(cameraRadar, transform.position, transform.rotation);	// Create Radar.
	var isRadar : Radar = camRadar.GetComponent(Radar);
	isRadar.isRadar = true;
	isRadar.show = true;
	isRadar.camera.depth++;
	camMain.parent = gameObject.transform;
	var find = GetComponentsInChildren(Transform);
	for (var found : Transform in find) {
		if (found.name=="TargetingInner") {
			targetingInner = found;
		}
		if (found.name=="TargetingOuter") {
			targetingOuter = found;
		}
	}
	if (!targetingInner||!targetingOuter) {
		Debug.Log("No targeting found.");
	}
	
	var startUpArray = FindObjectsOfType(StartUpScript);
	startUpScript = new StartUpScript[startUpArray.length];				// This is how far down the submenus will go.
	for (i=0; i<startUpScript.length; i++) {
		startUpScript[i] = startUpArray[i];
	}
	var temp = gameObject.FindWithTag("Player");
	basic = temp.GetComponent(PlayerBasic);
	human = basic;
	//	Invoke("LoadThisPart", 0);
	SwitchPlayer(human);		// Get Human at start.
	paused = false;
}

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public static bool paused = true;           // Controlls pause menu.		// Used to see if we are on main menu or playing game (we want BZFlag menu style).
												//
	public PlayerBasic basic;
	public GUISkin gUISkin;
	public Vector2 buttonSize = new Vector2();
	public Vector2 windowPadding = new Vector2();                       // How much spacing is between the buttons and the window.

	public Vector4 scrollBoxSize = new Vector4();
	public TextAsset[] sampleLevel = new TextAsset[0];
	//	public GUIStyle style = new GUIStyle();
	//
	private Helicopter helicopter;
	private bool winner = false;
	private int cameraType = 1;                     // Public so players can get value.
	private Vector3 fresh = new Vector3();
	//
	//	private Transform self;
	//	private Camera view;

	private PlayerBasic human;              // This is the object we use to spawn in and out of veichels.

	public ArrayList menuHierarchyArray = new ArrayList();      // This stores all the info on what menus to show.

	public Vector4 windowPropertiesValue = new Vector4();       // Actual size and position of window.
	public Vector4 windowPropertiesTarget = new Vector4();      // Size and postion we want the window to be at.
	public string[] caption = new string[] { "Play" , "Info" , "Email" , "Quit" };
	public string[] zone = new string[] { "" };
	private Vector2 scrollPosition = Vector2.zero;

	public string heading;      // This is what is displayed at the top of each menu.

	private LayerMask teamLayer;
	private LayerMask camLayer;
	//	private int field = 0;		// Tells us what floor we are on.


	private string text01 = "Tank Game Version 1.07\n\nObjective:\n\nBlow up the base of evil robotic vehicles before they blow up your base.\n\nStory:\n\nTank Game is a casual 3D Shooter about a tank and a helicopter saving the world from evil robotic vehicles (well, it's not quite that dramatic, but I need some sort of story line to go with the game). You control the helicopter and you go around blowing up the enemy bases. Your base will send out tanks to help defend against the enemy tanks which come after to blow up your base.\n\nControls:\n\nUp Arrow = Move Forward\nDown Arrow = Move Back\nLeft Arrow = Turn Left\nRight Arrow = Turn Right\nControl (Hold) = Fire\nOption (Hold) = Enable Strafe Mode\nCommand = Fire Missile\n[z] or [/] = Fire Rocket\nEscape = Toggle Pause Menu\nSpace = Toggle Map (Big/Small)\n\nFeedback:\n\nIf you want to provide feedback, comments, suggestions, ideas about new features, or if you find a bug, glitch, or error, then you can send me an email to Mem Dixy at mem_dixy_webpage@mac.com (Subject: Tank Game) by using the E-Mail button found on the main menu.";
	//	private string text02 = "Click SEND to open up your email application and send a message to mem_dixy_webpage@mac.com";
	//	private string text03 = "Click QUIT to quit the game.";
	private string title = "TestFile";
	private Vector3 lenght = new Vector3(0f , 0f , 0f);
	private GameObject[] ground;

	Vector2 biggest = new Vector2(20 , 20);
	public int here = 0;


	public Transform cameraMain;
	public Transform cameraRadar;
	public Transform cameraMap;






	public void Awake() {
		DontDestroyOnLoad(this);
	}

	public void Start() {
		fresh = new Vector3(2 , 2 , 1);                     // New editor level.
															// Actual growing box using window padding to offset it's position and size. We compinsate by altering the values we want with window padding so the two offsets cancel each other out.
		heading = "Main Menu";
		scrollBoxSize.x = buttonSize.x;
		scrollBoxSize.y = buttonSize.y * 4;
		scrollBoxSize.z = buttonSize.x;
		scrollBoxSize.w = buttonSize.y * 6;
		windowPropertiesValue.z = buttonSize.x;                                         // Growing box starts out at size of first menu.
		windowPropertiesValue.w = buttonSize.y * 4;                                     // Growing box starts out at size of first menu.
		windowPropertiesValue.x = (Screen.width / 2) + (-windowPropertiesValue.z / 2);      // Growing box starts out in middile of screen.
		windowPropertiesValue.y = (Screen.height / 2) + (-windowPropertiesValue.w / 2);     // Growing box starts out in middile of screen.

		//		Application.LoadLevel(Application.levelCount-1);
		//		Application.LoadLevel(Application.levelCount-2);
		//	while (paused) {		// We start out paused but once level is loaded it unpauses so we wait for that and pause it again.
		//yield;
		//	}
		paused = true;
	}

	public void Update() {
		//		if (Input.GetButtonDown("Fire1")) {
		//			LoadThisLevel(1);
		//		}

		if (Input.GetKeyDown("escape")) {       // Pause menu.
			paused = !paused;
		}
		if (basic) {
			//			basic.inputSpin = new Vector3(Input.GetAxisRaw("SpinX"), Input.GetAxisRaw("SpinY"), Input.GetAxisRaw("SpinZ"));
			//			basic.inputMove = new Vector3(Input.GetAxisRaw("XTransform"), Input.GetAxisRaw("YTransform"), Input.GetAxisRaw("ZTransform"));
			//			basic.fire1 = Input.GetButton("Fire1");
			//			basic.fire2 = Input.GetButton("Fire2");
			//			basic.fire3 = Input.GetButton("Fire3");
		}

	}
	public void LateUpdate() {
		if (!paused) {
			if (basic && !Input.GetKeyDown(KeyCode.Backspace)) {        // This positions the camera behind the player.
																		//	float x = basic.cameraPositions[1+(cameraType*3)];		// Get camera position.
				float y = basic.cameraPositions[2 + (cameraType * 3)];
				float z = basic.cameraPositions[3 + (cameraType * 3)];
				//		transform.rotation = Quaternion.Euler((basic.shoot!=null ? basic.shoot : basic).transform.eulerAngles.x, (basic.shoot!=null ? basic.shoot : basic).transform.eulerAngles.y, basic.transform.eulerAngles.z);
				//				if (basic.shoot!=null) {
				//					transform.rotation = Quaternion.Euler(basic.shoot.transform.eulerAngles.x, basic.shoot.transform.eulerAngles.y, basic.transform.eulerAngles.z);
				//
				//				}
				//				else {
				transform.rotation = Quaternion.Euler(basic.transform.eulerAngles.x , basic.transform.eulerAngles.y , basic.transform.eulerAngles.z);
				//				}
				//				transform.rotation = new Quaternion.Euler((basic.shoot!=null ? basic.shoot : basic).transform.eulerAngles.x, (basic.shoot!=null ? basic.shoot : basic).transform.eulerAngles.y, basic.transform.eulerAngles.z);
				transform.position = basic.transform.position + transform.TransformDirection(0 , y , z);
				//				targetingInner.position = (basic.shoot ? basic.shoot.lookAtSpot : basic.transform.position);
				//				targetingOuter.position = (basic.shoot ? basic.shoot.lookAtSpot : basic.transform.position);
				if (basic != human) {
					//	human.transform.rotation = Quaternion.Euler(0, basic.transform.eulerAngles.y, 0);
					//	human.transform.position = basic.transform.position;
				}
			}
			else {
				if (basic) {
					human.transform.position = human.transform.position + new Vector3(0 , 5 , 0);
				}
				SwitchPlayer(human);        // Switch back to human.
			}
			//			for (int i=0; i<startUpScript.length; i++) {		// Winner stuff.
			//				int remain;
			//				for (int j=0; j<startUpScript[i].objectives.length; j++) {
			//					remain += (startUpScript[i].objectives[j] ? 1 : 0);
			//				}
			//				if (remain==0) {
			//					winner = true;
			//					paused = true;
			//					if (startUpScript[i].gameObject.name=="AStartPoint00") {
			//						//
			//					}
			//				}
			//			}
		}
	}

	public void OnGUI() {
		GUI.skin = gUISkin;     // This tells how the text will display.
		GUI.BeginGroup(new Rect(Mathf.Max(biggest.x , windowPropertiesValue.x - windowPadding.x) , Mathf.Max(biggest.y , windowPropertiesValue.y - windowPadding.y - buttonSize.y) , Mathf.Min(Screen.width - biggest.x * 2 , windowPropertiesValue.z + windowPadding.x * 2) , Mathf.Min(Screen.height - biggest.y * 2 , windowPropertiesValue.w + windowPadding.y * 2 + buttonSize.y * 2)) , "MEOW");//, style);
		GUI.Box(new Rect(0 , 0 , Mathf.Min(Screen.width - biggest.x * 2 , windowPropertiesValue.z + windowPadding.x * 2) , Mathf.Min(Screen.height - biggest.y * 2 , windowPropertiesValue.w + windowPadding.y * 2 + buttonSize.y * 2)) , "");  // Empty growing box.	// Fix issue when we win.
		if (windowPropertiesValue == windowPropertiesTarget) {  // Only show buttons when window is done resizeing itself.
			GUI.Label(new Rect(windowPadding.x , windowPadding.y , windowPropertiesValue.z , buttonSize.y) , heading);      //+(windowPropertiesValue.w-buttonSize.y*2<scrollBoxSize.w ? 16 : 0)
			if (GUI.Button(new Rect(windowPadding.x , Mathf.Min(Screen.height - biggest.y * 2 - windowPadding.y - buttonSize.y , windowPadding.y + windowPropertiesValue.w + buttonSize.y) , buttonSize.x , buttonSize.y) , (here == 0 ? "Resume Game" : "Back To Menu"))) {
				if (menuHierarchyArray.Count <= 1) {
					paused = false;                                     // Return to the game.
				}
				else {
					Back();
				}
			}
			GUI.BeginGroup(new Rect(windowPadding.x , windowPadding.y + buttonSize.y , windowPropertiesValue.z , Mathf.Min(Screen.height - biggest.y * 2 , windowPropertiesValue.w)) , "MEOW");//, style);
			scrollPosition = GUI.BeginScrollView(new Rect(0 , 0 , windowPropertiesValue.z , Mathf.Min(Screen.height - biggest.y * 2 - windowPadding.y * 2 - buttonSize.y * 2 , windowPropertiesValue.w)) , scrollPosition , new Rect(0 , 0 , windowPropertiesValue.z , windowPropertiesValue.w));//caption.Length));

			if (paused) {       // We are on the main menu.
				MainMenu();
			}
			else {
				//SendMessage("Edit", this);
				GameMenu();
			}

			for (int i = 0; i < caption.Length; i++) {
				if (GUI.Button(new Rect(0 , buttonSize.y * i , buttonSize.x , buttonSize.y) , caption[i])) {
					menuHierarchyArray[menuHierarchyArray.Count - 1] = i + 1;       // i starts at 0 while menu starts at 1.				// Tell it what button we want.
					menuHierarchyArray.Add(0);                                  // Then move up one level.
				}
			}
			GUI.EndScrollView();
			GUI.EndGroup();
		}
		GUI.EndGroup();
		windowPropertiesValue.x += (windowPropertiesValue.x == windowPropertiesTarget.x ? 0 : Mathf.Sign(windowPropertiesTarget.x - windowPropertiesValue.x));
		windowPropertiesValue.y += (windowPropertiesValue.y == windowPropertiesTarget.y ? 0 : Mathf.Sign(windowPropertiesTarget.y - windowPropertiesValue.y));
		windowPropertiesValue.z += (windowPropertiesValue.z == windowPropertiesTarget.z ? 0 : Mathf.Sign(windowPropertiesTarget.z - windowPropertiesValue.z));
		windowPropertiesValue.w += (windowPropertiesValue.w == windowPropertiesTarget.w ? 0 : Mathf.Sign(windowPropertiesTarget.w - windowPropertiesValue.w));
	}

	public void Back() {
		menuHierarchyArray.RemoveAt(menuHierarchyArray.Count - 1);      // Remove last item in array.
		menuHierarchyArray[menuHierarchyArray.Count - 1] = 0;               // Set current state to zero.
	}

	public void LoadThisLevel(int level) {
		//		yield WaitForEndOfFrame;
		//		if (!Application.isLoadingLevel) {
		//			transform.DetachChildren();
		UnityEngine.SceneManagement.SceneManager.LoadScene(level);
		//		}
	}

	public void SwitchPlayer(PlayerBasic other) {
		//		human.gameObject.SetActiveRecursively(other==human);		// Hide human if we are not switching over to human.
		//		if (other==human&&basic) {
		//			basic.SendMessage("CanControl", false);
		//		}
		//		else {
		//			other.SendMessage("CanControl", true);
		//		}
		//		basic = other;
	}

	public void GameMenu() {
		// Camera Buttons.
		GUI.Box(new Rect(0 , (cameraType * 25) + 5 , 150 , 25) , "");       // Outline of selected option;
		if (GUI.Button(new Rect(0 , 0 , 150 , 25) , "Inside Camera" , "Plain") || Input.GetKeyDown(KeyCode.Alpha1)) {
			cameraType = 0;
			//camera.fieldOfView = 40;
		}
		if (GUI.Button(new Rect(0 , 25 , 150 , 25) , "Follow Camera" , "Plain") || Input.GetKeyDown(KeyCode.Alpha2)) {
			cameraType = 1;
			//camera.fieldOfView = 60;
		}
		if (GUI.Button(new Rect(0 , 50 , 150 , 25) , "Sky Camera" , "Plain") || Input.GetKeyDown(KeyCode.Alpha3)) {
			cameraType = 2;
			//camera.fieldOfView = 60;
		}
		// Player Info.
		GUI.Box(new Rect((Screen.width / 2) + 5 , 0 , 150 , 25) , (basic ? "Health : " + basic.healthCurrent.ToString() : "Regenerating") , "You Win");
		GUI.Box(new Rect((Screen.width / 2) + 5 , 25 , 150 , 25) , "Level : " + Application.loadedLevel , "You Win");   // Currnet level number.		
		if (basic) {
			helicopter = basic.GetComponent(typeof(Helicopter)) as Helicopter;
		}
		if (helicopter) {
			//			GUI.Box(new Rect((Screen.width/2)+5, 50, 150, 25), "Missiles : "+(helicopter ? helicopter.missileCount.ToString() : "0"), "You Win");
			//			GUI.Box(new Rect((Screen.width/2)+5, 75, 150, 25), "Rockets : "+(helicopter ? helicopter.rocketCount.ToString() : "0"), "You Win");
		}
		if (winner) {
			GUI.Label(new Rect((Screen.width / 2) - ((Screen.width / 2) / 2) , Screen.height - (Screen.height / 2) - 50 , (Screen.width / 2) , (Screen.height / 2)) , "You Win\n\nYou saved the world from evil robotic vehicles.");
			if (GUI.Button(new Rect((Screen.width / 2) - buttonSize.x , (Screen.height) - buttonSize.y , buttonSize.x * 2 , buttonSize.y) , "Back To Main Menu")) {
				winner = false;
				paused = true;
			}
		}
	}
	public void MainMenu() {
		if (here == 0) {        // This is what we see when we start.				// Main Menu.
			heading = "Main Menu";
			caption = new string[] { "Play" , "Info" , "Quit" };
			windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) - (caption.Length * buttonSize.y > 120 ? 8 : 0) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , scrollBoxSize.x + (caption.Length * buttonSize.y > 120 ? 16 : 0) , scrollBoxSize.y);
			//	windowPropertiesTarget = Vector4(100, 100, 400, 400);
			GUI.Box(new Rect((Screen.width / 2) - 175 , 25 , 350 , 50) , "Tank Game");
			GUI.Box(new Rect((Screen.width / 2) - 100 , 75 , 200 , 25) , "By: Mem Dixy" , "Author");
			GUI.Box(new Rect((Screen.width / 2) - 100 , 125 , 200 , 25) , Application.loadedLevelName , "Author");
		}
		else if ((int) menuHierarchyArray[0] == 1) {        // Select Level.
			if ((int) menuHierarchyArray[1] == 0) {
				heading = "Select Level";
				caption = new string[] { "Level01" , "Editor" };
				windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , scrollBoxSize.x , scrollBoxSize.y);
			}
			else if ((int) menuHierarchyArray[1] == 1) {
				LoadThisLevel(1);
				Back();
			}
			else if ((int) menuHierarchyArray[1] == 2) {
				//		var.edits.EditorMenu();
				if ((int) menuHierarchyArray[2] == 0) {     // Editor Menu.
					heading = "Editor Menu";
					caption = new string[] { "New" , "Open" , "Save" };
					windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , scrollBoxSize.x , scrollBoxSize.y);
				}
				else if ((int) menuHierarchyArray[2] == 1) {            // New Menu.
					heading = "New";
					caption = new string[] { };
					windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , scrollBoxSize.x * 2 , scrollBoxSize.y * 2);
					GUI.BeginGroup(new Rect(windowPropertiesValue.x - (caption.Length * buttonSize.y > 120 ? 8 : 0) , windowPropertiesValue.y , windowPropertiesValue.z + (caption.Length * buttonSize.y > 120 ? 16 : 0) , windowPropertiesValue.w) , "AOEUEOA");//, style);
					if (GUI.Button(new Rect(00 , 50 , 50 , 25) , "<" , "Plain")) {
						fresh.x = (fresh.x > 2 ? fresh.x / 2 : 16);
					}
					GUI.Box(new Rect(50 , 50 , 200 , 50) , "" + fresh.x);       // Outline of selected option;
					if (GUI.Button(new Rect(250 , 50 , 50 , 25) , ">" , "Plain")) {
						fresh.x = (fresh.x < 16 ? fresh.x * 2 : 2);
					}
					// Y.
					if (GUI.Button(new Rect(00 , 100 , 50 , 25) , "<" , "Plain")) {
						fresh.y = (fresh.y > 2 ? fresh.y / 2 : 16);
					}
					GUI.Box(new Rect(50 , 100 , 200 , 50) , "" + fresh.y);      // Outline of selected option;
					if (GUI.Button(new Rect(250 , 100 , 50 , 25) , ">" , "Plain")) {
						fresh.y = (fresh.y < 16 ? fresh.y * 2 : 2);
					}
					// Z.
					if (GUI.Button(new Rect(00 , 150 , 50 , 25) , "<" , "Plain")) {
						fresh.z = (fresh.z > 1 ? fresh.z - 1 : 8);
					}
					GUI.Box(new Rect(50 , 150 , 200 , 50) , "" + fresh.z);      // Outline of selected option;
					if (GUI.Button(new Rect(250 , 150 , 50 , 25) , ">" , "Plain")) {
						fresh.z = (fresh.z < 8 ? fresh.z + 1 : 1);
					}
					if (GUI.Button(new Rect(50 , 200 , 150 , 50) , "Generate")) {
						string wood = "";
						for (int i = 0; i < fresh.z; i++) {
							wood += "*";
							for (int j = 0; j < fresh.x; j++) {
								for (int k = 0; k < fresh.y; k++) {
									wood += "-0,";
								}
								wood += "\n";
							}
						}
						//					var.Open(wood);
					}
					GUI.EndGroup();
					//	Back();
				}
				else if ((int) menuHierarchyArray[2] == 2) {
					string level = (PlayerPrefs.GetString("Levels" , "Null").Split("/"[0])).ToString();
					if ((int) menuHierarchyArray[3] == 0) {     // Open Menu.
						heading = "Open Menu";
						caption = new string[] { };
						windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , buttonSize.x , buttonSize.y * (sampleLevel.Length + level.Length + 1));
						//scrollBoxSize = Vector4(buttonSize.x*1+16, buttonSize.y*4, buttonSize.x*1, buttonSize.y*(sampleLevel.Length+level.Length+1));
						caption = new string[sampleLevel.Length + level.Length + 1];
						caption[0] = "Import";
						print(sampleLevel.Length + " " + level.Length);
						for (int i = 1; i < caption.Length; i++) {
							caption[i] = (i - 1 < sampleLevel.Length ? sampleLevel[i - 1].name.ToString() : level[i - 1 - sampleLevel.Length].ToString());
						}
					}
					else if ((int) menuHierarchyArray[3] == 1) {            // Import Menu.
						heading = "Import Menu";
						caption = new string[] { };
						title = GUI.TextField(new Rect(windowPropertiesValue.x , windowPropertiesValue.y + (0 * buttonSize.y) , 235 , buttonSize.y) , title , 16);
						GUI.Label(new Rect(windowPropertiesValue.x + 235 , windowPropertiesValue.y + (0 * buttonSize.y) , scrollBoxSize.x , buttonSize.y) , ".txt");
						if (GUI.Button(new Rect(windowPropertiesValue.x , windowPropertiesValue.y + (2 * buttonSize.y) , buttonSize.x , buttonSize.y) , "Import")) {        // Gets level on Desktop.
							print("Import");
							string word = "";
							//					try {
							System.IO.StreamReader fileOpen = new System.IO.StreamReader(title + ".txt");       // Create an instance of StreamReader to read from a file.
																												//					}
																												//					catch (Exception bug) {
							print("The file could not be Imported.");       // Let the user know what went wrong.
																			//						print(bug.Message);
																			//					}
																			//					if (fileOpen) {
							string line = (fileOpen.ReadLine()).ToString();     // Read and display lines from the file until the end of the file is reached.
							while (line != null) {
								word += line + "\n";        // Line returns are not read so we have to add them back in again.
								line = fileOpen.ReadLine();
								print(line);
							}
							print(word);
							//							var.Open(word);
							//				}
						}
					}
					else if ((int) menuHierarchyArray[3] >= 2) {            // Open Level.
						print("Open");
						print(((int) menuHierarchyArray[3] - 2 < sampleLevel.Length ? sampleLevel[(int) menuHierarchyArray[3] - 2].text : PlayerPrefs.GetString(level[(int) menuHierarchyArray[3] - 2 - sampleLevel.Length].ToString() , "")));
						//					var.Open(((int)menuHierarchyArray[3]-2<sampleLevel.Length ? sampleLevel[(int)menuHierarchyArray[3]-2].text : PlayerPrefs.GetString(level[(int)menuHierarchyArray[3]-2-sampleLevel.Length].ToString(), "")));
						Back();
					}
				}
				else if ((int) menuHierarchyArray[2] == 3) {
					if ((int) menuHierarchyArray[3] == 0) {     // Save Menu.
						heading = "Save Menu";
						caption = new string[] { "Save" , "Export" };
						scrollBoxSize = new Vector4(buttonSize.x * 2 , buttonSize.y * 4 , buttonSize.x * 1 , buttonSize.y * 1);
						title = GUI.TextField(new Rect(windowPropertiesValue.x , windowPropertiesValue.y + 100 , 235 , 25) , title , 16);
						GUI.Label(new Rect(windowPropertiesValue.x + 235 , windowPropertiesValue.y + 100 , scrollBoxSize.x , 25) , ".txt");
						title = GUI.TextField(new Rect(windowPropertiesValue.x , windowPropertiesValue.y + (3 * buttonSize.y) , 235 , buttonSize.y) , title , 16);
						//						}
						//						if ((int)menuHierarchyArray[3]==1) {		// Save Menu.
						//
						//							if (GUI.Button(new Rect(windowPropertiesValue.x, windowPropertiesValue.y, buttonSize.x, buttonSize.y), "Export")) {
						//								NextMenu(2);
						//							}
						//							if (GUI.Button(new Rect(windowPropertiesValue.x, windowPropertiesValue.y+(1*buttonSize.y), buttonSize.x, buttonSize.y), "Save")) {			// Gets level on Desktop.
						//								menu = "Save Menu";
						//								scrollBoxSize = Vector4(buttonSize.x*2, buttonSize.y*4, buttonSize.x*1, buttonSize.y*1);
						//								NextMenu(2);
						//							}
						//
						//							var levelS = PlayerPrefs.Getstring("Levels", "Null").Split("/"[0]);
						//							if (levelS[0]!="Null") {
						//								for (int i=0; i<levelS.Length; i++) {
						//									if (GUI.Button(new Rect(windowPropertiesValue.x, windowPropertiesValue.y+((i+1)*buttonSize.y), buttonSize.x, buttonSize.y), levelS[i])) {		// Gets levelS data form player prefs.
						//										print(levelS[i]);
						//										Open(PlayerPrefs.Getstring(levelS[i]));
						//									}
						//								}
						//							}
					}
					else if ((int) menuHierarchyArray[3] == 1) {        // Save.
						print("Save");
						string output = "Tank Game Version 7 Saved On : " + System.DateTime.Now + "\n";
						for (int i = 0; i < lenght.x; i++) {        // Ground.
							output += "*";
							for (int j = 0; j < lenght.y; j++) {
								for (int k = 0; k < lenght.z; k++) {
									output += ground[(i * (int) lenght.y * (int) lenght.z + j * (int) lenght.z + k)].name + ",";
								}
								output += "\n";
							}
						}
						PlayerPrefs.SetString(title , output);
						PlayerPrefs.SetString("Levels" , PlayerPrefs.GetString("Levels" , "Null") + "/" + title);
						Back();
					}
					else if ((int) menuHierarchyArray[3] == 2) {
						print("Export");
						//				try {												// Atempt to create a new file.
						print(title);
						System.IO.StreamWriter fileSave = new System.IO.StreamWriter(title + ".txt");       // Create an instance of StreamWriter to write text to a file.
																											//				}
																											//				catch (Exception bug) {
						print("The fileSave could not be saved: ");         // Let the user know what went wrong.
																			//					print(bug.Message);
																			//				}
																			//				if (fileSave) {											// If the fileSave was created then fill it with text.
						fileSave.WriteLine("Tank Game Version 7 Saved On : " + System.DateTime.Now);        // Arbitrary objects can also be written to the file.
						for (int i = 0; i < lenght.x; i++) {        // Ground.
							fileSave.Write("*");
							for (int j = 0; j < lenght.y; j++) {
								for (int k = 0; k < lenght.z; k++) {
									fileSave.Write(ground[(i * (int) lenght.y * (int) lenght.z + j * (int) lenght.z + k)].name + ",");
								}
								fileSave.WriteLine();
							}
						}
						fileSave.Close();
					}
					Back();
					//			}
				}
			}
		}
		else if ((int) menuHierarchyArray[0] == 2) {        // Info Menu.
			heading = "Info Menu";
			caption = new string[] { };
			windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , 500 , 500);
			zone = new string[] { text01 };
		}
		else if ((int) menuHierarchyArray[0] == 3) {        // Quit Menu.
			if ((int) menuHierarchyArray[1] == 0) {
				heading = "Quit Menu";
				caption = new string[] { "Yes, Quit" , "No, Don't" };
				windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowPropertiesValue.z / 2) , (Screen.height / 2) + (-windowPropertiesValue.w / 2) , scrollBoxSize.x , scrollBoxSize.y);
			}
			else if ((int) menuHierarchyArray[0] == 1) {
				Application.Quit();
				Back();
			}
			else if ((int) menuHierarchyArray[0] == 2) {
				Back();
				//	Back();
			}
		}
	}
	public void OnLevelWasLoaded(int level) {
		winner = false;                                         // Makes this false. If it where true and we load level 0 then we win.
																//		menuHierarchyActive = 0;							// Go back to the main menu.
																//		menuHierarchyArray[menuHierarchyActive] = 0;		// Then set current state to zero.
		Transform camMain = Instantiate(cameraMain , transform.position , transform.rotation) as Transform;     // Create Camera.
																												//		Transform camMap = Instantiate(cameraRadar, transform.position, transform.rotation) as Transform;	// Create Radar.
		Transform camRadar = Instantiate(cameraRadar , transform.position , transform.rotation) as Transform;   // Create Radar.
		Radar isRadar = camRadar.GetComponent(typeof(Radar)) as Radar;
		isRadar.isRadar = true;
		isRadar.show = true;
		isRadar.GetComponent<Camera>().depth++;
		camMain.parent = gameObject.transform;
		//		Transform[] find = GetComponentsInChildren(typeof(Transform)) as Transform[];
		//		foreach (Transform found in find) {		// Put into GUI.
		//			if (found.name=="TargetingInner") {
		//				targetingInner = found;
		//			}
		//			if (found.name=="TargetingOuter") {
		//				targetingOuter = found;
		//			}
		//		}
		//		if (!targetingInner||!targetingOuter) {
		//			Debug.Log("No targeting found.");
		//		}

		StartUpScript[] startUpArray = FindObjectsOfType(typeof(StartUpScript)) as StartUpScript[];
		StartUpScript[] startUpScript = new StartUpScript[startUpArray.Length];             // This is how far down the submenus will go.
		for (int i = 0; i < startUpScript.Length; i++) {
			startUpScript[i] = startUpArray[i];
		}
		GameObject temp = GameObject.FindWithTag("Player");
		Debug.Log(temp + " Player" , temp);
		basic = temp.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		print(basic);
		human = basic;
		//	Invoke("LoadThisPart", 0);
		SwitchPlayer(human);        // Get Human at start.
		paused = false;
	}
}

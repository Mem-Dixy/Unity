using UnityEngine;
public class PauseMenu : MonoBehaviour {
	
	public static bool paused = true;			// Controlls pause menu.		// Used to see if we are on main menu or playing game (we want BZFlag menu style).

	public GUISkin gUISkin;
	public Transform Player;
	public LevelEdit levelEdit;
	public bool playing = false;


	//	
//	public Vector4 scrollBoxSize = new Vector4();
	public Vector2 scrollBoxSize = new Vector2();
	public Vector2 windowSize = new Vector2();

	public Vector2 buttonSize = new Vector2();
	public Vector2 windowPadding = new Vector2();						// How much spacing is between the buttons and the window.
	public Vector2 screenPadding = new Vector2(20, 20);

	public System.Collections.ArrayList menuHierarchyArray = new System.Collections.ArrayList();		// This stores all the info on what menus to show.
	public System.Collections.ArrayList menuHeadingArray = new System.Collections.ArrayList();		// This stores all the info on what menus to show.


	public string heading;		// This is what is displayed at the top of each menu.
	private float sound = 100;		// How loud the sound output is.
	private string[] levelLoad;
	private string levelTest;
	
	////
	////
	private Vector3 fresh = new Vector3();		// Set new grid size.
	private Vector3 pretty = new Vector3(255, 255, 255);		// Set color of grid.
	public Material[] wraping;		// Set color of grid.
	public TextAsset[] sampleLevel = new TextAsset[0];
	private bool sizeSet = false;		// Has the default windowSize been overidden?

	private PlayerBasic selectedPlayer;		// Who we are currently controling.
	
	private void Start () {
		menuHeadingArray.Add("Main Menu");
		menuHierarchyArray.Add(0);									// Then move up one level.
		fresh = new Vector3(2, 2, 2);
		
		Object find = FindObjectOfType(typeof(LevelEdit)) as Object;
		if (find) {
			levelEdit = find as LevelEdit;
//			print(levelEdit);
		}
		else {
			print("Bad thing happened.");
		}
		caption = new string[]{"Restart Level", "Load Level", "Level Editor", "Setup", "Help", "Quit"};
		windowSize = new Vector2(buttonSize.x, buttonSize.y*(caption.Length)+buttonSize.y*2);
	}


	private Vector4 windowPropertiesStep = new Vector4();
	private Vector4 windowPropertiesTarget = new Vector4();
	private Vector4 windowPropertiesValue = new Vector4();
	public string[] caption = new string[] { };
	private Vector2 scrollPosition = Vector2.zero;
	private void FixedUpdate() {


		float x1 = windowPropertiesTarget.x;
		float y1 = windowPropertiesTarget.y;
		float z1 = windowPropertiesTarget.z;
		float w1 = windowPropertiesTarget.w;

		float step = 5;

		float x2 = x1 - windowPropertiesValue.x;
		float y2 = y1 - windowPropertiesValue.y;
		float z2 = z1 - windowPropertiesValue.z;
		float w2 = w1 - windowPropertiesValue.w;
		UnityEngine.Debug.Log("check" + new Vector4(x2 , y2 , z2 , w2));

		float x3 = Mathf.Sign(x2);
		float y3 = Mathf.Sign(y2);
		float z3 = Mathf.Sign(z2);
		float w3 = Mathf.Sign(w2);
		UnityEngine.Debug.Log("sign" + new Vector4(x3 , y3 , z3 , w3));

		windowPropertiesValue.x += Mathf.Min(x2 , x3 * step);
		windowPropertiesValue.y += Mathf.Min(y2 , y3 * step);
		windowPropertiesValue.z += Mathf.Min(z2 , z3 * step);
		windowPropertiesValue.w += Mathf.Min(w2 , w3 * step);
		UnityEngine.Debug.Log("value" + windowPropertiesValue);
		UnityEngine.Debug.Log("target" + windowPropertiesTarget);
	}
	private void Update () {
		if (Input.GetKeyDown("escape")) {		// Pause menu.
			paused = !paused;
		}
		if (!PauseMenu.paused && LevelEdit.playing) {		// While in the game we have a "get player" laser.
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<30)) {
				PlayerBasic get = hit.transform.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
				if (get) {
					SwitchPlayer(get);
				}
			}
		}
	}
	private void LateUpdate () {
		if (!PauseMenu.paused && LevelEdit.playing) {
			if(selectedPlayer!=null) {		// While in the game we have a "get player" laser.
				transform.position = selectedPlayer.transform.position;
				transform.rotation = selectedPlayer.transform.rotation;
				transform.parent = selectedPlayer.transform;
				GetComponent<Camera>().orthographic = false;
			}
			else {
				transform.parent = null;
				transform.position = new Vector3(0, 16.4f, 0);
				transform.rotation = Quaternion.Euler(90, 0, 0);
				GetComponent<Camera>().orthographic = true;
			}
		}
	}

	private float scrollBarX;
	private float scrollBarY;

	private void OnGUI() {
		if (!paused || levelEdit.isLoading) {
			return;
		}
		Vector2 groupSize = new Vector2(windowPropertiesValue.z - windowPadding.x * 2 , windowPropertiesValue.w - windowPadding.y * 2); // Store the value of the window in this variable.
		scrollBarX = (caption.Length * buttonSize.y > groupSize.y - buttonSize.y * 2 ? 16 : 0);     // Determins if we should increase window size to fit a scroll bar in.
		scrollBarY = (buttonSize.x > groupSize.x ? 16 : 0);
		GUI.skin = gUISkin;     // This tells how the text will display.

		GUI.Box(new Rect(windowPropertiesValue.x , windowPropertiesValue.y , windowPropertiesValue.z , windowPropertiesValue.w) , "");  // Empty growing box.	// Fix issue when we win.
		GUI.BeginGroup(new Rect(windowPropertiesValue.x + windowPadding.x , windowPropertiesValue.y + windowPadding.y , groupSize.x , groupSize.y));//, "MEOW");//, style);

		windowPropertiesTarget = new Vector4((Screen.width / 2) + (-windowSize.x / 2) , (Screen.height / 2) + (-windowSize.y / 2) , windowSize.x , windowSize.y);     // Default value.
		windowPropertiesTarget = new Vector4(Mathf.Floor(windowPropertiesTarget.x) , Mathf.Floor(windowPropertiesTarget.y) , Mathf.Ceil(windowPropertiesTarget.z) , Mathf.Ceil(windowPropertiesTarget.w));
		windowPropertiesTarget = new Vector4(
			Mathf.Max(windowPropertiesTarget.x , screenPadding.x) ,
			Mathf.Max(windowPropertiesTarget.y , screenPadding.y) ,
			Mathf.Min(windowPropertiesTarget.z , Screen.width - screenPadding.x * 2) + scrollBarX ,
			Mathf.Min(windowPropertiesTarget.w , Screen.height - screenPadding.y * 2) + scrollBarY
		);

		if (windowPropertiesValue == windowPropertiesTarget) { // Only show buttons when window is done resizeing itself.
			//levelLoad = PlayerPrefs.GetString("Levels" , "No Saved Files").Split("/"[0]);
			GUI.Label(new Rect(0 , 0 , buttonSize.x , buttonSize.y) , heading);      //+(windowPropertiesValue.w-buttonSize.y*2<scrollBoxSize.w ? 16 : 0)
			GUI.BeginGroup(new Rect(0 , buttonSize.y , groupSize.x , groupSize.y - buttonSize.y));//, "MEOW");//, style);

			// if (GUI.Button(new Rect(0, 0, buttonSize.x, buttonSize.y), ((int)menuHierarchyArray[0]==0 ? "Unpause" : "Return"))) {
			//				if (!LevelEdit.playing) {
			//					if (GUI.Button(new Rect(0, 0, buttonSize.x, buttonSize.y), ((int)menuHierarchyArray[0]==0 ? "End" : "Return"))) {
			//						if ((int)menuHierarchyArray[0]==0) {
			//							LevelEdit.playing = !LevelEdit.playing;
			//							paused = !LevelEdit.playing;										// Return to the game.
			//						}
			//						else {
			//							Back();		// Up one in the menu.
			//						}
			//					}
			//				}

			if ((int) menuHierarchyArray[0] != 0) {
				if (GUI.Button(new Rect(0 , 0 , buttonSize.x , buttonSize.y) , "Return")) {
					Back();     // Up one in the menu.
				}
			}
			else if (LevelEdit.editor) {
				if (GUI.Button(new Rect(0 , 0 , buttonSize.x , buttonSize.y) , "Unpause")) {
					paused = false;     // Up one in the menu.
				}
			}


			//				if (GUI.Button(new Rect(0, 0, buttonSize.x, buttonSize.y), ((int)menuHierarchyArray[0]==0 ? (!LevelEdit.playing ? "Play" : "End") : "Return"))) {
			//					if ((int)menuHierarchyArray[0]==0) {
			//						LevelEdit.playing = !LevelEdit.playing;
			//						paused = !LevelEdit.playing;										// Return to the game.
			//					}
			//					else {
			//						Back();		// Up one in the menu.
			//					}
			//				}
			scrollPosition = GUI.BeginScrollView(new Rect(0 , buttonSize.y , groupSize.x , groupSize.y - buttonSize.y * 2) , scrollPosition , new Rect(0 , 0 , Mathf.Max(groupSize.x , buttonSize.x) - scrollBarX , buttonSize.y * caption.Length - scrollBarY));//caption.Length));			
			if (LevelEdit.playing) {        // We are on the main menu.
				if (LevelEdit.editor) {
					GameEdit();
				}
				else {
					GameMenu();
				}
			}
			else if (LevelEdit.editor) {
				EditorMenu();
			}
			else {      // We are on the main menu.
				MainMenu();
			}
			if (!sizeSet) {
				windowSize = new Vector2(buttonSize.x , buttonSize.y * (caption.Length) + buttonSize.y * 2);
			}
			sizeSet = false;
			for (int i = 0; i < caption.Length; i++) {      // Show the buttons.
				if (GUI.Button(new Rect(0 , buttonSize.y * i , buttonSize.x , buttonSize.y) , caption[i])) {
					menuHierarchyArray[menuHierarchyArray.Count - 1] = i + 1;       // i starts at 0 while menu starts at 1.				// Tell it what button we want.
					menuHierarchyArray.Add(0);                                  // Then move up one level.
					menuHeadingArray.Add(caption[i]);
				}
				heading = (string) menuHeadingArray[menuHierarchyArray.Count - 1];
			}
			GUI.EndScrollView();
			GUI.EndGroup();
		}
		GUI.EndGroup();
	}
	private void Back () {
		menuHierarchyArray.RemoveAt(menuHierarchyArray.Count-1);		// Remove last item in array.
		menuHierarchyArray[menuHierarchyArray.Count-1] = 0;				// Set current state to zero.
		menuHeadingArray.RemoveAt(menuHierarchyArray.Count);		// Remove last item in array.
//		menuHeadingArray[menuHierarchyArray.Count-1] = 0;
	}
	private void WindowSize (Vector2 input) {
		windowSize = input;
		sizeSet = true;
	}


	private void GameEdit () {
		if ((int)menuHierarchyArray[0]==0) {
			caption = new string[]{"Continue Test", "End Test", "Leave Editor", "Quit"};
		}
		else if ((int)menuHierarchyArray[0]==1) {		// Continue Test.
			paused = false;
			Back();
		}
		else if ((int)menuHierarchyArray[0]==2) {		// End Test.
			LevelEdit.playing = false;
			print(levelTest);
			StartCoroutine(levelEdit.Open(levelTest));
			Back();
		}
		else if ((int)menuHierarchyArray[0]==3) {		// Leave Editor.
			LevelEdit.playing = false;
			LevelEdit.editor = false;
			Back();
		}
		else if ((int)menuHierarchyArray[0]==4) {		// Quit.
			QuitGame(1);
		}
	}

	private void GameMenu () {
		if ((int)menuHierarchyArray[0]==0) {
			caption = new string[]{"Continue Playing", "Restart Level", "Load Level", "End Game", "Quit"};
		}
		else if ((int)menuHierarchyArray[0]==1) {		// Continue Playing.
			paused = false;
			Back();
		}
		else if ((int)menuHierarchyArray[0]==2) {		// Restart Level.
			StartCoroutine(levelEdit.Open(levelEdit.lastLoad));
			Back();
		}
		else if ((int)menuHierarchyArray[0]==3) {		// Load Level.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[levelEdit.levelGroup.Length];
				print(levelEdit.levelGroup.Length);
				for (int i=0; i<caption.Length; i++) {
					string[] load = levelEdit.levelGroup[i].Split(new char[]{'^'});
					caption[i] = load[0];
				}
			}
			if ((int)menuHierarchyArray[1]>=1) {
				StartCoroutine(levelEdit.Open(levelEdit.levelGroup[(int)menuHierarchyArray[1]-1]));
				Back();
				Back();		// Go to root because on Open we switch menues.
			}
		}
		else if ((int)menuHierarchyArray[0]==4) {		// End Game.
			LevelEdit.playing = false;
			Back();
		}
		else if ((int)menuHierarchyArray[0]==5) {		// Quit.
			QuitGame(1);
		}

//			if ((int)menuHierarchyArray[1]==1) {
//				if ((int)menuHierarchyArray[1]-1<sampleLevel.Length) {
//					StartCoroutine(levelEdit.Open(sampleLevel[(int)menuHierarchyArray[1]-1].text));
//				}
//				else if (levelLoad[(int)menuHierarchyArray[1]-1-sampleLevel.Length]!="No Saved Files") {
//					StartCoroutine(levelEdit.Open(PlayerPrefs.GetString(levelLoad[(int)menuHierarchyArray[1]-1-sampleLevel.Length].ToString(), "")));
//				}
//				Back();
//			}

	}	
	private void EditorMenu () {
//		if ((int)menuHierarchyArray[0]==0) {
//			caption = new string[]{"Continue", "Load Level", "Test", "Leave Editor", "Quit"};
//		}
//		if ((int)menuHierarchyArray[0]==0) {
//			caption = new string[]{"Test", "Tiles", "Colors", "New", "Open Menu", "Save Menu"};
//		}
		if ((int)menuHierarchyArray[0]==0) {
//			caption = new string[]{"Test", "Tiles", "Colors", "New", "Load Menu", "Save Menu", "Leave Editor", "Quit"};
			caption = new string[]{"Test", "Tiles", "New", "Load Menu", "Save Menu", "Leave Editor", "Quit"};
		}
		else if ((int)menuHierarchyArray[0]==1) {		// Test.
			levelTest = levelEdit.LevelToString();
			LevelEdit.playing = true;
			paused = false;										// Return to the game.
			Back();
		}
		else if ((int)menuHierarchyArray[0]==2) {		// Tiles Menu.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[levelEdit.titleArray.Length];
				for (int i=0; i<caption.Length; i++) {
					caption[i] = levelEdit.titleArray[i].name;
				}
			}
			else if ((int)menuHierarchyArray[1]>=1) {
//				levelEdit.CreateTile((int)menuHierarchyArray[2]-1+":0,0,0,1:-1,-1");
				levelEdit.CreateTile((int)menuHierarchyArray[1]-1+",-,-;0,0,0;0,0,0");
				paused = false;
				Back();
			}
//				for (int i=0; i<levelEdit.titleArray.Length; i++) {
//					if (GUI.Button(new Rect(0, i*buttonSize.y, buttonSize.x, buttonSize.y), titleArray[i].name)) {
//						levelEdit.CreateTile(i+",0");
//					}
//				}	
		}
//		else if ((int)menuHierarchyArray[0]==3) {		// Colors Menu.
//			caption = new string[]{};
//			WindowSize(new Vector2(scrollBoxSize.x*2, scrollBoxSize.y*2));
//			GUI.BeginGroup(new Rect(0, 0, windowPropertiesValue.z+(caption.Length*buttonSize.y>120 ? 16 : 0), windowPropertiesValue.w-(caption.Length*buttonSize.y>120 ? 8 : 0)));//, "AOEUEOA");//, style);
//
//			GUI.Label(new Rect(00, 50, 50, 25), "R", "Plain");
//			pretty.x = GUI.HorizontalSlider(new Rect(50, 50, 250, 25), Mathf.Round(pretty.x), 0, 255);
//			GUI.Label(new Rect(250, 50, 50, 25), ""+pretty.x, "Plain");
//
//			GUI.Label(new Rect(00, 100, 50, 25), "G", "Plain");
//			pretty.y = GUI.HorizontalSlider(new Rect(50, 100, 250, 25), Mathf.Round(pretty.y), 0, 255);
//			GUI.Label(new Rect(250, 100, 50, 25), ""+pretty.y, "Plain");
//
//			GUI.Label(new Rect(00, 150, 50, 25), "B", "Plain");
//			pretty.z = GUI.HorizontalSlider(new Rect(50, 150, 250, 25), Mathf.Round(pretty.z), 0, 255);
//			GUI.Label(new Rect(250, 150, 50, 25), ""+pretty.z, "Plain");
//			for (int i=0; i<wraping.Length; i++) {
//				wraping[i].color = new Color(pretty.x/255, pretty.y/255, pretty.z/255, 1);
//			}
//			GUI.EndGroup();
//		}
		else if ((int)menuHierarchyArray[0]==3) {//4) {		// New.
			caption = new string[]{};
			WindowSize(new Vector2(scrollBoxSize.x*2, scrollBoxSize.y*2));
//					GUI.BeginGroup(new Rect(windowPropertiesValue.x-(caption.Length*buttonSize.y>120 ? 8 : 0), windowPropertiesValue.y, windowPropertiesValue.z+(caption.Length*buttonSize.y>120 ? 16 : 0), windowPropertiesValue.w), "AOEUEOA");//, style);
			GUI.BeginGroup(new Rect(0, 0, windowPropertiesValue.z+(caption.Length*buttonSize.y>120 ? 16 : 0), windowPropertiesValue.w-(caption.Length*buttonSize.y>120 ? 8 : 0)));//, "AOEUEOA");//, style);
//			GUI.Label(new Rect(0, 0, scrollBoxSize.x+300, buttonSize.y), "Are you sure you want to replace this file?");
//			GUI.Label(new Rect(0, 0, scrollBoxSize.x+300, buttonSize.y), "Making new gird will erase everthing.");
			GUI.Label(new Rect(0, 0, scrollBoxSize.x+300, buttonSize.y), "New gird erases old grid.");
			if (GUI.Button(new Rect(00, 50, 50, 25), "<", "Plain")) {
				fresh.x = (fresh.x>1 ? fresh.x-1 : 16);
			}
			GUI.Box(new Rect(50, 50, 200, 50), ""+fresh.x);		// Outline of selected option;
			if (GUI.Button(new Rect(250, 50, 50, 25), ">", "Plain")) {
				fresh.x = (fresh.x<16 ? fresh.x+1 : 1);
			}
			// Y.
			if (GUI.Button(new Rect(00, 100, 50, 25), "<", "Plain")) {
				fresh.y = (fresh.y>1 ? fresh.y-1 : 16);
			}
			GUI.Box(new Rect(50, 100, 200, 50), ""+fresh.y);		// Outline of selected option;
			if (GUI.Button(new Rect(250, 100, 50, 25), ">", "Plain")) {
				fresh.y = (fresh.y<16 ? fresh.y+1 : 1);
			}
			// Z.
			if (GUI.Button(new Rect(00, 150, 50, 25), "<", "Plain")) {
				fresh.z = (fresh.z>1 ? fresh.z-1 : 16);
			}
			GUI.Box(new Rect(50, 150, 200, 50), ""+fresh.z);		// Outline of selected option;
			if (GUI.Button(new Rect(250, 150, 50, 25), ">", "Plain")) {
				fresh.z = (fresh.z<8 ? fresh.z+1 : 1);
			}
			if (GUI.Button(new Rect(50, 200, 150, 50), "Generate")) {
				levelEdit.train.terrainData.size = fresh*LevelEdit.cellSize;
				Back();
			}
			GUI.EndGroup();
		}
		else if ((int)menuHierarchyArray[0]==4) {//5) {		// Open Menu.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[levelEdit.levelGroup.Length];
				print(levelEdit.levelGroup.Length);
				for (int i=0; i<caption.Length; i++) {
					string[] load = levelEdit.levelGroup[i].Split(new char[]{'^'});
					caption[i] = load[0];
				}
			}
			if ((int)menuHierarchyArray[1]>=1) {
				StartCoroutine(levelEdit.Open(levelEdit.levelGroup[(int)menuHierarchyArray[1]-1]));
				Back();
				Back();		// Go to root because on Open we switch menues.
			}
		}
		else if ((int)menuHierarchyArray[0]==5) {//6) {		// Save Menu.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[]{"Yes, Save", "No, Don't"};
			}
			else if ((int)menuHierarchyArray[1]==1) {
				levelEdit.Save();
				print(levelEdit.Export());
//				AudioSource.Play(saved);
				Back();		// This is so it is only called once.
			}
			else if ((int)menuHierarchyArray[1]==2) {
				Back();
				Back();
			}
		}
		else if ((int)menuHierarchyArray[0]==6) {//7) {		// Leave Editor.
			LevelEdit.editor = false;
			Back();
		}
		else if ((int)menuHierarchyArray[0]==7) {//8) {		// Quit.
			QuitGame(1);
		}
	}

	private void QuitGame (int index) {
		if ((int)menuHierarchyArray[index]==0) {
			caption = new string[]{"Yes, Quit", "No, Don't"};
		}
		else if ((int)menuHierarchyArray[index]==1) {
			Application.Quit();
			Back();		// This is so it is only called once.
		}
		else if ((int)menuHierarchyArray[index]==2) {
			Back();
			Back();
		}
	}

	private void MainMenu () {
		if ((int)menuHierarchyArray[0]==0) {
			caption = new string[]{"Play Game", "Edit Custom Game", "Preferences", "Quit"};//"Info", "Quit"};
//			GUI.Box(new Rect((Screen.width/2)-175, 25, 350, 50), "Tank Game");
//			GUI.Box(new Rect((Screen.width/2)-100, 75, 200, 25), "By: Mem Dixy", "Author");
//			GUI.Box(new Rect((Screen.width/2)-100, 125, 200, 25), Application.loadedLevelName, "Author");
		}
		else if ((int)menuHierarchyArray[0]==1) {		// Play Game.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[]{"Start New Game", "Play Custom Game"};//, "Continue Saved Game", "View Your Solutions"};
			}
			else if ((int)menuHierarchyArray[1]==1) {		// Start New Game.
//				string load = "";
//				for (int i=0; i<sampleLevel.Length; i++) {
//					load += sampleLevel[i];
//					load += (i+1<sampleLevel.Length ? "\n" : "");
//				}
				string load = sampleLevel[0].ToString();
				levelEdit.levelGroup = load.Split(new char[]{'\n'});
				StartCoroutine(levelEdit.Open(levelEdit.levelGroup[0]));
				Back();
				Back();
			}
			else if ((int)menuHierarchyArray[1]==2) {		// Play Custom Game.
				caption = new string[]{};
				WindowSize(new Vector2(buttonSize.x+300, buttonSize.y*(sampleLevel.Length+levelLoad.Length+1)));
				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Import")) {		// Gets level on Desktop.
					print(levelEdit.Import(0));
					Back();
					Back();
				}
			}
//// Change function and file name.
//			else if ((int)menuHierarchyArray[1]==3) {		// Continue Saved Game.
//				caption = new string[]{};
//				WindowSize(new Vector2(buttonSize.x+300, buttonSize.y*(sampleLevel.Length+levelLoad.Length+1)));
//				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
//				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
//				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Import")) {		// Gets level on Desktop.
//					print(levelEdit.Import(0));
//					Back();
//				}
//			}
//// Change function and file name.
//			else if ((int)menuHierarchyArray[1]==4) {		// View Your Solutions.
//				caption = new string[]{};
//				WindowSize(new Vector2(buttonSize.x+300, buttonSize.y*(sampleLevel.Length+levelLoad.Length+1)));
//				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
//				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
//				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Import")) {		// Gets level on Desktop.
//					print(levelEdit.Import(0));
//					Back();
//				}
//			}
		}
		else if ((int)menuHierarchyArray[0]==2) {		// Edit Custom Game.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[]{"Create New", "Edit Existing"};//, "Download Games", "Submit Your Games"};
			}
			else if ((int)menuHierarchyArray[1]==1) {		// Create New.
//				caption = new string[]{};
//				WindowSize(new Vector2(buttonSize.x+100, buttonSize.y+300));
//				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
//				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
//				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Export")) {		// Gets level on Desktop.
//					print(levelEdit.Export());
//				}

				caption = new string[]{};
//				WindowSize(new Vector2(buttonSize.x+300, buttonSize.y*(sampleLevel.Length+levelLoad.Length+1)));
				WindowSize(new Vector2(buttonSize.x*1.5f, buttonSize.y*5));
				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Create")) {		// Gets level on Desktop.
					print(levelEdit.Export());
					LevelEdit.playing = false;
					LevelEdit.editor = true;
					Back();
					Back();
				}
				
			}
			else if ((int)menuHierarchyArray[1]==2) {		// Edit Existing.
				caption = new string[]{};
//				WindowSize(new Vector2(buttonSize.x+300, buttonSize.y*(sampleLevel.Length+levelLoad.Length+1)));
				WindowSize(new Vector2(buttonSize.x*1.5f, buttonSize.y*5));
				levelEdit.levelTitle = GUI.TextField(new Rect(0, 0, 235, buttonSize.y), levelEdit.levelTitle, 16);
				GUI.Label(new Rect(235, 0, scrollBoxSize.x, buttonSize.y), ".txt");
				if (GUI.Button(new Rect(0, (2*buttonSize.y), buttonSize.x, buttonSize.y), "Import")) {		// Gets level on Desktop.
					print(levelEdit.Import(0));
					LevelEdit.playing = false;
					LevelEdit.editor = true;
					Back();
					Back();
				}
			}
//			else if ((int)menuHierarchyArray[1]==3) {		// "Download Games".
//				Application.OpenURL("http://web.me.com/mem_dixy/Dixies_Journey_To_The_Mainframe/Story.html");
//				Back();
//			}
//			else if ((int)menuHierarchyArray[1]==4) {		// Submit Your Games.
//				Application.OpenURL("http://web.me.com/mem_dixy/Dixies_Journey_To_The_Mainframe/Help.html");
//				Back();
//			}
		}
		else if ((int)menuHierarchyArray[0]==3) {		// Preferences.
			if ((int)menuHierarchyArray[1]==0) {
				caption = new string[]{"Fullscreen", "Resolution", "Quality Levels"};//, "Sound"};
			}
			else if ((int)menuHierarchyArray[1]==1) {		// Fullscreen.
				Screen.fullScreen = !Screen.fullScreen;
				Back();
			}
			else if ((int)menuHierarchyArray[1]==2) {		// Resolution.
				Resolution[] screenSizes = Screen.resolutions;
				if ((int)menuHierarchyArray[2]==0) {
					caption = new string[screenSizes.Length];
					for (int i=0; i<caption.Length; i++) {
						caption[i] = ""+screenSizes[i].width+" "+screenSizes[i].height;
					}			
				}
				else if ((int)menuHierarchyArray[2]>=1) {
					Screen.SetResolution(screenSizes[(int)menuHierarchyArray[2]-1].width,screenSizes[(int)menuHierarchyArray[2]-1].height, Screen.fullScreen);
					Back();
				}
			}
			else if ((int)menuHierarchyArray[1]==3) {		// Quality Levels.
				if ((int)menuHierarchyArray[2]==0) {
					caption = new string[]{"Fastest", "Fast", "Simple", "Good", "Beautiful", "Fantastic"};
				}
				else if ((int)menuHierarchyArray[2]==1) {
					QualitySettings.currentLevel = QualityLevel.Fastest;
					Back();
				}
				else if ((int)menuHierarchyArray[2]==2) {
					QualitySettings.currentLevel = QualityLevel.Fast;
					Back();
				}
				else if ((int)menuHierarchyArray[2]==3) {
					QualitySettings.currentLevel = QualityLevel.Simple;
					Back();
				}
				else if ((int)menuHierarchyArray[2]==4) {
					QualitySettings.currentLevel = QualityLevel.Good;
					Back();
				}
				else if ((int)menuHierarchyArray[2]==5) {
					QualitySettings.currentLevel = QualityLevel.Beautiful;
					Back();
				}
				else if ((int)menuHierarchyArray[2]==6) {
					QualitySettings.currentLevel = QualityLevel.Fantastic;
					Back();
				}
			}
//			else if ((int)menuHierarchyArray[1]==4) {		// Sound.
//				caption = new string[]{};
//				WindowSize(new Vector2(scrollBoxSize.x*2, scrollBoxSize.y*2));
//				GUI.Label(new Rect(00, 50, 50, 25), "0", "Plain");
//				sound = GUI.HorizontalSlider(new Rect(50, 50, 250, 25), Mathf.Round(sound), 0, 100);
//				GUI.Label(new Rect(250, 50, 50, 25), "100", "Plain");
//				GUI.Label(new Rect(300, 50, 50, 25), ""+sound, "Plain");
//				AudioListener.volume = sound/100;
//			}
		}
//		else if ((int)menuHierarchyArray[0]==4) {		// Info.
//			if ((int)menuHierarchyArray[1]==0) {
//				caption = new string[]{"High Scores", "Credits", "Visit Game Website", "Game Instructions"};
//			}
//			else if ((int)menuHierarchyArray[1]==1) {		// High Scores.
//				Application.LoadLevel(1);
//				Back();
//			}
//			else if ((int)menuHierarchyArray[1]==2) {		// Credits.
//				caption = new string[]{};
//				WindowSize(new Vector2(scrollBoxSize.x*2, scrollBoxSize.y*2));
//				GUI.BeginGroup(new Rect(0, 0, windowPropertiesValue.z+(caption.Length*buttonSize.y>120 ? 16 : 0), windowPropertiesValue.w-(caption.Length*buttonSize.y>120 ? 8 : 0)));//, "AOEUEOA");//, style);
//				GUI.Box(new Rect(0, 25, 350, 50), "Dixie's Journey To The Mainframe");
//				GUI.Box(new Rect(0, 75, 200, 25), "By: Mem Dixy", "Author");
//				GUI.Box(new Rect(0, 125, 200, 25), Application.loadedLevelName, "Author");
//				GUI.EndGroup();
//			}
//			else if ((int)menuHierarchyArray[1]==3) {		// "Visit Game Website".
//				Application.OpenURL("http://web.me.com/mem_dixy/Dixies_Journey_To_The_Mainframe/Story.html");
//				Back();
//			}
//			else if ((int)menuHierarchyArray[1]==4) {		// Game Instructions.
//				Application.OpenURL("http://web.me.com/mem_dixy/Dixies_Journey_To_The_Mainframe/Help.html");
//				Back();
//			}
//		}
		else if ((int)menuHierarchyArray[0]==4) {//5) {		// Quit.
			QuitGame(1);
		}
	}
	private void SwitchPlayer (PlayerBasic other) {
		if (selectedPlayer) {
			selectedPlayer.human = false;
		}
		selectedPlayer = other;
		selectedPlayer.human = true;
	}
}

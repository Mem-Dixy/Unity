using UnityEngine;
using System.Collections;

public class LevelEdit : MonoBehaviour {
	// Global.
	public static int cellSize = 16;
	public static int rotateSpeed = 180;
	public static int translateSpeed = 45;
	public static bool playing = false;
	public static bool editor = false;
	// Shared with gameController.
	public string levelTitle = "TestFile";
	public string lastLoad = "";	// Stores the data of the last level loaded.
		// File variables.
	public string levelName = "";	// What the level is called.
	public string levelMaker = "";	// Who made the level.
	public int playerScore = 0;	// The player's score.
	public int highScore = 0;		// The level's high score.
	public Vector3 dimensions = Vector3.zero;	// Holds the data for the level size.
	// GUI Skins.
	public GUISkin gUISkin;
//	public GUIStyle style = new GUIStyle();
	//
	private Vector3 gridSize = new Vector3();
	// Tiles and tile modifications.
	public Transform[] titleArray;
	public Transform stationary;
	public Transform corupted;
	//	
	public Terrain train;
	private Placer placer;
	public float loadCurrent = 0;

	public Transform activeTile;		// This is what follows the camera/current tile.
	public bool isLoading = false;
	public int floor = 0;		// Tells us what floor we are on.


	public string[] levelGroup;		// A collection of a set of levels (groups of 50).
	public int selectedLevel = 0;	// Stores the level we are currently playing/working on.
	// Localise ettings.
	private Transform myTransForm;
//	private Transform self;
//	private Camera view;

	public void Start () {
		myTransForm = transform;
		placer = AssetLoad.assetLoad.GetComponent(typeof(Placer)) as Placer;
	}
	public void Update () {
		if (isLoading || playing || !editor) {
			return;
		}
		float inputMoveX = Input.GetAxis("Slip");
		float inputMoveY = Input.GetAxis("Heave");
		float inputMoveZ = Input.GetAxis("Surge");
		if (playing) {
			inputMoveX = 0;
			inputMoveY = 0;
			inputMoveZ = 0;
		}
		Camera camMain = Camera.main;
		float moveSpeed = 20;
		//camMain.transform.rotation = Quaternion.Euler(Vector3(euler.x+inputSpinX, euler.y+inputSpinY, euler.z+inputSpinZ ));
		camMain.transform.Translate(new Vector3(inputMoveX, inputMoveZ, 0) * Time.deltaTime * moveSpeed);
		camMain.orthographicSize += -inputMoveY * Time.deltaTime * moveSpeed;
		camMain.orthographicSize = Mathf.Clamp(camMain.orthographicSize, 8, Mathf.Infinity);
		camMain.transform.position = new Vector3(camMain.transform.position.x, floor*cellSize+cellSize+0.4f, camMain.transform.position.z);
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			floor = (floor>0 ?  floor-1 : Mathf.RoundToInt(gridSize.y)-1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			floor = (floor<Mathf.RoundToInt(gridSize.y)-1 ? floor+1 : 0);
		}
		floor = Mathf.Clamp(floor, 0, Mathf.RoundToInt(gridSize.y));	// In the event that grid size is 0 we don't want it becoming -1.

		//
		//
		//
		if (PauseMenu.paused) {		// Prevent tile from being picked up while paused.
			return;
		}
		Vector3 location = camMain.ScreenToWorldPoint(Input.mousePosition);				// Gets the mouse position in the form of a ray.
		if (activeTile) {														// Check to see if we are holding a tile.
			Vector3 placement;
//			placement.x = Mathf.Clamp(location.x, 0, train.terrainData.size.z);
//			placement.y = Mathf.Clamp(location.y, 0, train.terrainData.size.y);
//			placement.z = Mathf.Clamp(location.z, 0, train.terrainData.size.z);
			placement.x = Mathf.Clamp(Mathf.Round(location.x/cellSize)*cellSize, 0, train.terrainData.size.x);
			placement.y = Mathf.Clamp(floor*cellSize, 0, train.terrainData.size.y);
			placement.z = Mathf.Clamp(Mathf.Round(location.z/cellSize)*cellSize, 0, train.terrainData.size.z);
//			placement.x = Mathf.Clamp(Mathf.Round(location.x/cellSize)*cellSize, 0, (gridSize.x-1)*cellSize);
//			placement.y = Mathf.Clamp(floor*cellSize, 0, gridSize.y*cellSize);
//			placement.z = Mathf.Clamp(Mathf.Round(location.z/cellSize)*cellSize, 0, (gridSize.z-1)*cellSize);
			activeTile.position = placement;
//			activeTile.position = new Vector3(Mathf.Round(location.x/cellSize), 0, Mathf.Round(location.z/cellSize))*cellSize;// Edit: frest * cellsize?
			if (Input.GetButtonDown("Fire1")) {
				TilePlacement();
			}
			else if (Input.GetKeyDown(KeyCode.Backspace)||Input.GetKeyDown(KeyCode.Delete)) {		// Delete the tile.
				Destroy(activeTile.gameObject);		// Delete tile.
				activeTile = null;					// Destroying tile doesn't make this null, so manualy set it to null.
			}
	// (FIX?) Rotate the tile. (ADD?) Make it rotate both ways.
			else if (Input.GetButtonDown("Fire2")) {
				activeTile.transform.Rotate(0, 90, 0, Space.World);
			}
		}
		else {
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(camMain.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<30) && Input.GetButtonDown("Fire1")) {	// Only use tile layer (currently 30).
				activeTile = hit.transform;
				if (activeTile.parent) {
					activeTile.parent = null;
				}
			}
			else if (Input.GetButtonDown("Fire3") && hit.transform!=null) {	// Copy.
				// Non functonal.
				CreateTile(hit.transform.name);		// Clone this tile.
			}
		}
	}
	private void TilePlacement () {
		if (!activeTile) {
			return;
		}
		activeTile.position = new Vector3(Mathf.Clamp(activeTile.position.x, 0, train.terrainData.size.z), Mathf.Clamp(activeTile.position.y, 0, train.terrainData.size.y), Mathf.Clamp(activeTile.position.z, 0, train.terrainData.size.z));
//		activeTile.position = new Vector3(Mathf.Clamp(Mathf.Round(activeTile.position.x/cellSize)*cellSize, 0, gridSize.x), Mathf.Clamp(Mathf.Round(activeTile.position.y/cellSize)*cellSize, 0, gridSize.y), Mathf.Clamp(Mathf.Round(activeTile.position.z/cellSize)*cellSize, 0, gridSize.z));
//		activeTile.position = new Vector3(Mathf.Round(activeTile.position.x/cellSize), Mathf.Round(activeTile.position.y/cellSize), Mathf.Round(activeTile.position.z/cellSize))*cellSize;
		if (placer.Check()) {
			activeTile.parent = myTransForm;
			activeTile = null;
		}
	}
	private void OnGUI () {
		GUI.skin = gUISkin;
//		if (isLoading) {
//			string loader = (loadCurrent*100).ToString();
//			loader += (loader.Length<3 ? ".0" : "");
//			GUI.Label(new Rect(Screen.width/2-100, Screen.height/2, 200, 50), "Loading "+loader+"%", "Author");
//		}
		if (!isLoading && !playing && editor) {
			GUI.Label(new Rect(0, 0, 200, 25), "Editor Floor", "You Win");
			GUI.Box(new Rect(16, 25, 60, 50), ""+floor);		// Outline of selected option;
			
			GUI.Label(new Rect(0, 50, 200, 25), "Level Name", "You Win");
			levelName = GUI.TextField(new Rect(216, 50, 160, 50), levelName, 16);		// Outline of selected option;
			GUI.Label(new Rect(0, 100, 200, 25), "Level Maker", "You Win");
			levelMaker = GUI.TextField(new Rect(216, 100, 160, 50), levelMaker, 16);		// Outline of selected option;
			
		}
//		if (!isLoading && !playing && editor) {
//			GUI.Label(new Rect(25, 275, 200, 25), "Editor Floor", "You Win");
//			if (GUI.Button(new Rect(0, 300, 50, 25), "<", "You Win")||Input.GetKeyDown(KeyCode.Alpha1)) {
//				floor = (floor>0 ?  floor-1 : Mathf.RoundToInt(gridSize.y)-1);
//			}
//			GUI.Box(new Rect(50, 300, 60, 50), ""+floor);		// Outline of selected option;
//			if (GUI.Button(new Rect(100, 300, 50, 25), ">", "You Win")||Input.GetKeyDown(KeyCode.Alpha2)) {
//				floor = (floor<Mathf.RoundToInt(gridSize.y)-1 ? floor+1 : 0);
//			}
//		}
	}
	public void CreateTile (string input) {
//		input = "0,1,-1;0,0,0;0,0,0";
//		input = "";
//		input = "1,1,1;1,1,1;1,1,1";
//		input = "";
//		input = "0,-,-;0,0,0;0,0,0";

		
		int index = 0;
		bool use1 = false;
		bool use2 = false;
		Vector3 location = Vector3.zero;
		Vector3 degree = Vector3.zero;

		string[] array = input.Split(new char[]{';'});		// This is not yet in effect in the level data builder.
		string[] subArray = array[0].Split(new char[]{','});
		int.TryParse(subArray[0].ToString(), out index);
		Transform prefab = titleArray[Mathf.Clamp(index, 0, titleArray.Length-1)];
		if (subArray.Length-1>=1) {
			int number;
			use1 = int.TryParse(subArray[1].ToString(), out number);
			if (subArray.Length-1>=2) {
				use2 = int.TryParse(subArray[2].ToString(), out number);
			}
		}
		if (array.Length-1>=1) {		// Get the position.
			subArray = array[1].Split(new char[]{','});
			float.TryParse(subArray[0].ToString(), out location.x);
			if (subArray.Length-1>=1) {
				float.TryParse(subArray[1].ToString(), out location.y);
				if (subArray.Length-1>=2) {
					float.TryParse(subArray[2].ToString(), out location.z);
				}
			}
		}
		if (array.Length-1>=2) {		// Get the rotation.
			subArray = array[2].Split(new char[]{','});
			float.TryParse(subArray[0].ToString(), out degree.x);
			if (subArray.Length-1>=1) {
				float.TryParse(subArray[1].ToString(), out degree.y);
				if (subArray.Length-1>=2) {
					float.TryParse(subArray[2].ToString(), out degree.z);
				}
			}
		}
		if (prefab) {
			if (activeTile) {
				Destroy(activeTile.gameObject);
			}
			activeTile = Instantiate(prefab, myTransForm.position+location, Quaternion.Euler(myTransForm.eulerAngles+degree)) as Transform;
			activeTile.name = ""+index;
//			if (!activeTile.collider) {
//				activeTile.gameObject.AddComponent(typeof(MeshCollider));
//			}
//			activeTile.collider.isTrigger = true;
			if (use1) {
				Transform station = Instantiate(stationary) as Transform;
				station.parent = activeTile;
				station.localPosition = Vector3.zero;
				station.localRotation = Quaternion.identity;
			}
			if (use2) {
				Transform corupt = Instantiate(corupted) as Transform;
				corupt.parent = activeTile;
				corupt.localPosition = Vector3.zero;
				corupt.localRotation = Quaternion.identity;
			}
		}
	}

	public IEnumerator Open (string line) {		// Opens a level (not a group).
		if (line==null) {
			yield break;
		}
		lastLoad = line;
		print(lastLoad);
		isLoading = true;
		playing = false;
		if (activeTile) {
			Destroy(activeTile.gameObject);
			yield return 0;			// Allow items to be deleted.
		}

		levelName = "";
		levelMaker = "";
		playerScore = 0;
		highScore = 0;
		dimensions = Vector3.zero;
		
		string[] array = line.Split(new char[]{'^'});		// This is not yet in effect in the level data builder.
		string[] subArray = array[0].Split(new char[]{','});
		levelName = subArray[0];
		if (subArray.Length-1>=1) {
			levelMaker = subArray[1];
		}
		if (array.Length-1>=1) {		// Get the scores.
			subArray = array[1].Split(new char[]{','});
			int.TryParse(subArray[0].ToString(), out playerScore);
			if (subArray.Length-1>=1) {
				int.TryParse(subArray[1].ToString(), out highScore);
			}
		}
		if (array.Length-1>=2) {		// Get the scores.
			subArray = array[2].Split(new char[]{','});
			float.TryParse(subArray[0].ToString(), out dimensions.x);
			if (subArray.Length-1>=1) {
				float.TryParse(subArray[1].ToString(), out dimensions.y);
				if (subArray.Length-1>=2) {
					float.TryParse(subArray[2].ToString(), out dimensions.z);
				}
			}
		}
		train.terrainData.size = dimensions;
		if (array.Length-1>=3) {		// Get the rotation.
			subArray = array[3].Split(new char[]{':'});
			for (int i=0; i<subArray.Length; i++) {
				loadCurrent = i/subArray.Length;		// Show how much of the level has been loaded.
				CreateTile(subArray[i]);		// Make tile.
				TilePlacement();				// Place tile.
			}
			if (activeTile) {
				Destroy(activeTile.gameObject);		// Remove tile so that we don't start out holding a random tile.
			}
		}
		isLoading = false;
		if (editor) {
			playing = false;
			GameController.paused = false;										// Return to the game.			
		}
		else {
			playing = true;
			GameController.paused = !playing;										// Return to the game.
		}
		print(Time.time);
	}
	public void Save () {
		string output = levelName+","+levelMaker+"^"+playerScore+","+highScore+"^"+train.terrainData.size.x+","+train.terrainData.size.y+","+train.terrainData.size.z+"^";
		System.Collections.Generic.List<Transform> list = new System.Collections.Generic.List<Transform>();
		Component[] find = GetComponentsInChildren(typeof(Transform)) as Component[];
		for (int i=0; i<find.Length; i++) {
			if (find[i].gameObject!=gameObject) {
				if (find[i].transform.parent==transform) {
					list.Add(find[i].transform);
				}
			}
		}
		for (int i=0; i<list.Count; i++) {
			Transform found = list[i];
			output += found.name+",-,-"+";";			// Edit: Make this detect quality's in children.
			string pos = found.localPosition.ToString();
			for (int j=1; j<pos.Length-1; j++) {
				output += pos[j];
			}
			output += ";";
			string rot = found.transform.localEulerAngles.ToString();
			for (int j=1; j<rot.Length-1; j++) {
				output += rot[j];
			}
			if (i+1<list.Count) {
				output += ":";		// This will not show up if on final tile.
			}
		}
		if (levelGroup.Length<=0) {
			levelGroup = new string[1];
		}
		levelGroup[selectedLevel] = output;
	}

//	public void Read () {
//		print("Open "+levelTitle);
//		string output = PlayerPrefs.GetString(levelTitle, "");
//		StartCoroutine(Open(output));
//	}
//	public void Save () {
//		print("Save "+levelTitle);
//		string output = LevelToString();
//		PlayerPrefs.SetString(levelTitle, output);
//		PlayerPrefs.SetString("Levels", PlayerPrefs.GetString("Levels", "Null")+"/"+levelTitle);
//	}
	public string Import (int level) {
		try {
			using (System.IO.StreamReader file = new System.IO.StreamReader(levelTitle+".txt")) {
				string output = "";
				string input = file.ReadLine();
				while (input!=null) {
					output += input;
					input = file.ReadLine();
					if (input!=null) {
						output += "\n";
					}
				}
				levelGroup = output.Split(new char[]{'\n'});
				StartCoroutine(Open(levelGroup[level<levelGroup.Length ? level : 0]));
			}
		}
		catch (System.Exception bug) {
			return "The file could not be opened: "+bug.Message;
		}
		return "Imported "+levelTitle+".txt";
	}
	public string Export () {
		try {
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(levelTitle+".txt")) {
				foreach (string line in levelGroup) {
					file.WriteLine(line);
				}
//				string[] output = LevelToString().Split(new char[]{'\n'});
//				for (int i=0; i<output.Length; i++) {
//					file.WriteLine(output[i]);
//				}
			}
		}
		catch (System.Exception bug) {
			return "The fileSave could not be saved: "+bug.Message;
		}
		return "Exported "+levelTitle+".txt";
	}
	public string LevelToString () {
		Component[] find = GetComponentsInChildren(typeof(Transform)) as Component[];
		string output = levelName+","+levelMaker+"^"+playerScore+","+highScore+"^"+train.terrainData.size.x+","+train.terrainData.size.y+","+train.terrainData.size.z+"^";
		for (int i=0; i<find.Length; i++) {
			if (find[i].gameObject!=gameObject) {
				if (find[i].transform.parent==transform) {
					Transform found = find[i] as Transform;
					output += found.name+",-,-"+";";			// Edit: Make this detect quality's in children.
					string pos = found.localPosition.ToString();
					for (int j=1; j<pos.Length-1; j++) {
						output += pos[j];
					}
					output += ";";
					string rot = found.transform.localEulerAngles.ToString();
					for (int j=1; j<rot.Length-1; j++) {
						output += rot[j];
					}
					output += ":";								// Edit: Make this not show up if on final tile.
				}
			}
		}
		string holdString = output;
		output = "";
		for (int i=0; i<holdString.Length-1; i++) {		// This is to delete the extra ":" on the end of the file.
			output += holdString[i];
		}	
		return output;
	}
//	
//	private void OnDrawGizmos () {
//		float startRadius = 8;
//		float endRadius = startRadius*2-Mathf.Sqrt(Mathf.Pow(startRadius, 2)*3);
//		float location = startRadius+endRadius;
//		Vector3 offset = Vector3.one;
//		Gizmos.DrawSphere(Vector3.zero, startRadius);
//		for (int i=0; i<8; i++) {
//			Gizmos.DrawSphere(offset.normalized*location, endRadius);
//			offset = new Vector3(Mathf.Sign(i%2-1), Mathf.Sign(i%4-2), Mathf.Sign(i-4));
//		}
//	}
//
}
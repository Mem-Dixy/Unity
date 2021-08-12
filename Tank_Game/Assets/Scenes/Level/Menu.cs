using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public static bool paused = true;			// Controlls pause menu.		// Used to see if we are on main menu or playing game (we want BZFlag menu style).
	//
//	public PlayerBasic basic;
	public GUISkin gUISkin;
	public Vector2 buttonSize = new Vector2();
	public Vector2 windowPadding = new Vector2();						// How much spacing is between the buttons and the window.

	public Vector4 scrollBoxSize = new Vector4();
	public TextAsset[] sampleLevel = new TextAsset[0];
//	public GUIStyle style = new GUIStyle();
	//
//	private Helicopter helicopter;
//	private bool winner = false;
//	private int cameraType = 1;						// Public so players can get value.
//	private Vector3 fresh = new Vector3();
	//
//	private Transform self;
//	private Camera view;

//	private PlayerBasic human;				// This is the object we use to spawn in and out of veichels.

	public ArrayList menuHierarchyArray = new ArrayList();		// This stores all the info on what menus to show.

	public Vector4 windowPropertiesValue = new Vector4();		// Actual size and position of window.
	public Vector4 windowPropertiesTarget = new Vector4();		// Size and postion we want the window to be at.
	public string[] caption = new string[]{"Play", "Info", "Email", "Quit"};
	public string[] zone = new string[]{""};
	private Vector2 scrollPosition = Vector2.zero;

	public string heading;		// This is what is displayed at the top of each menu.
	
	private LayerMask teamLayer;
	private LayerMask camLayer;
//	private int field = 0;		// Tells us what floor we are on.


	Vector2 biggest = new Vector2(20, 20);
	public int here = 0;



	public LevelEdit edit;
//	private void Start () {
//		
//	}
	public void OnGUI () {	
		GUI.skin = gUISkin;		// This tells how the text will display.
		GUI.BeginGroup(new Rect(Mathf.Max(biggest.x, windowPropertiesValue.x-windowPadding.x), Mathf.Max(biggest.y, windowPropertiesValue.y-windowPadding.y-buttonSize.y), Mathf.Min(Screen.width-biggest.x*2, windowPropertiesValue.z+windowPadding.x*2), Mathf.Min(Screen.height-biggest.y*2, windowPropertiesValue.w+windowPadding.y*2+buttonSize.y*2)), "MEOW");//, style);
		GUI.Box(new Rect(0, 0, Mathf.Min(Screen.width-biggest.x*2, windowPropertiesValue.z+windowPadding.x*2), Mathf.Min(Screen.height-biggest.y*2, windowPropertiesValue.w+windowPadding.y*2+buttonSize.y*2)), "");	// Empty growing box.	// Fix issue when we win.
		if (windowPropertiesValue==windowPropertiesTarget) {	// Only show buttons when window is done resizeing itself.
			GUI.Label(new Rect(windowPadding.x, windowPadding.y, windowPropertiesValue.z, buttonSize.y), heading);		//+(windowPropertiesValue.w-buttonSize.y*2<scrollBoxSize.w ? 16 : 0)
			if (GUI.Button(new Rect(windowPadding.x, Mathf.Min(Screen.height-biggest.y*2-windowPadding.y-buttonSize.y, windowPadding.y+windowPropertiesValue.w+buttonSize.y), buttonSize.x, buttonSize.y), (here==0 ? "Resume Game" : "Back To Menu"))) {
				if (menuHierarchyArray.Count<=1) {
					paused = false;										// Return to the game.
				}
				else {
					Back();
				}
			}
			GUI.BeginGroup(new Rect(windowPadding.x, windowPadding.y+buttonSize.y, windowPropertiesValue.z, Mathf.Min(Screen.height-biggest.y*2, windowPropertiesValue.w)), "MEOW");//, style);
			scrollPosition = GUI.BeginScrollView(new Rect(0, 0, windowPropertiesValue.z, Mathf.Min(Screen.height-biggest.y*2-windowPadding.y*2-buttonSize.y*2, windowPropertiesValue.w)), scrollPosition, new Rect(0, 0, windowPropertiesValue.z, windowPropertiesValue.w));//caption.Length));
			
			if (paused) {		// We are on the main menu.
//				edit.Edit(this);
			}
			else {
				//SendMessage("Edit", this);
				//edit.GameMenu();
			}

//			for (int i=0; i<caption.Length; i++) {
//				if (GUI.Button(new Rect(0, buttonSize.y*i, buttonSize.x, buttonSize.y), caption[i])) {
////					menuHierarchyArray[menuHierarchyArray.Count-1] = i+1;		// i starts at 0 while menu starts at 1.				// Tell it what button we want.
//					menuHierarchyArray.Add(i);								// Then move up one level.
//				}
//			}
			GUI.EndScrollView();
			GUI.EndGroup();				
		}
		GUI.EndGroup();				
		windowPropertiesValue.x += (windowPropertiesValue.x==windowPropertiesTarget.x ? 0 : Mathf.Sign(windowPropertiesTarget.x-windowPropertiesValue.x));
		windowPropertiesValue.y += (windowPropertiesValue.y==windowPropertiesTarget.y ? 0 : Mathf.Sign(windowPropertiesTarget.y-windowPropertiesValue.y));
		windowPropertiesValue.z += (windowPropertiesValue.z==windowPropertiesTarget.z ? 0 : Mathf.Sign(windowPropertiesTarget.z-windowPropertiesValue.z));
		windowPropertiesValue.w += (windowPropertiesValue.w==windowPropertiesTarget.w ? 0 : Mathf.Sign(windowPropertiesTarget.w-windowPropertiesValue.w));
	}
	public void Back () {
		menuHierarchyArray.RemoveAt(menuHierarchyArray.Count-1);		// Remove last item in array.
		menuHierarchyArray[menuHierarchyArray.Count-1] = 0;				// Set current state to zero.
	}

}

public class MyWindow : UnityEditor.EditorWindow {
	public UnityEngine.Object[] source = new UnityEngine.Object[0];
	private void sourceObjectField (ref UnityEngine.Object[] array, System.Int32 index) {
		System.String label = System.String.Format("Source {0}", index);
		UnityEngine.Object obj = array[index];
		System.Type objType = typeof(UnityEngine.GameObject);
		System.Boolean allowSceneObjects = true;
		array[index] = UnityEditor.EditorGUILayout.ObjectField(label, obj, objType, allowSceneObjects);
	}
	public System.Int32 repeat = 0;


    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
	public void stark (UnityEngine.GameObject original, UnityEngine.GameObject spawn, System.Int32 remain) {
		UnityEngine.Debug.Log ("remaining " + remain);
		remain -= 1;
		if (remain < 0) {
			return;
		}
		UnityEngine.Transform parent = original.transform;
		UnityEngine.GameObject instantiate = Instantiate(original, spawn.transform);
		instantiate.name = System.String.Format("Spawn {0}", remain);

		UnityEngine.Transform item = instantiate.transform;
		//item.parent = parent.parent;
		item.Translate (parent.localPosition);
		item.Rotate (parent.localRotation.eulerAngles);
		//item.AddComponent<VRCSDK2.VRC_EventHandler>();
		//item.AddComponent<VRCSDK2.VRC_Trigger>();
		//VRCSDK2.VRC_Trigger trigger = item.GetComponent<VRCSDK2.VRC_Trigger>();
		//trigger.Triggers = AddTriggers();
		stark (original, instantiate, remain);
	}
	private System.Collections.Generic.List<VRCSDK2.VRC_Trigger.TriggerEvent> AddTriggers(){
		System.Collections.Generic.List<VRCSDK2.VRC_Trigger.TriggerEvent> list;
		list = new System.Collections.Generic.List<VRCSDK2.VRC_Trigger.TriggerEvent> ();
		list.Add (AddTriggerEvent ());
		return list;
	}
	private VRCSDK2.VRC_Trigger.TriggerEvent AddTriggerEvent() {
		VRCSDK2.VRC_Trigger.TriggerEvent item;
		item = new VRCSDK2.VRC_Trigger.TriggerEvent();
		item.TriggerType = VRCSDK2.VRC_Trigger.TriggerType.OnAvatarHit;
		return item;
	}

    // Add menu item named "My Window" to the Window menu
    [UnityEditor.MenuItem ("Window/My Window")]
    public static void ShowWindow() {
        //Show existing window instance. If one doesn't exist, make one.
        UnityEditor.EditorWindow.GetWindow(typeof(MyWindow));
    }
    public void OnGUI () {
        UnityEngine.GUILayout.Label ("Base Settings", UnityEditor.EditorStyles.boldLabel);
		for (System.Int32 index = 0; index < source.Length; index += 1) {
			sourceObjectField (ref source, index);
		}
		if (UnityEngine.GUILayout.Button("Add Source")) {
			System.Collections.Generic.List<UnityEngine.Object> list = new System.Collections.Generic.List<UnityEngine.Object> ();
			list.AddRange (source);
			list.Add (new UnityEngine.Object ());
			source = list.ToArray ();
		}
        groupEnabled = UnityEditor.EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
		repeat = UnityEditor.EditorGUILayout.IntField ("Repeat Count", repeat);
        myBool = UnityEditor.EditorGUILayout.Toggle ("Toggle", myBool);
        myFloat = UnityEditor.EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
        UnityEditor.EditorGUILayout.EndToggleGroup ();
		if (UnityEngine.GUILayout.Button("Click")) {
			for (System.Int32 index = 0; index < source.Length; index += 1) {
				UnityEngine.GameObject original = (UnityEngine.GameObject)source [index];
				//stark (original, original, repeat);
				System.Collections.IEnumerator fun = new Builder(original, repeat);

				EditorCoroutine.Start(fun);
			}
			UnityEngine.Debug.LogWarning ("Boop");
        }
    }
}
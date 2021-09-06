namespace AssemblyCSharp {
	public class main : UnityEngine.MonoBehaviour {
		private screen view;
		private void Awake () {
			useGUILayout = false;
			view = new screen();
		}
		private void Update () {
			float time = UnityEngine.Time.time;
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad0)) {
				UnityEngine.Debug.Log("down:" + time);
			}
			if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Keypad0)) {
				UnityEngine.Debug.Log("hold:" + time);
			}
			if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.Keypad0)) {
				UnityEngine.Debug.Log("up:" + time);
			}
			if (UnityEngine.Input.anyKeyDown) {
				System.UInt64 import = keyboard.keyboarding.type();
				if (import != keyboard.alias.error) {
					view.update(import);
				}
			}
		}
	}
}
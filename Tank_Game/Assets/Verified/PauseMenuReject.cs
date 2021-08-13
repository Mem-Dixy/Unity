using UnityEngine;
public class PauseMenuReject : MonoBehaviour {
	private PlayerBasic selectedPlayer;     // Who we are currently controling.
	private void Update() {
		if (!PauseMenu.paused && LevelEdit.playing) {       // While in the game we have a "get player" laser.
			RaycastHit hit = new RaycastHit();
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition) , out hit , Mathf.Infinity , 1 << 30)) {
				PlayerBasic get = hit.transform.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
				if (get) {
					SwitchPlayer(get);
				}
			}
		}
	}
	private void LateUpdate() {
		if (!PauseMenu.paused && LevelEdit.playing) {
			if (selectedPlayer != null) {       // While in the game we have a "get player" laser.
				transform.position = selectedPlayer.transform.position;
				transform.rotation = selectedPlayer.transform.rotation;
				transform.parent = selectedPlayer.transform;
				GetComponent<Camera>().orthographic = false;
			}
			else {
				transform.parent = null;
				transform.position = new Vector3(0 , 16.4f , 0);
				transform.rotation = Quaternion.Euler(90 , 0 , 0);
				GetComponent<Camera>().orthographic = true;
			}
		}
	}

	private void SwitchPlayer(PlayerBasic other) {
		if (selectedPlayer) {
			selectedPlayer.human = false;
		}
		selectedPlayer = other;
		selectedPlayer.human = true;
	}
}
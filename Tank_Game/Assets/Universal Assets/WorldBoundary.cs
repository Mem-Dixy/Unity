using UnityEngine;
using System.Collections;

public class WorldBoundary : MonoBehaviour {
	// If worried about it going so fast it flys out of level then add an array that stores objects that fly away
	// and if they don't enter into sphere again then delete them after a short pause;
	private void OnTriggerExit (Collider other) {
		PlayerBasic basic = other.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		if (basic) {
			basic.ResetPosition();
		}
		else {
			Destroy(other.gameObject);
		}
	}
}

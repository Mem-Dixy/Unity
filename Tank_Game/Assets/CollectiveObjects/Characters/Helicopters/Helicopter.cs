using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Helicopter : MonoBehaviour {
	
	private PlayerBasic basic;

	private void Start () {
		basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		basic.teamSide = transform.root.name;		// Set what team we are on.
		// Set controls availability.
	}
	private void Update () {
		if (basic.human) {
			basic.PlayerInput(transform, new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), Input.GetAxisRaw("Roll")), new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge")));
		}
		else if (basic.autopilot) {
			Vector3 destination = basic.GetDestination();
			basic.RotateTo(destination, transform, Vector3.one);
			basic.TranslateTo(destination, transform, Vector3.one);
		}
	}
}
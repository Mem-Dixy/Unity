using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Tank : MonoBehaviour {

	private PlayerBasic basic;

	private void Start () {
		basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic;
		basic.teamSide = transform.root.name;		// Set what team we are on.
		// Set controls availability.
	}
	private void Update () {
		if (basic.human) {
			basic.PlayerInput(transform, new Vector3(0, Input.GetAxisRaw("Slip"), 0), new Vector3(0, 0, Input.GetAxisRaw("Surge")));
			basic.PlayerInput(basic.laser, new Vector3(Input.GetAxisRaw("Pitch"), Input.GetAxisRaw("Yaw"), 0), Vector3.zero);
		}
		else if (basic.autopilot) {
			Vector3 destination = basic.GetDestination();
			basic.RotateTo(destination, transform, Vector3.up);
			basic.TranslateTo(destination, transform, Vector3.forward);
			basic.RotateTo((basic.tracking ? basic.tracking.position : Vector3.zero), basic.laser, Vector3.one);
		}
	}
}
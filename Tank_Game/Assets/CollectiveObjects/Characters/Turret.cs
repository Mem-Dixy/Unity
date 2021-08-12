using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerBasic))]

public class Turret : MonoBehaviour {

	private PlayerBasic basic;
	
	private void Start () {
		(basic = GetComponent(typeof(PlayerBasic)) as PlayerBasic).autopilot = true;
	}	
	private void Update () {
		if (basic.autopilot) {
			Vector3 destination = basic.GetDestination();
			if (destination==Vector3.zero) {
				destination = transform.position+transform.forward;
			}
			basic.RotateTo(destination, basic.laser, Vector3.one);
		}
		else {//if (canControll) {		// Is the player in this vehicle?
			basic.PlayerInput(transform, Vector3.zero, new Vector3(Input.GetAxisRaw("Slip"), Input.GetAxisRaw("Heave"), Input.GetAxisRaw("Surge")));
		}
	}
}
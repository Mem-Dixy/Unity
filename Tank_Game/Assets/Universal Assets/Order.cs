using UnityEngine;
using System.Collections;

public class Order : MonoBehaviour {
	
	private void FixedUpdate () {
		print(Time.time+"A");
		StartCoroutine(Moo(""));
		print(Time.time+"B");
	}
	public IEnumerator Moo (string input) {
		print(Time.time+"C");
		yield return new WaitForFixedUpdate();
		print(Time.time+"D");
	}
	public IEnumerator OnTriggerStay (Collider other) {
		print(Time.time+"E");
		yield return new WaitForFixedUpdate();
		print(Time.time+"F");
	}

	
//	private void Awake () {
//		print(Time.time+"A");
//	}
//	private void Start () {
//		StartCoroutine(Run());
//	}
//	private void FixedUpdate () {
//		print(Time.time+"H");
//	}
//	private void OnTriggerStay (Collider other) {
//		Debug.Log(Time.time+"F", transform);
//	}
//	private void OnTriggerEnter (Collider other) {
//		Debug.Log(Time.time+"E", transform);
//	}
//	private void OnTriggerExit (Collider other) {
//		Debug.Log(Time.time+"G", transform);
//	}
//	yield return new WaitForFixedUpdate();
//	private void Update () {
//		print(Time.time+"C");
//	}
//	yield return 0;
//	private void LateUpdate () {
//		print(Time.time+"D");
//	}
//	yield return new WaitForEndOfFrame();
//	private IEnumerator Run () {
//		print(Time.time+"I");
//		yield return 0;
//		print(Time.time+"J");
//		yield return 99999;
//		print(Time.time+"K");
//		yield return new WaitForFixedUpdate();
//		print(Time.time+"L");
//		yield return new WaitForEndOfFrame();
//		print(Time.time+"M");
//	}


//	private void Awake () {
//		print(Time.time+"A");
//	}
//	private void Start () {
//		print(Time.time+"B");
//	}
//	private void Update () {
//		print(Time.time+"C");
//	}
//	private void LateUpdate () {
//		print(Time.time+"D");
//	}
//	private void OnTriggerEnter (Collider other) {
//		print(Time.time+"E");
//	}
//	private void OnTriggerStay (Collider other) {
//		print(Time.time+"F");
//	}
//	private void OnTriggerExit (Collider other) {
//		print(Time.time+"G");
//	}
//	private void FixedUpdate () {
//		print(Time.time+"H");
//	}

}

using UnityEngine;
using System.Collections;

public class I : MonoBehaviour {

	public Transform hippo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		Collider[] lame = Physics.OverlapSphere(transform.position, 100);
//		if (lame[0]) {
//			if (lame[0].attachedRigidbody) {
//				transform.LookAt(lame[0].attachedRigidbody.transform);
//				Debug.Log("R", lame[0].attachedRigidbody.transform);
//			}
//			else {
//				transform.LookAt(lame[0].transform);
//			}
//			Debug.Log(lame[0], lame[0]);
//		}
//		else {
			transform.LookAt(hippo);
//		}
	}
	void OnDrawGizmos () {
		Gizmos.DrawSphere(transform.position, 100);
	}
		
}

//public void StatsCube (Vector3 dimensions) {
//	float Volume = dimensions.x*dimensions.y*dimensions.z;
//	float Area = dimensions.x*dimensions.y*dimensions.z;
//	float Area =  2(dimensions.x*dimensions.y) + (2*dimensions.x + 2*dimensions.y)*dimensions.z;
//	
//	Surface Area of Prism = 2(lw) + (2l + 2w)h = 2A + Ph
//}
//public void () {
//	
//}
//public void () {
//	
//}
using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private Camera camMain;
	private Transform bar;
	private PlayerBasic basic;
	
	private void Start () {
		camMain = Camera.main;
		Component[] tempArray = GetComponentsInChildren(typeof(Transform));
		for (int i=0; i<tempArray.Length; i++) {
			if (tempArray[i].transform!=transform) {
				bar = tempArray[i].transform;
			}
		}
		basic = transform.parent.GetComponent(typeof(PlayerBasic)) as PlayerBasic;
	}
	private void Update () {
		if (camMain) {
			transform.LookAt(camMain.transform);
		}
		if (basic && basic.healthMax!=0) {
			bar.localScale = new Vector3(Mathf.Clamp01(basic.healthCurrent/basic.healthMax), 1, 1);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour {
	public float big;
	public float small;
	
	private float ratio;
	private GameController found;
	
	public bool isRadar;
	public bool show;
//	Transform di;

	public void Start () {
		found = Camera.main.GetComponent(typeof(GameController)) as GameController;
		if (!found) {
			Debug.Log("Radar could not find GameController script");
		}
		ratio = (0.0f+(float)Screen.width)/(float)Screen.height;
		big = 512;	//120;
		small = 128;//40;
		if (isRadar) {
			GetComponent<Camera>().orthographicSize = small;	// 128.
			GetComponent<Camera>().rect = new Rect(1-(0.4f/ratio), 0f, 0.4f/ratio, 0.4f);
		}
		else {
			GetComponent<Camera>().orthographicSize = big;	// 512.
			GetComponent<Camera>().rect = new Rect((1-(0.9f/ratio))/2, 0.05f, 0.9f/ratio, 0.9f);
		}
	}
	
	public void Update () {
		if (!isRadar) {
			show = (Input.GetButtonDown("Map") ? !show : show);
			GetComponent<Camera>().enabled = show;		// Potention for performence enhancement but unnecessary at the moment.
		}
	}
	
	public void LateUpdate () {
		if (found && found.basic) {
			transform.rotation = Quaternion.Euler(90, (isRadar ? found.basic.transform.eulerAngles.y : 0), 0);
			transform.position = found.basic.transform.position+new Vector3(0, 512, 0);
		}
		else {
			found = Camera.main.GetComponent(typeof(GameController)) as GameController;
			found = GetComponent(typeof(GameController)) as GameController;
		}
	}
}
using UnityEngine;
using System.Collections;

public class TurretTarget : MonoBehaviour {
	public void InitiateCommand (Vector3 worldPoint, Vector3 upDirection, Vector3 axises, float turnSpeed) {		// Need to add limits.
		worldPoint = transform.InverseTransformPoint(worldPoint);
		Vector3 rotate = new Vector3(-Mathf.Atan2(worldPoint.y, worldPoint.z)*Mathf.Rad2Deg, Mathf.Atan2(worldPoint.x, worldPoint.z)*Mathf.Rad2Deg, Mathf.Atan2(transform.InverseTransformDirection(-upDirection).x, transform.up.y)*Mathf.Rad2Deg);
		rotate = Vector3.Scale(rotate, new Vector3(Mathf.Round(Mathf.Clamp01(axises.x)), Mathf.Round(Mathf.Clamp01(axises.y)), Mathf.Round(Mathf.Clamp01(axises.z))));
		transform.Rotate((rotate.sqrMagnitude>=rotate.normalized.sqrMagnitude ? rotate.normalized : rotate)*turnSpeed*Time.deltaTime);
	}
}
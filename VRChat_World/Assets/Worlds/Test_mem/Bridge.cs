public class Bridge : UnityEngine.MonoBehaviour {
    public void OnDrawGizmos () {
		UnityEngine.Gizmos.color = UnityEngine.Color.blue;
		UnityEngine.Gizmos.DrawCube (UnityEngine.Vector3.zero, UnityEngine.Vector3.one);
    }
}
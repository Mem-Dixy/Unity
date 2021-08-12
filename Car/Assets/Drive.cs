public class Drive : UnityEngine.MonoBehaviour {
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void Update() {
        Transform(0 , 1 , 0);
    }

    public void Transform(System.Single x , System.Single y, System.Single z = 0) {
        x *= UnityEngine.Time.deltaTime;
        y *= UnityEngine.Time.deltaTime;
        z *= UnityEngine.Time.deltaTime;
        UnityEngine.Vector3 translation = new UnityEngine.Vector3(x , y , z);
        transform.Translate(translation);
    }
}

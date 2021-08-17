using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{
    public GameObject destroyedVersion;
    
    void OnCollisionEnter(Collision collision)
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}

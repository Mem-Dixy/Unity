using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPole : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>() as Rigidbody;
        rigidbody.isKinematic = false;
    }
}

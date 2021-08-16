using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timedLife = 20;
    public float speed = 25;

    void FixedUpdate() {
        transform.Translate(0 , 0 , speed * Time.deltaTime);
	}
    // Update is called once per frame
    void Update()
    {
        timedLife -= Time.deltaTime;
        if (timedLife < 0) {
            Destroy(gameObject);
		}
    }
}

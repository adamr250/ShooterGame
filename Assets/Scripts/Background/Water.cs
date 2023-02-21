using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float speed = 2.0f;

    void Update()
    {
        float movement =  speed * Time.deltaTime;
        transform.Translate(0, -movement, 0);

        if (transform.position.y <= -10.0f) {
            transform.position = new Vector3(0.0f, 10.0f, 10.0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float speed = 2.0f;
    //public GameObject waterPrefab;
    //private Vector3 spawnPoint1 = new Vector3(0f, 0f, 1.0f);
    //private Vector3 spawnPoint2 = new Vector3(0f, 10f, 1.0f);

    void Update()
    {
        float movement =  speed * Time.deltaTime;
        transform.Translate(0, -movement, 0);

        if (transform.position.y <= -10.0f) {
            //Debug.Log("water y = -10");
            //Destroy(gameObject);
            transform.position = new Vector3(0.0f, 10.0f, 100.0f);
        }
    }
}

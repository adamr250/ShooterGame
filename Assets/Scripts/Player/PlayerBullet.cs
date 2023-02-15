using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float speed = 9.0f;
    private float lifeTime = 0.75f;


    void FixedUpdate()
    {
        float movement = speed * Time.deltaTime;
		transform.Translate(0, movement, 0);
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string objectTag = collision.gameObject.tag;
        if (objectTag == "Obstacle" || objectTag == "Enemy" || objectTag == "Boss")
        {   
            Destroy(gameObject);
        }
    }
}

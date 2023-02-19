using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed = 200.0f;
    private float lifeTime_Enemy = 1.5f;
    private float lifeTime_Boss = 2.5f;

    private Rigidbody2D body;

    private void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            gameObject.GetComponent<Rigidbody2D>().mass = 0.0001f;
        }
        body = gameObject.GetComponent<Rigidbody2D>();
        
        if(gameObject.tag == "EnemyBullet")
        {
            Destroy(gameObject, lifeTime_Enemy);
        }
        else if(gameObject.tag == "BossBullet")
        {
            Destroy(gameObject, lifeTime_Boss);
        }
    }

    void FixedUpdate()
    {
        body.velocity = body.transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player")
        {   
            Destroy(gameObject);
        }
    }
}

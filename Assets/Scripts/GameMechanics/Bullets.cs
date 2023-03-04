using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private float speed = 200.0f;

    private float[] bulletLifeTime = { 1.5f, 2.5f, 0.2f, 0.75f }; //Normal, Boss, Sniper, Player
    private float playerBulletSpeed = 400.0f;
    public static float bossBulletSpeed = 250.0f;

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

        string tagName = gameObject.tag;
        switch (tagName)
        {
            case "EnemyBullet":
                Destroy(gameObject, bulletLifeTime[0]);
                break;
            case "BossBullet":
                Destroy(gameObject, bulletLifeTime[1]);
                speed = bossBulletSpeed;
                break;
            case "SniperBullet":
                Destroy(gameObject, bulletLifeTime[2]);
                break;
            case "PlayerBullet":
                speed = playerBulletSpeed;
                Destroy(gameObject, bulletLifeTime[3]);
                break;
        }
    }

    void FixedUpdate()
    {
        body.velocity = body.transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag != "PlayerBullet")
        {
            if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            string objectTag = collision.gameObject.tag;
            if (objectTag == "Obstacle" || objectTag == "Enemy" || objectTag == "Boss")
            {
                Destroy(gameObject);
            }
        }
    }
}

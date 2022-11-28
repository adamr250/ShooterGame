using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    Score score;

    private GameObject scoreHolder;
    private float shootTimer = 0.0f;
    private Transform child;
    private Vector3 enemyGun;
    private Vector3 target;
    private Vector3 direction;

    public float shootCooldown = 2.0f;
    public GameObject bulletPref;
    public float speed = 3.0f;
    Rigidbody2D body;

    void Start()
    {
        scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
        score = scoreHolder.GetComponent<Score>();

        child = gameObject.transform.GetChild(0);
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        target = GameObject.Find("Player").transform.position;

        direction = target - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = rotation;
        direction.Normalize();

        enemyGun = child.transform.position;

        if (Time.time > shootTimer)
        {
            shootTimer = Time.time + shootCooldown;
            Instantiate(bulletPref, enemyGun, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
        }
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(target, transform.position);
        if(distance<3)
        {
            Debug.Log(distance);
            movement(direction);
        }
    }

    void movement(Vector2 dir)
    {
        body.MovePosition((Vector2)transform.position - (dir * speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            score.increaseScore(1000);
        }
    }
}

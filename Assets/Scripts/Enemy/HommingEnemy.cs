using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingEnemy : MonoBehaviour
{
    Score score;
    SpawnBuffs spawnBuffs;

    private GameObject buffsObject;
    private GameObject scoreHolder;
    private Vector3 direction;

    [SerializeField] private float speed; //3.5f

    Rigidbody2D body;

    private float spawnTime = 0;

    private bool gotKilled = false; //czy zosta³ zabity przez gracza

    void Start()
    {
        spawnTime = Time.time;

        DifficultyManager.enemySpawnedCount++;

        scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
        score = scoreHolder.GetComponent<Score>();

        body = GetComponent<Rigidbody2D>();

        buffsObject = GameObject.Find("GameCore");
        spawnBuffs = buffsObject.GetComponent<SpawnBuffs>();
    }

    void Update()
    {
        direction = GameObject.Find("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        body.velocity = body.transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            death();
        }
    }

    void death()
    {
        gotKilled = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (gotKilled)
        {
            DifficultyManager.enemyKilledCount++;
            spawnBuffs.spawnBuffs(transform.position);
            score.increaseScore(100);
        }
        DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
    }
}

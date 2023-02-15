using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingEnemy : MonoBehaviour
{
    Score score;

    private GameObject scoreHolder;
    private Vector3 direction;

    public float speed;

    Rigidbody2D body;

    private float spawnTime = 0;

    private bool gotKilled = false; //czy zosta� zabity przez gracza

    void Start()
    {
        spawnTime = Time.time;

        DifficultyManager.enemySpawnedCount++;

        scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
        score = scoreHolder.GetComponent<Score>();

        body = GetComponent<Rigidbody2D>();
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
        moveHommingEnemy(direction);
    }

    void moveHommingEnemy (Vector2 dir)
    {
        body.MovePosition((Vector2)transform.position + (dir * (speed * (OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier)) * Time.deltaTime));
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
            score.increaseScore(100);
        }
        DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
    }
}

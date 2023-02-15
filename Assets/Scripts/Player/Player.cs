using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Life life;
    HealthBar healthBar;
    Bomb bomb;

    private float startX = 2.75f;
	private float startY = -3.0f;
	private float movementVertical, movementHorizontal;

    public static bool attackBoosted = false;
    private float attackBoostDuration = 8.0f;
    private float attackBoostTimer;

    public float speed;
    public float boostedSpeed;
    public float shootCooldown = 2.0f;
    public bool invincible;
    public bool isCooldown;
    public GameObject shield;
    public GameObject lifeHolder;
    public GameObject health;
    public GameObject bombObject;

    Rigidbody2D body;

    void Start()
    {
        transform.position = new Vector2(startX, startY);
        
        body = GetComponent<Rigidbody2D>();
        healthBar = health.GetComponent<HealthBar>();
        life = lifeHolder.GetComponent<Life>();
        bomb = bombObject.GetComponent<Bomb>();
    }

    void Update()
    {
        movementVertical = Input.GetAxis("Vertical");
	    movementHorizontal = Input.GetAxis("Horizontal");

        if (attackBoosted && attackBoostTimer < Time.time)
        {
            attackBoosted = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && Bomb.bombCount > 0)
        {
            bomb.bombTextDisplay(-1);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            for (int i = 0; i < enemyBullets.Length; i++)
            {
                Destroy(enemyBullets[i]);
            }
        }
    }

	void FixedUpdate() {
        playerMovement();
    }

    void playerMovement() {
        body.velocity = new Vector2(movementHorizontal * speed, movementVertical * speed);
    }

	void OnCollisionEnter2D(Collision2D collision)
    {
        string tagName = collision.gameObject.tag;
        switch (tagName)
        {
            case "Enemy":
                if (!invincible)
                    dmgTaken(100);
                break;
            case "EnemyBullet":
                if (!invincible)
                    dmgTaken(30);
                break;
            case "SniperBullet":
                if (!invincible)
                    dmgTaken(51);
                break;
            case "LifeBuff":
                life.lifeChangeValue(1);
                break;
            case "AttackBuff":
                attackBoosted = true;
                attackBoostTimer = Time.time + attackBoostDuration;
                break;
            case "Bomb":
                bomb.bombTextDisplay(1);
                break;
        }
    }

    public void dmgTaken(int dmg)
    {
        healthBar.damageTaken(dmg);
    }
    public void godmode()
    {
        invincible = !invincible;
    }
}

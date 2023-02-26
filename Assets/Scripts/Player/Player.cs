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

        if(Input.GetKeyDown(KeyCode.Space) && Bomb.bombCount > 0 && !Pause.isPaused)
        {
            bomb.bombTextDisplay(-1);
            bomb.activateBomb();
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
            case "Boss":
                if (!invincible)
                    healthBar.damageTaken(100);
                break;
            case "EnemyBullet":
                if (!invincible)
                    healthBar.damageTaken(20);
                break;
            case "BossBullet":
                if (!invincible)
                    healthBar.damageTaken(25);
                break;
            case "SniperBullet":
                if (!invincible)
                    healthBar.damageTaken(60);
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

    public void godmode()
    {
        invincible = !invincible;
    }
}

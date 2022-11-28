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
    private float shootTimer;
    private float startSpeed;

    private bool speedBoosted = false;
    private float speedBoostDuration = 15.0f;
    private float speedBoostTimer;

    public static bool attackBoosted = false;
    private float attackBoostDuration = 20.0f;
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

    //private Transform playerShootTransform;
    
    Rigidbody2D body;

    void Start()
    {
        transform.position = new Vector2(startX, startY);
        startSpeed = speed;
        boostedSpeed = speed * 2.0f;
        
        body = GetComponent<Rigidbody2D>();
        healthBar = health.GetComponent<HealthBar>();
        life = lifeHolder.GetComponent<Life>();
        bomb = bombObject.GetComponent<Bomb>();

        //playerShootTransform = transform.Find("PlayerGun");

        //Instantiate(shield, new Vector2(startX, startY - 0.5f), Quaternion.identity);
    }

    void Update()
    {
        movementVertical = Input.GetAxis("Vertical");// * speed * Time.deltaTime;
	    movementHorizontal = Input.GetAxis("Horizontal");// * speed * Time.deltaTime;

        if (speedBoosted && speedBoostTimer < Time.time)
        {
            speed = startSpeed;
            speedBoosted = false;
        }

        if (attackBoosted && attackBoostTimer < Time.time)
            attackBoosted = false;

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
        //playerMovement();
    }

    void playerMovement() {
        body.velocity = new Vector2(movementHorizontal * speed, movementVertical * speed);
        //transform.Translate(movementHorizontal, movementVertical, 0);
        //rb2D.AddForce(transform.up * thrust);
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
                    dmgTaken(20);
                break;
            case "LifeBuff":
                Debug.Log("Life collected");
                life.lifeChangeValue(1);
                break;
            case "SpeedBuff":
                speedBoosted = true;
                speedBoostTimer = Time.time + speedBoostDuration;
                speed = boostedSpeed;
                break;
            case "AttackBuff":
                attackBoosted = true;
                attackBoostTimer = Time.time + attackBoostDuration;
                break;
            case "Bomb":
                bomb.bombTextDisplay(1);
                break;
        }
        /*if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (!invincible)
            {
                dmgTaken(20);
            }
        }

        if (collision.gameObject.tag == "LifeBuff")
        {
            Debug.Log("Life collected");
            life.lifeChangeValue(1);
        }

        if(collision.gameObject.tag == "SpeedBuff")
        {

            speedBoosted = true;
            speedBoostTimer = Time.time + speedBoostDuration;
        }

        if*/
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

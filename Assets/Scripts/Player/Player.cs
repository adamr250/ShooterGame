using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Life life;
    HealthBar healthBar;

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

    public float speed = 10.0f;
    public float boostedSpeed;
    public float shootCooldown = 2.0f;
    public bool isShooting;
    public bool invincible;
    public bool isCooldown;
    public GameObject shield;
    public GameObject lifeHolder;
    public GameObject health;

    //private Transform playerShootTransform;
    
    Rigidbody2D body;

    void Start()
    {
        transform.position = new Vector2(startX, startY);
        isShooting = false;
        startSpeed = speed;
        boostedSpeed = speed * 2.0f;
        
        body = GetComponent<Rigidbody2D>();
        healthBar = health.GetComponent<HealthBar>();
        
        life = lifeHolder.GetComponent<Life>();
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
    }

	void FixedUpdate() {
        playerMovement();
        if (body.velocity.magnitude > 0 && GameObject.Find("Shield(Clone)") != null)
        {
            Destroy(GameObject.Find("Shield(Clone)"));
            isShooting = true;
        }
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
        healthBar.damageTaken(20);
    }
    public void godmode()
    {
        invincible = !invincible;
    }
}

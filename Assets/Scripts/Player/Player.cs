using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Life life;
    HealthBar healthBar;

    private float startX = 2.75f;
	private float startY = -3.0f;
	private float movementVertical, movementHorizontal;
    private float shootTimer;
    private float startSpeed;

    public float speed = 10.0f;
    public float shootCooldown = 2.0f;
    public bool isShooting;
    public bool invincible;
    public bool isCooldown;
    public GameObject shield;
    //public GameObject lifeHolder;
    public GameObject health;

    //private Transform playerShootTransform;
    
    Rigidbody2D body;

    void Start()
    {
        transform.position = new Vector2(startX, startY);
        isShooting = false;
        startSpeed = speed;
        
        body = GetComponent<Rigidbody2D>();
        healthBar = health.GetComponent<HealthBar>();
        //life = lifeHolder.GetComponent<Life>();
        //playerShootTransform = transform.Find("PlayerGun");

        //Instantiate(shield, new Vector2(startX, startY - 0.5f), Quaternion.identity);
    }

    void Update()
    {
        movementVertical = Input.GetAxis("Vertical");// * speed * Time.deltaTime;
	   
	    movementHorizontal = Input.GetAxis("Horizontal");// * speed * Time.deltaTime;

        //if (isShooting /*&& Input.GetKeyDown(KeyCode.Space)*/)
        //{
        //   if (Time.time > shootTimer /*|| !isCooldown*/ && shootCooldown>0)
        //    {
        //        Shooting();
        //        shootTimer = Time.time + shootCooldown; 
        //    }
        //}
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
        if (!invincible)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
            {
                healthBar.damageTaken(20);
                //life.lifeChangeValue(-1);
                //SceneManager.LoadScene(0);
                //SceneManager.UnloadSceneAsync(1);
            }
            /*if (collision.gameObject.tag == "Enemy")
            {
                transform.position = new Vector2(startX, startY);
            }

            if (collision.gameObject.tag == "EnemyBullet")
            {
                Instantiate(shield, new Vector2(startX, startY + 0.5f), Quaternion.identity);
                transform.position = new Vector2(startX, startY);
                speed = 0;
                isShooting = false;
                Invoke("stopFreeze", 1.0f);
            }*/
        }
    }

    void stopFreeze()
    {
        speed = startSpeed;
    }
}

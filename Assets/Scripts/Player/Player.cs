using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float startX;
	private float startY;
	private float movementVertical, movementHorizontal;
    private float shootTimer;
	
    public float speed = 10.0f;
    public float shootCooldown = 2.0f;
    public bool isShooting;
    public bool invincible;
    public bool isCooldown;

    Transform playerShootTransform;
    Rigidbody2D body;

    public event EventHandler<OnShootEventArgs> OnShoot;
	public class OnShootEventArgs : EventArgs {
		public Vector2 shootPosition;
		public int direction = 1; //up
	}

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        startX = transform.position.x;
        startY = transform.position.y;
        playerShootTransform = transform.Find("PlayerGun");
    }

    void Update()
    {
        movementVertical = Input.GetAxis("Vertical");// * speed * Time.deltaTime;
	   
	    movementHorizontal = Input.GetAxis("Horizontal");// * speed * Time.deltaTime;

        if (isShooting && Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time > shootTimer || !isCooldown)
            {
                Shooting();
                shootTimer = Time.time + shootCooldown; 
            }
        }
    }
	void FixedUpdate() {
        playerMovement();
    }
    void playerMovement() {
        body.velocity = new Vector2(movementHorizontal * speed, movementVertical * speed);
        //transform.Translate(movementHorizontal, movementVertical, 0);
        //rb2D.AddForce(transform.up * thrust);
    }
	void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Found obstacle!");
			Debug.Log(transform.position.y);
            //wallCollision = true;
            //wallX = transform.position.x;
            //wallY = transform.position.y;
			//GameObject.FindGameObjectWithTag("Your_Tag_Here").transform.position;
			//transform.position = new Vector2 (transform.position, transform.position.y);
        }

        if (!invincible)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                transform.position = new Vector2(startX, startY);
            }

            if (collision.gameObject.tag == "Bullet")
            {
                transform.position = new Vector2(startX, startY);
            }
        }
    }

    void Shooting() {
        if(OnShoot != null) {
            OnShoot(this, new OnShootEventArgs { shootPosition = playerShootTransform.position });
        }
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 2.0f;
	private int direction = -1;
	private Transform enemyShootTransform;
	private float shootTimer = 0.0f;
	public bool isShooting;
 	public float shootCooldown = 2.0f;
	public int enemyHealth = 5;

	SpriteRenderer enemySpriteRenderer;
	//							red,			orange,					yellow,			green,		blue
	Color[] enemyColor = { Color.red, new Color(1.0f, 0.5f, 0.0f), Color.yellow, Color.green, Color.blue };

	public event EventHandler<OnShootEventArgs> OnShoot;
	public class OnShootEventArgs : EventArgs {
		public Vector2 shootPosition;
		public int direction = -1; //down
	}

    void Start()
    {
		enemySpriteRenderer.color = enemyColor[enemyHealth - 1];
		enemySpriteRenderer = GetComponent<SpriteRenderer>();
		enemyShootTransform = transform.Find("EnemyGun");
    }


    void Update()
    {
		LeftRightMovement();
		if (Time.time > shootTimer ) {
			shootTimer += shootCooldown;
			if(isShooting)
				Shooting();
		}

		if (enemyHealth < 1)
		{
			Destroy(gameObject);
			Debug.Log("Enemy Dead");
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			if (enemyHealth > 0)
			{
				enemySpriteRenderer.color = enemyColor[enemyHealth-1];
				enemyHealth--;
			}
		}
	}


	void LeftRightMovement()
	{
		float maxWidth = 5;
		float minWidth = -5;

		if(transform.position.x > maxWidth){
			direction = -1;
		}
		if(transform.position.x < minWidth){
			direction = 1;
		}
		float movementHorizontal = direction * speed;
		movementHorizontal *= Time.deltaTime;
		transform.Translate(movementHorizontal, 0, 0);
		
		//transform.position = new Vector2 (5-Mathf.PingPong (Time.time * speed, 10), transform.position.y);
	}

	void Shooting() {
        if(OnShoot != null) {
            OnShoot(this, new OnShootEventArgs { shootPosition = enemyShootTransform.position });
        }
    }
	
}

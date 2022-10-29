using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private float maxWidth;
	private float minWidth;
	private int direction = -1;
	private float shootTimer = 0.0f;
	private float moveRange = 1.5f;
	public int enemyHealth = 5;

	public float speed = 2.0f;
 	public float shootCooldown = 2.0f;
	public bool isShooting;

	private Transform enemyShootTransform;
	SpriteRenderer enemySpriteRenderer;
	//							grey,		red,			orange,					yellow,			green,		blue
	Color[] enemyColor = { Color.red, new Color(1.0f, 0.5f, 0.0f), Color.yellow, Color.green, Color.blue };

	public event EventHandler<OnShootEventArgs> OnShoot;
	public class OnShootEventArgs : EventArgs
	{
		public Vector2 shootPosition;
		public int direction = -1; //up
	}

	void Start()
    {
		enemyHealth--;

		maxWidth = transform.position.x + moveRange;
		minWidth = transform.position.x - moveRange;

		enemySpriteRenderer = GetComponent<SpriteRenderer>();
		enemySpriteRenderer.color = enemyColor[0];
		enemyShootTransform = transform.Find("EnemyGun");
	}


	void Update()
    {
		LeftRightMovement();
		if (Time.time > shootTimer ) {
			shootTimer = Time.time + shootCooldown;// + UnityEngine.Random.Range(0f, 1.0f);
			if (isShooting)
			{
				Shooting();
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "PlayerBullet")
		{
			damageTaken();
		}
	}

	void damageTaken()
    {
		enemyHealth--;

		if (enemyHealth < 0)
		{
			Destroy(gameObject);
			Debug.Log("Enemy Dead");
		}

		if (enemyHealth >= 0)
		{
			enemySpriteRenderer.color = enemyColor[enemyHealth];
			Debug.Log("Enemy Hit: " + enemyHealth);
		}
	}

	void LeftRightMovement()
	{
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

    void Shooting()
    {
		if (OnShoot != null) 
		{
			OnShoot(this, new OnShootEventArgs { shootPosition = enemyShootTransform.position });

		}
	}

}

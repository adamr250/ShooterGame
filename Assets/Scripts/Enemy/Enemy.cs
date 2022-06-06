using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed = 2.0f;
	private int direction = -1;
	private Transform enemyShootTransform;
	public bool isShooting;
	private float nextActionTime = 0.0f;
 	public float period = 2.0f;

	public event EventHandler<OnShootEventArgs> OnShoot;
	public class OnShootEventArgs : EventArgs {
		public Vector2 shootPosition;
		public int direction = -1; //down
	}

    void Start()
    {
		enemyShootTransform = transform.Find("EnemyGun");
    }


    void Update()
    {
		LeftRightMovement();
		if (Time.time > nextActionTime ) {
			nextActionTime += period;
			if(isShooting)
				Shooting();
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

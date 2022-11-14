using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
	Score score;

	private float shootTimer = 0.0f;
	private Transform child;
	private Vector3 enemyGun;
	private GameObject scoreHolder;


	public float shootCooldown = 2.0f;
	public GameObject bulletPref;


	void Start()
	{
		scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
		score = scoreHolder.GetComponent<Score>();
	}


	void Update()
	{
		if (Time.time > shootTimer)
		{
			shootTimer = Time.time + shootCooldown;
			Shooting();
		}
	}

    private void FixedUpdate()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
		{
			Destroy(gameObject);
			score.increaseScore(50);
		}
	}

	void Shooting()
	{
		child = this.gameObject.transform.GetChild(0);
		enemyGun = child.transform.position;
		Instantiate(bulletPref, enemyGun, Quaternion.Euler(0.0f, 0.0f, 180));
	}
}

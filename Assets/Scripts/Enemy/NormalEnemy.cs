using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
	Score score;
	SpawnBuffs spawnBuffs;

	private float shootTimer;
	private Transform child;
	private Vector3 enemyGun;
	private GameObject scoreHolder;
	private GameObject buffsObject;

	public int health;
	public float shootCooldown;
	public GameObject bulletPref;

	void Start()
	{
		shootTimer = shootCooldown;

		//Debug.Log("time: " + Time.time + ";  shootTimer:" + shootTimer);

		scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
		score = scoreHolder.GetComponent<Score>();

		buffsObject = GameObject.Find("GameCore");
		spawnBuffs = buffsObject.GetComponent<SpawnBuffs>();
	}


	void Update()
	{
		if (Time.time > shootTimer)
		{
			//Debug.Log(Time.time + ";  " + shootTimer);
			shootTimer = Time.time + shootCooldown;
			Shooting();
		}

		if(health <= 0)
        {
			spawnBuffs.spawnBuffs(transform.position);
			Destroy(gameObject);
			score.increaseScore(50);
		}
	}

    /*private void FixedUpdate()
    {
        if(transform.position.y > 4f)
        {
			movement();
        }
    }*/

    void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			spawnBuffs.spawnBuffs(transform.position);
			Destroy(gameObject);
			score.increaseScore(50);
		} 
		else if(collision.gameObject.tag == "PlayerBullet")
        {
			health -= 50;
        }
	}

	void movement()
    {
		float speed = 4f;
		float movementHorizontal = speed * Time.deltaTime;
		transform.Translate(0, -movementHorizontal, 0);
		//transform.Translate(transform.position.x, movementHorizontal, transform.position.z);
	}

	void Shooting()
	{
		child = this.gameObject.transform.GetChild(0);
		enemyGun = child.transform.position;
		Instantiate(bulletPref, enemyGun, Quaternion.Euler(0.0f, 0.0f, 180));
	}
}

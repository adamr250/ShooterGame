using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
	Score score;
	SpawnBuffs spawnBuffs;

	[SerializeField] private float speed;

	private float shootTimer;
	private Transform child;
	private Vector3 enemyGun;
	private GameObject scoreHolder;
	private GameObject buffsObject;

	private float rotation;

	public int health;
	public float shootCooldown;
	public GameObject bulletPref;

	private Vector3 target;
	private Vector3 direction;

	private Rigidbody2D body;

	void Start()
	{
		DifficultyManager.enemySpawnedCount++;
		
		if (!gameObject.GetComponent<Rigidbody2D>())
        {
			body = gameObject.AddComponent<Rigidbody2D>();
			body.gravityScale = 0;
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		shootTimer = 3*shootCooldown;

		//Debug.Log("time: " + Time.time + ";  shootTimer:" + shootTimer);

		scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
		score = scoreHolder.GetComponent<Score>();

		buffsObject = GameObject.Find("GameCore");
		spawnBuffs = buffsObject.GetComponent<SpawnBuffs>();


		target = GameObject.Find("Player").transform.position;

		direction = target - transform.position;
		rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		GetComponent<Rigidbody2D>().rotation = rotation + 90;
	}


	void Update()
	{
		if (Time.time > shootTimer)
		{
			//Debug.Log(Time.time + ";  " + shootTimer);
			shootTimer = Time.time + shootCooldown;
			Shooting();
		}
	}

	private void FixedUpdate()
    {
		movement();
    }

	void Shooting()
	{
		child = this.gameObject.transform.GetChild(0);
		enemyGun = child.transform.position;
		Instantiate(bulletPref, enemyGun, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			health = -1;
		} 
		else if(collision.gameObject.tag == "PlayerBullet")
        {
			health -= 51;
		}

		if (health <= 0)
		{
			death();
		}
	}

	void movement()
    {
		body.velocity = -body.transform.up * speed * Time.deltaTime;
	}

	void death()
    {
		DifficultyManager.enemyKilledCount++;

		spawnBuffs.spawnBuffs(transform.position, rotation);
		Destroy(gameObject);
		score.increaseScore(50);
	}
}

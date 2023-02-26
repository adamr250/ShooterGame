using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
	Score score;
	SpawnBuffs spawnBuffs;

	[SerializeField] private float speed; // = 75;

	private float shootTimer;
	private Transform child;
	private Vector3 enemyGun;
	private GameObject scoreHolder;
	private GameObject buffsObject;

	private float rotation;

	[SerializeField] private int health; // = 100;
	[SerializeField] private float shootCooldown; // = 0.59;
	[SerializeField] private GameObject bulletPref;

	private Vector3 target;
	private Vector3 direction;

	private Rigidbody2D body;

	private float spawnTime = 0;

	private bool gotKilled = false;

	void Start()
	{
		spawnTime = Time.time;

		DifficultyManager.enemySpawnedCount++;
		
		if (!gameObject.GetComponent<Rigidbody2D>())
        {
			body = gameObject.AddComponent<Rigidbody2D>();
			body.gravityScale = 0;
			body.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		shootTimer = 3*shootCooldown; //zapobiega strzelani od razu po pojawieniu, kiedy przeciwnik jest jeszcze poza map¹

		scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
		score = scoreHolder.GetComponent<Score>();

		buffsObject = GameObject.Find("GameCore");
		spawnBuffs = buffsObject.GetComponent<SpawnBuffs>();


		target = GameObject.Find("Player").transform.position;

		direction = target - transform.position;
		rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		GetComponent<Rigidbody2D>().rotation = rotation + 90;

		Destroy(gameObject, 10);
	}


	void Update()
	{
		if (Time.time > shootTimer)
		{
			shootTimer = Time.time + shootCooldown/(OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier);
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
			health = 0;
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
		gotKilled = true;
		Destroy(gameObject);
	}

    private void OnDestroy()
    {
		if (gotKilled)
		{
			DifficultyManager.enemyKilledCount++;
			spawnBuffs.spawnBuffs(transform.position);
			score.increaseScore(50);
		}
		DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    Score score;
    SpawnBuffs spawnBuffs;

    private GameObject buffsObject;
    private GameObject scoreHolder;
    private float shootTimer = 0.0f;
    private Transform child;
    private Vector3 target;
    private Vector3 direction;
    private float rotation;

    private int shootCounter = 0;

    [SerializeField] private float shootCooldown = 1.4f;
    [SerializeField] GameObject bulletPref;
    Rigidbody2D body;

    [SerializeField] private float defaultDistanceRay = 15;
    LineRenderer lineRenderer;

    [SerializeField] private LayerMask mask;
    Transform trans;

    private float spawnTime = 0;

    private bool isAiming = false;
    private bool isReloading = false;
    private bool isShooting = false;

    private bool gotKilled = false; //czy zostal zabity przez gracza

    void Start()
    {
        spawnTime = Time.time;

        DifficultyManager.enemySpawnedCount++;

        scoreHolder = GameObject.FindGameObjectWithTag("ScoreVal");
        score = scoreHolder.GetComponent<Score>();

        child = gameObject.transform.GetChild(0);
        body = GetComponent<Rigidbody2D>();

        trans = GetComponent<Transform>();

        gameObject.GetComponent<LineRenderer>().enabled = isAiming;

        lineRenderer = gameObject.GetComponent<LineRenderer>();

        shootTimer = Time.time + shootCooldown;
        isReloading = true;

        buffsObject = GameObject.Find("GameCore");
        spawnBuffs = buffsObject.GetComponent<SpawnBuffs>();
    }


    void Update()
    {
        if (isShooting)
        {
            target = GameObject.Find("Player").transform.position;

            direction = target - transform.position;
            rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            body.rotation = rotation;
            direction.Normalize();

            
        }

        if (Time.time > shootTimer)
        {
            shootTimer = Time.time + shootCooldown / (OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier);
            if (isAiming)
            {
                isAiming = false;
                isShooting = true;
                isReloading = false;
                lineRenderer.enabled = true;
            } else if(isShooting)
            {
                isAiming = false;
                isShooting = false;
                isReloading = true;
                lineRenderer.enabled = true;
            } else if(isReloading)
            {
                Instantiate(bulletPref, lineRenderer.GetPosition(lineRenderer.positionCount - 1), Quaternion.Euler(0.0f, 0.0f, rotation - 90));
                shootCounter++;

                isAiming = true;
                isShooting = false;
                isReloading = false;
                lineRenderer.enabled = false;
            }
        }
        aimLaser();

        if(shootCounter > 5)
        {
            DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
            Destroy(gameObject);
        }
    }

    void aimLaser()
    {
        if(Physics2D.Raycast(trans.position, transform.right, defaultDistanceRay, mask))
        {
            RaycastHit2D hit = Physics2D.Raycast(child.transform.position, transform.right, Mathf.Infinity, mask);
            draw2DRay(child.transform.position, hit.point);
        }
        else
        {
            draw2DRay(child.transform.position, child.transform.transform.right * defaultDistanceRay);
        }
    }
    void draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            death();
        }
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
            score.increaseScore(250);
            spawnBuffs.spawnBuffs(transform.position);
        }
        DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
    }
}

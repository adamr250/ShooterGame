using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour
{
    Score score;

    private GameObject scoreHolder;
    private float shootTimer = 0.0f;
    private Transform child;
    private Vector3 enemyGun;
    private Vector3 target;
    private Vector3 direction;
    private float rotation;

    private int shootCounter = 0;

    public float shootCooldown = 2.0f;
    public GameObject bulletPref;
    public float speed = 3.0f;
    Rigidbody2D body;

    [SerializeField] private float defaultDistanceRay = 15;
    LineRenderer lineRenderer;
    //public Transform firePoint;
    Transform trans;

    private float spawnTime = 0;

    private bool isAiming = false;
    private bool isReloading = false;
    private bool isShooting = false;
    private bool isFreshlySpawned = true;

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
    }


    void Update()
    {
        enemyGun = child.transform.position;

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
            shootTimer = Time.time + shootCooldown;
            if (isAiming)
            {
                //Debug.Log("isAiming");
                isAiming = false;
                isShooting = true;
                isReloading = false;
                lineRenderer.enabled = true;
            } else if(isShooting)
            {
                //Debug.Log("isShooting");
                isAiming = false;
                isShooting = false;
                isReloading = true;
                lineRenderer.enabled = true;
            } else if(isReloading)
            {
                if (!isFreshlySpawned)
                {
                    Instantiate(bulletPref, lineRenderer.GetPosition(lineRenderer.positionCount - 1), Quaternion.Euler(0.0f, 0.0f, rotation - 90));
                    isFreshlySpawned = false;
                    shootCounter++;
                }

                //Debug.Log("isRealoading");
                isAiming = true;
                isShooting = false;
                isReloading = false;
                lineRenderer.enabled = false;
            }
        }
        aimLaser();

        if(shootCounter > 4)
        {
            DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;
            Destroy(gameObject);
        }
    }

    void aimLaser()
    {
        if(Physics2D.Raycast(trans.position, transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(child.transform.position, transform.right);
            draw2DRay(child.transform.position, hit.point);
            //draw2DRay(child.transform.position, child.transform.transform.right * defaultDistanceRay);
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

    /*private void FixedUpdate()
    {
        float distance = Vector2.Distance(target, transform.position);
        if(distance<3)
        {
            Debug.Log(distance);
            movement(direction);
        }
    }

    void movement(Vector2 dir)
    {
        body.MovePosition((Vector2)transform.position - (dir * speed * Time.deltaTime));
        body.velocity = body.transform.right * speed * Time.deltaTime;
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            DifficultyManager.enemyKilledCount++;
            DifficultyManager.enemyTotalLifetime += Time.time - spawnTime;

            Destroy(gameObject);
            score.increaseScore(250);
        }
    }
}

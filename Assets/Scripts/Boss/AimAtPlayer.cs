using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private float shootCooldown;
    private float defaultShootCooldown;
    private float shootTimer = 0.0f;

    private Vector3 target;
    private Vector3 direction;
    private Vector3 weaponPoint;

    private void Start()
    {
        defaultShootCooldown = shootCooldown;

        if(!gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void Update()
    {
        shootCooldown = defaultShootCooldown / (1 + (float)BossBodyManager.bodyPartsDestroyedCounter / 3.5f);
    }
    void FixedUpdate()
    {
        target = GameObject.Find("Player").transform.position;

        direction = target - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
        direction.Normalize();

        weaponPoint = gameObject.transform.GetChild(0).transform.position;
        shooting(rotation);
        
    }

    private void shooting(float rotation)
    {
        if (Time.time > shootTimer)
        {
            shootTimer = Time.time + shootCooldown;
            if (BossHealthBar.isInvulnerable || !BossBodyManager.bossBodyCompleted)
                return;
            Instantiate(bulletPref, weaponPoint, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
        }
    }
}

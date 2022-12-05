using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    [SerializeField] float shootCooldown;
    private float shootTimer;

    private Vector3 target;
    private Vector3 direction;
    private Vector3 weaponPoint;

    private void Start()
    {
        if(!gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
    void FixedUpdate()
    {
        target = GameObject.Find("Player").transform.position;

        direction = target - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gameObject.GetComponent<Rigidbody2D>().rotation = rotation;
        direction.Normalize();

        weaponPoint = gameObject.transform.GetChild(0).transform.position;

        if (Time.time > shootTimer)
        {
            shootTimer = Time.time + shootCooldown;
            Instantiate(bulletPref, weaponPoint, Quaternion.Euler(0.0f, 0.0f, rotation-90));
        }
    }
}

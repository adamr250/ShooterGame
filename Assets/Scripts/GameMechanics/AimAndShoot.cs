using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 playerGun;
    //private float shootTimer1 = 0.0f;
    private float shootTimer2 = 0.0f;

    //public float shootCooldown = 0.5f;
    public float shotgunCooldown = 5.0f;
    public GameObject bulletPref;

    void Start()
    {
        //Cursor.visible = false;
    }

    void Update()
    {
        if (!Pause.Paused)
        {
            mouse = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

            Vector3 direction = mouse - GameObject.Find("PlayerSprite").transform.position;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject.Find("PlayerSprite").transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation-90);

            //if (Time.time > shootTimer1)
            //{
            if (Input.GetMouseButtonDown(0))
            {
                //shootTimer1 = Time.time + shootCooldown;

                playerGun = GameObject.Find("PlayerGun").transform.position;
                Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
                if(Player.attackBoosted)
                {
                    Debug.Log("Boosted Attack");
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 85.0f));
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 95.0f));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 87.5f));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 92.5f));
                }

            };
            //}

            if (Time.time > shootTimer2)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    shootTimer2 = Time.time + shotgunCooldown;

                    playerGun = GameObject.Find("PlayerGun").transform.position;
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 70));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 75));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 80));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 85));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 95));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 100));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 105));
                    Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 110));
                };
            }
        }

    }
}

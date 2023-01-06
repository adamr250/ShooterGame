using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 playerGun;
    private float shootTimer = 0.0f;
    private float shotgunTimer = 0.0f;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    private CursorMode cursorMode = CursorMode.Auto;

    private float shootCooldownDefault = 0.3f;
    private float shootCooldownBoosted = 0.2f;
    private float shootCooldown;
    public float shotgunCooldown = 5.0f;
    public GameObject bulletPref;

    void Start()
    {
        shootCooldown = shootCooldownDefault;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //Cursor.visible = false;
    }

    void Update()
    {
        if (!Pause.Paused)
        {
            mouse = transform.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = mouse - GameObject.Find("PlayerSprite").transform.position;
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GameObject.Find("PlayerSprite").transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation-90);

            //if (Time.time > shootTimer1)
            //{
            if (Input.GetMouseButton(0) && Time.time > shootTimer)
            {

                shootTimer = Time.time + shootCooldown;

                playerGun = GameObject.Find("PlayerGun").transform.position;
                Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 90));
                if(Player.attackBoosted)
                {
                    Debug.Log("Boosted Attack");
                    shootCooldown = shootCooldownBoosted;
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 85.0f));
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 95.0f));
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 87.5f));
                    //Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation - 92.5f));
                } else
                {
                    shootCooldown = shootCooldownDefault;
                }

            };
            //}

            if (Time.time > shotgunTimer)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    shotgunTimer = Time.time + shotgunCooldown;

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

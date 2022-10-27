using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 playerGun;

    public GameObject bulletPref;

    void Start()
    {
        //Cursor.visible = false;
    }

    void Update()
    {
        mouse = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 direction = mouse - GameObject.Find("Player").transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject.Find("Player").transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);

        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("X: " + mouse.x + ";    Y: " + mouse.y);
            playerGun = GameObject.Find("PlayerGun").transform.position;
            Instantiate(bulletPref, playerGun, Quaternion.Euler(0.0f, 0.0f, rotation-90));
            //Debug.Log("X: " + GameObject.Find("Player").transform.position.x + ";    Y: " + GameObject.Find("Player").transform.position.y);

        };

        if (Input.GetMouseButtonDown(1))
        {
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

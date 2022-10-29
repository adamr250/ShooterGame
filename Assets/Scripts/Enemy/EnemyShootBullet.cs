using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBullet : MonoBehaviour
{
    [SerializeField] private Transform prefabBullet;

    private void Awake() {
        Enemy enemy = GetComponent<Enemy>();
        enemy.OnShoot += OnShoot;
    }

    private void OnShoot(object sender, Enemy.OnShootEventArgs e) {
    //    playerGun = GameObject.Find("PlayerGun").transform.position;
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0.0f, 0.0f, 160));
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0.0f, 0.0f, 170));
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0.0f, 0.0f, 180));
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0.0f, 0.0f, 190));
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0.0f, 0.0f, 200));		
        //Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0,0,180));
    }
}

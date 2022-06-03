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
        Instantiate(prefabBullet, e.shootPosition, Quaternion.Euler(0,0,180));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBullet : MonoBehaviour
{
    [SerializeField] private Transform prefabBullet;

    private void Awake() {
        Player player = GetComponent<Player>();
        player.OnShoot += OnShoot;
    }

    private void OnShoot(object sender, Player.OnShootEventArgs e) {
        Instantiate(prefabBullet, e.shootPosition, Quaternion.identity);
    }
}


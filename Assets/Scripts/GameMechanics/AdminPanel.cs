using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    public GameObject hommingEnemy;
    public GameObject sniperEnemy;
    private Vector3 spawnPoint1 = new Vector3(10f, 3.0f, 0.0f);
    //private Vector3 spawnPoint1 = new Vector3(2.0f, 5.5f, 0.0f);
    private Vector3 spawnPoint2 = new Vector3(2.5f, 4.5f, 0.0f);


    public void spawnHommingEnemy()
    { 
        Instantiate(hommingEnemy, spawnPoint1, Quaternion.identity);
    }

    public void bulletHellTime()
    {
        //BulletHellTime.bulletHellTime();
    }

    public void spawnSniper()
    {
        Instantiate(sniperEnemy, spawnPoint2, Quaternion.identity);
    }

    public void killAll ()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i=0; i< enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
    }
    
}

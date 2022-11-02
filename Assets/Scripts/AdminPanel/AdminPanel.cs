using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    public GameObject hommingEnemy;
    private Vector3 spawnPoint1 = new Vector3(10f, 3.0f, 0.0f);

    public static bool Paused = false;


    public void spawnHommingEnemy()
    {
        Instantiate(hommingEnemy, spawnPoint1, Quaternion.identity);
    }

    public void bulletHellTime()
    {
        //BulletHellTime.bulletHellTime();
    }
    public void pauseGame()
    {

        if(Paused)
        {
            Time.timeScale = 1.0f;
            Paused = false;
            Debug.Log("Unpaused");

        } else if(!Paused)
        {
            Time.timeScale = 0f;
            Paused = true;
            Debug.Log("Paused");
        }
    }
}

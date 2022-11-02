using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminPanel : MonoBehaviour
{
    public GameObject hommingEnemy;
    private Vector3 spawnPoint1 = new Vector3(10f, 3.0f, 0.0f);
    public void spawnHommingEnemy()
    {
        Instantiate(hommingEnemy, spawnPoint1, Quaternion.identity);
    }
}

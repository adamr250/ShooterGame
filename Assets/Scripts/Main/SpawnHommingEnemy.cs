using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHommingEnemy : MonoBehaviour
{
	public GameObject hommingEnemy;
	private Vector3 spawnPoint1 = new Vector3(10f, 3.0f, 0.0f);

    void Start()
    {
        Instantiate(hommingEnemy, spawnPoint1, Quaternion.identity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWater : MonoBehaviour
{
    public GameObject waterPrefab;
    private Vector3 spawnPoint1 = new Vector3(0f, 0f, 1.0f);
    private Vector3 spawnPoint2 = new Vector3(0f, 10f, 1.0f);

    void Start()
    {
        Instantiate(waterPrefab, spawnPoint1, Quaternion.identity);
        Instantiate(waterPrefab, spawnPoint2, Quaternion.identity);
    }
}

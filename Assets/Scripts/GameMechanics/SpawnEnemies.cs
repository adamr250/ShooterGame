using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public bool spawnHommingOff = false;
    public bool spawnSniperOff = false;

    public GameObject hommingEnemy;
    public float hommingSpawnCd = 5.0f;
    private float hommingSpawnTimer = 0.0f;
    private Vector3 spawnPointHomming;// = new Vector3(10f, 3.0f, 0.0f);

    public GameObject sniperEnemy;
    public float sniperSpawnCd = 5.0f;
    private float sniperSpawnTimer = 0.0f;
    private Vector3 spawnPointSniper;// = new Vector3(10f, 3.0f, 0.0f);

    void Update()
    {
        spawnHomming();

        spawnSniper();

        //bulletHellTime();
        hommingSpawnCd = 5.0f/(1+(Score.scoreNum/10000));

    }
    public void spawnHomming()
    {
        if (!spawnHommingOff)
        {
            if (Time.time > hommingSpawnTimer)
            {
                hommingSpawnTimer = Time.time + hommingSpawnCd;
                float axis = Random.Range(0f, 1.0f);
                int plusminus = Random.Range(0, 2) * 2 - 1;
                Debug.Log("cd: " + hommingSpawnCd);
                if (axis <= 0.5f)
                {
                    spawnPointHomming = new Vector3(Random.Range(-3.4f, 9.5f), plusminus * 6.0f, 0.0f);
                }
                else
                {
                    spawnPointHomming = new Vector3((plusminus * 6.55f) + 2.95f, Random.Range(-5.5f, 5.5f), 0.0f);

                }

                Instantiate(hommingEnemy, spawnPointHomming, Quaternion.identity);
            }
        }
    }
    public void spawnSniper()
    {
        if (!spawnSniperOff)
        {
            if (Time.time > sniperSpawnTimer)
            {
                sniperSpawnTimer = Time.time + sniperSpawnCd;
                spawnPointSniper = new Vector3(Random.Range(-2f, 8f), Random.Range(-1.0f, 1.0f) + 4.0f, 0.0f);

                Instantiate(sniperEnemy, spawnPointSniper, Quaternion.identity);
            }
        }
    }
    public void bulletHellTime()
    {

    }
}

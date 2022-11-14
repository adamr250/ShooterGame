using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public bool spawnHommingOff = false;
    public bool spawnSniperOff = false;
    public bool spawnNormalOff = false;

    public GameObject hommingEnemy;
    public float hommingSpawnCd = 7.0f;
    private float hommingSpawnTimer;
    private Vector3 spawnPointHomming;// = new Vector3(10f, 3.0f, 0.0f);

    public GameObject sniperEnemy;
    public float sniperSpawnCd = 10.0f;
    private float sniperSpawnTimer;
    private Vector3 spawnPointSniper;// = new Vector3(10f, 3.0f, 0.0f);

    public GameObject normalEnemy;
    public float normalSpawnCd = 4.5f;
    private float normalSpawnTimer;
    private Vector3 spawnPointNormal;

    private void Start()
    {
        hommingSpawnTimer = hommingSpawnCd;
        sniperSpawnTimer = sniperSpawnCd;
        normalSpawnTimer = normalSpawnCd;
    }
    void Update()
    {
        spawnHomming();

        if(Score.scoreNum > 1000)
            spawnSniper();

        spawnNormal();

        //bulletHellTime();

        hommingSpawnCd = 5.0f/(1+((float)Score.scoreNum/10000));

    }
    public void spawnHomming()
    {
        if (!spawnHommingOff)
        {
            if (Time.time > hommingSpawnTimer)
            {
                hommingSpawnTimer = Time.time + hommingSpawnCd;
                float axis = Random.Range(0f, 1.0f);
                int plusOrMinus = Random.Range(0, 2) * 2 - 1;
                if (axis <= 0.5f)
                {
                    spawnPointHomming = new Vector3(Random.Range(-3.4f, 9.5f), plusOrMinus * 6.0f, 0.0f);
                }
                else
                {
                    spawnPointHomming = new Vector3((plusOrMinus * 6.55f) + 2.95f, Random.Range(-5.5f, 5.5f), 0.0f);

                }

                Instantiate(hommingEnemy, spawnPointHomming, Quaternion.identity);
            }
        }
    }

    public void spawnNormal()
    {
        if (!spawnNormalOff)
        {
            if (Time.time > normalSpawnTimer)
            {
                normalSpawnTimer = Time.time + normalSpawnCd;
                spawnPointNormal = new Vector3(Random.Range(-2f, 8f), 6.0f, 0f);

                Instantiate(normalEnemy, spawnPointNormal, Quaternion.identity);
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
                spawnPointSniper = new Vector3(Random.Range(-2f, 8f), Random.Range(-0.5f, 0.5f) + 4.25f, 0.0f);

                Instantiate(sniperEnemy, spawnPointSniper, Quaternion.identity);
            }
        }
    }
    public void bulletHellTime()
    {

    }
}

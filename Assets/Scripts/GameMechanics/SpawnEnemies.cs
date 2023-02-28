using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private bool spawnNormalOff = false;
    [SerializeField] private bool spawnHommingOff = false;
    [SerializeField] private bool spawnSniperOff = false;

    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private float normalSpawnCd = 4.5f;
    private float normalSpawnTimer;
    private Vector3 spawnPointNormal;

    [SerializeField] private GameObject hommingEnemy;
    [SerializeField] private float hommingSpawnCd = 7.0f;
    private float hommingSpawnTimer;
    private Vector3 spawnPointHomming;

    [SerializeField] private GameObject sniperEnemy;
    [SerializeField] private float sniperSpawnCd = 7.3f;
    private float sniperSpawnTimer;
    private Vector3 spawnPointSniper;

    [SerializeField] GameObject boss;

    [SerializeField] private Collider2D[] colliders;
    [SerializeField] private float radius = 20;
    [SerializeField] LayerMask mask;

    [SerializeField] private int scoreToSpawnNormal;
    [SerializeField] private int scoreToSpawnHomming;
    [SerializeField] private int scoreToSpawnSniper;
    [SerializeField] private int scoreToSpawnBoss;
    private int increaseScoreToSpawnBoss;

    private bool bossIsSpawned = false;

    private void Start()
    {
        hommingSpawnTimer = hommingSpawnCd;
        sniperSpawnTimer = sniperSpawnCd;
        normalSpawnTimer = normalSpawnCd;

        increaseScoreToSpawnBoss = scoreToSpawnBoss/2;
    }

    void Update()
    {
        if (Score.scoreNum < scoreToSpawnBoss)
        {
            if (!spawnNormalOff && Time.time > normalSpawnTimer && Score.scoreNum >= scoreToSpawnNormal)
            {
                spawnNormal();
            }

            if (!spawnHommingOff && Time.time > hommingSpawnTimer && Score.scoreNum >= scoreToSpawnHomming)
            {
                spawnHomming();
            }

            if (!spawnSniperOff && Time.time > sniperSpawnTimer && Score.scoreNum >= scoreToSpawnSniper)
            {
                spawnSniper();
            }
        } else if(!bossIsSpawned)
        {
            bossIsSpawned = true;
            DifficultyManager.scoreThreshold += 1000;
            Instantiate(boss, new Vector3(-4.83f, 4.08f, 0.0f), Quaternion.identity);
        }

        if (normalSpawnCd > 0.8f)
            normalSpawnCd = 2.8f/(1+((float)Score.scoreNum/20000));

    }

    public void spawnNormal()
    {
        bool canSpawnHere = false; 

        int safetyBreak = 0;
        while (true)
        {
            normalSpawnTimer = Time.time + normalSpawnCd / (OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier);

            int axis = Random.Range(0, 2);
            int plusOrMinus = Random.Range(0, 2) * 2 - 1;

            if (axis == 0)
            {   //spawn at axis X
                spawnPointNormal = new Vector3(Random.Range(-2.2f, 8.3f), plusOrMinus * 5.25f, 0.0f);
            }
            else
            {   //spawn at axis Y
                spawnPointNormal = new Vector3((plusOrMinus * 6.25f) + 2.95f, Random.Range(-4.75f, 4.75f), 0.0f);
            }

            canSpawnHere = preventSpawnOverlap(spawnPointNormal);

            if(canSpawnHere)
            {
                Instantiate(normalEnemy, spawnPointNormal, Quaternion.identity);
                break;
            }

            if (safetyBreak++ > 50)
            {
                Debug.Log("Normal Enemy didn't spawn due to too many attempts.");
                break;
            }
        }
    }

    public void spawnHomming()
    {
        hommingSpawnTimer = Time.time + hommingSpawnCd;
        int axis = Random.Range(0, 2);
        int plusOrMinus = Random.Range(0, 2) * 2 - 1;
        if (axis == 0)
        {   //spawn at axis X
            spawnPointHomming = new Vector3(Random.Range(-3.4f, 9.5f), plusOrMinus * 6.0f, 0.0f);
        }
        else
        {   //spawn at axis Y
            spawnPointHomming = new Vector3((plusOrMinus * 6.55f) + 2.95f, Random.Range(-5.5f, 5.5f), 0.0f);
        }

        Instantiate(hommingEnemy, spawnPointHomming, Quaternion.identity);
    }

    public void spawnSniper()
    {
        bool canSpawnHere = false;

        int safetyBreak = 0;
        while (true)
        {
            sniperSpawnTimer = Time.time + sniperSpawnCd;

            int axis = Random.Range(0, 2);
            int plusOrMinus = Random.Range(0, 2) * 2 - 1;

            if (axis == 0)
            {   //spawn at axis X
                spawnPointSniper = new Vector3(Random.Range(-2.2f, 8.3f), plusOrMinus * 4.25f, 0.0f);
            }
            else
            {   //spawn at axis Y
                spawnPointSniper = new Vector3((plusOrMinus * 5.25f) + 2.95f, Random.Range(-4.5f, 4.5f), 0.0f);
            }

            canSpawnHere = preventSpawnOverlap(spawnPointSniper);

            if (canSpawnHere)
            {
                Instantiate(sniperEnemy, spawnPointSniper, Quaternion.identity);
                break;
            }

            if (safetyBreak++ > 50)
            {
                Debug.Log("Sniper Enemy didn't spawn due to too many attempts.");
                break;
            }
        }

    }

    bool preventSpawnOverlap(Vector3 spawnPoint)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius, mask);
        for(int i = 0; i<colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = 2*colliders[i].bounds.extents.x;
            float height = 2*colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float downExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if(spawnPoint.x <= rightExtent && spawnPoint.x >= leftExtent )
            {
                if(spawnPoint.y <= upperExtent && spawnPoint.y >= downExtent)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void increaseBossScoreThreshold()
    {
        bossIsSpawned = false;
        scoreToSpawnBoss += increaseScoreToSpawnBoss;
    }
}

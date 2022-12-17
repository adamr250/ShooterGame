using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public bool spawnNormalOff = false;
    public bool spawnHommingOff = false;
    public bool spawnSniperOff = false;

    public GameObject normalEnemy;
    public float normalSpawnCd = 4.5f;
    private float normalSpawnTimer;
    private Vector3 spawnPointNormal;

    public GameObject hommingEnemy;
    public float hommingSpawnCd = 7.0f;
    private float hommingSpawnTimer;
    private Vector3 spawnPointHomming;// = new Vector3(10f, 3.0f, 0.0f);

    public GameObject sniperEnemy;
    public float sniperSpawnCd = 10.0f;
    private float sniperSpawnTimer;
    private Vector3 spawnPointSniper;// = new Vector3(10f, 3.0f, 0.0f);

    public Collider2D[] colliders;
    public float radius = 20;
    public LayerMask mask;

    private bool canSpawnHere = false;
    private bool canSpawnHere1 = false;
    private bool canSpawnHere2 = false;

    [SerializeField] private GameObject bossManager;
    private bool bossIsSpawned = false;

    private void Start()
    {
        bossManager.GetComponent<BossBodyManager>().enabled = false;
        hommingSpawnTimer = hommingSpawnCd;
        sniperSpawnTimer = sniperSpawnCd;
        normalSpawnTimer = normalSpawnCd;
    }

    void Update()
    {
        if (Score.scoreNum < 50000)
        {
            if (!spawnNormalOff && Time.time > normalSpawnTimer)
            {
                spawnNormal();
            }

            if (!spawnHommingOff && Time.time > hommingSpawnTimer && Score.scoreNum > 800)
            {
                spawnHomming();
            }

            if (!spawnSniperOff && Time.time > sniperSpawnTimer && Score.scoreNum > 5000)
            {
                spawnSniper();
            }
        } else if(!bossIsSpawned)
        {
            bossIsSpawned = true;
            bossManager.GetComponent<BossBodyManager>().enabled = true;
        }

        //bulletHellTime();
        if (hommingSpawnCd > 1.0f)
            hommingSpawnCd = 5.0f/(1+((float)Score.scoreNum/10000));

    }

    public void spawnNormal()
    {
        canSpawnHere = false; //responsible for overlaping
        canSpawnHere1 = false; //responsible for distance from player
        canSpawnHere2 = false; //responsible for distance from map center

        int safetyBreak = 0;
        while (true)
        {
            normalSpawnTimer = Time.time + normalSpawnCd;

            int axis = Random.Range(0, 2);
            int plusOrMinus = Random.Range(0, 2) * 2 - 1;
            //spawnPointNormal = new Vector3(Random.Range(-2f, 8f), Random.Range(3.5f, 4.5f), 0f);
            /*spawnPointNormal = Random.insideUnitCircle;
            Debug.Log("random spawn point: " + spawnPointNormal);
            Debug.Log("corrected spawn point: " + spawnPointNormal*3 + " + " + (spawnPointNormal.normalized));
            spawnPointNormal = spawnPointNormal * 2 + (spawnPointNormal.normalized)*3;
            spawnPointNormal += new Vector3(2.0f, 0, 0);
            spawnPointNormal.x *= 1.5f;*/
            Debug.Log("axis: " + axis);
            if (axis == 0)
            {   //spawn at axis X
                spawnPointNormal = new Vector3(Random.Range(-2.2f, 8.3f), plusOrMinus * 4.25f, 0.0f);
            }
            else
            {   //spawn at axis Y
                spawnPointNormal = new Vector3((plusOrMinus * 5.25f) + 2.95f, Random.Range(-4.75f, 4.75f), 0.0f);
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
        sniperSpawnTimer = Time.time + sniperSpawnCd;
        spawnPointSniper = new Vector3(Random.Range(-2f, 8f), Random.Range(4.0f, 4.75f), 0.0f);

        Instantiate(sniperEnemy, spawnPointSniper, Quaternion.identity);
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
}

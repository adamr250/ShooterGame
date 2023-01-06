using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    public static int enemySpawnedCount = 0;
    public static int enemyKilledCount = 0;

    public static int totalLifesCount = 0;
    public static int deathsCount = 0;

    private int enemySpawnedCountPrevious = 0;
    private int enemyKilledCountPrevious = 0;

    private int totalLifesCountPrevious = 0;
    private int deathsCountPrevious = 0;

    public static float inGameTimeCount = 0;

    private float enemyKilledPrecentage = 0;
    private float enemyKilledPrecentagePrevious = 0;

    private int scoreThreshold = 100;
    //private float dynamicDifficultyMultiplier = 1;


    private void Start()
    {
        totalLifesCount = Life.lifeNum;
    }

    void Update()
    {
        //Debug.Log("time: " + Time.time);
        //if (!(enemyKilledCount > enemySpawnedCount))
        //    Debug.Log("spawned: " + enemySpawnedCount + ";  killed: " + enemyKilledCount + ";  precentage: " + enemyKilledPrecentage);


        if(scoreThreshold < Score.scoreNum)
        {
            scoreThreshold += 100;

            enemyKilledPrecentage = 100 * (float)enemyKilledCount / enemySpawnedCount;
            Debug.Log("lifes: " + totalLifesCount);
            //updateDifficulty();
        }
    }

    public void updateDifficulty()
    {
        if (Score.scoreNum < 2000)
        {
            updatePreviousVariables();
            return;
        }

        if(Life.lifeNum < 3)
        {
            //zwieksz lifeBuff drop rate
        } else if(Life.lifeNum > 6)
        {
            //przeciwnicy szybciej strzelaja i sa szybsi
            //sniper mechnika ruchu
        }
        if(enemyKilledPrecentage > 80)
        {
            //normalEnemy sa szybsi
        }
        if(Time.time > 60)
        {
            //zwiêksz ogolny poziom trudnosci
        }
    }

    private void updatePreviousVariables()
    {
        enemySpawnedCountPrevious = enemySpawnedCount;
        enemyKilledCountPrevious = enemyKilledCount;
        enemyKilledPrecentagePrevious = enemyKilledPrecentage;
        totalLifesCountPrevious = totalLifesCount;
        deathsCountPrevious = deathsCount;


    }
}

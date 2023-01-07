using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    public static int enemySpawnedCount = 0;
    public static int enemyKilledCount = 0;

    public static int totalLifesCount = 0;
    public static int deathsCount = 0;

    public static float enemyTotalLifeTime = 0; 

    private int enemySpawnedCountPrevious = 0;
    private int enemyKilledCountPrevious = 0;

    private int totalLifesCountPrevious = 0;
    private int deathsCountPrevious = 0;

    private float enemyTotalLifeTimePrevious = 0;

    private float enemyKilledTotalPrecentage = 0;
    private float enemyKilledTotalPrecentagePrevious = 0;

    private int scoreThreshold = 1000;

    //private float dynamicDifficultyMultiplier = 1;


    //public float fireRateMultiplier = 1;

    private void Start()
    {
        totalLifesCount = Life.lifeNum;
    }

    void Update()
    {
        Debug.Log("time: " + enemyTotalLifeTime);
        //if (!(enemyKilledCount > enemySpawnedCount))
        //    Debug.Log("spawned: " + enemySpawnedCount + ";  killed: " + enemyKilledCount + ";  precentage: " + enemyKilledPrecentage);


        if(scoreThreshold < Score.scoreNum)
        {
            scoreThreshold += 1000;

            Debug.Log("lifes: " + totalLifesCount);
            //updateDifficulty();
        }
    }

    public void updateDifficulty()
    {
        float evaluationLifes = 0;
        float evaluationKills = 0;

        int lifesGained = totalLifesCount - totalLifesCountPrevious;
        int additionalDeaths = deathsCount - deathsCountPrevious;

        float enemyKilledPrecentageThisRound = 100 * (float)(enemyKilledCount - enemyKilledCountPrevious) / (enemySpawnedCount - enemySpawnedCountPrevious);
        enemyKilledTotalPrecentage = 100 * (float)(enemyKilledCount / enemySpawnedCount);

        if (Score.scoreNum < 2000)
        {
            updatePreviousVariables();
            return;
        }

        evaluationLifes = (float)(lifesGained - additionalDeaths) / (totalLifesCount - deathsCount);

        if (enemyKilledPrecentageThisRound > 50)
            evaluationKills = ((float)(enemyKilledPrecentageThisRound - 50) / 25) - 1;
        else
            evaluationKills = -1;


        /*if (Life.lifeNum > 6)
        {
            //zmnijeszy drop rate, zwieksz poziom trudnosci
        }

        if(additionalDeaths > lifesGained) // je¿eli gracz wiêcej razy umar³ ni¿ dosta³ bonusowe ¿ycie
        {

        }


        if(enemyKilledTotalPrecentage > 90)
        {
            //normalEnemy sa szybsi
            //sniper szybciej znika
        }*/

        if(Time.time > 90)
        {
            //zwiêksz ogolny poziom trudnosci
        }

        updatePreviousVariables();
    }

    private void updatePreviousVariables()
    {
        enemySpawnedCountPrevious = enemySpawnedCount;
        enemyKilledCountPrevious = enemyKilledCount;
        enemyKilledTotalPrecentagePrevious = enemyKilledTotalPrecentage;
        totalLifesCountPrevious = totalLifesCount;
        deathsCountPrevious = deathsCount;

        //enemyKilledTotalPrecentage = 0;
    }
}

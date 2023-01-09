using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    public static int enemySpawnedCount = 0;
    private int enemySpawnedCountPrevious = 0;

    public static int enemyKilledCount = 0;
    private int enemyKilledCountPrevious = 0;


    public static int totalLifesCount = 0;
    private int totalLifesCountPrevious = 0;

    public static int deathsCount = 0;
    private int deathsCountPrevious = 0;

    
    public static float enemyTotalLifetime = 0;
    private float enemyTotalLifetimePrevious = 0;

    private float enemyLifetimeThisRound = 0;
    private float enemyLifetimeThisRoundPrevious = 0;

    private float enemyAverageLifetime = 0;
    private float enemyAverageLifetimePrevious = 0;

    private float enemyKilledTotalPrecentage = 0;
    private float enemyKilledTotalPrecentagePrevious = 0;

    
    private int scoreThreshold = 1000;

    private float dynamicDifficultyMultiplier = 1;


    private void Start()
    {
        totalLifesCount = Life.lifeNum;
        Debug.Log("(InGame) default difficulty: " + OptionsMenu.defaultDifficultyMultiplier);
    }

    void Update()
    {
        //Debug.Log("time: " + enemyTotalLifetime);
        //if (!(enemyKilledCount > enemySpawnedCount))
        //    Debug.Log("spawned: " + enemySpawnedCount + ";  killed: " + enemyKilledCount + ";  precentage: " + enemyKilledPrecentage);


        if(scoreThreshold <= Score.scoreNum)
        {
            scoreThreshold += 1000;

            //Debug.Log("lifes: " + totalLifesCount);
            updateDifficulty();
        }
    }

    public void updateDifficulty()
    {
        if (Score.scoreNum < 2000)
        {
            updatePreviousVariables();
            return;
        }

        //zmienne oceniaj¹ce gre gracza na podstawie, odpowiednio, iloœci œmierci i zyskanych ¿yæ, procent zabitych wrogów oraz œredni czas ¿ycie wrogów
        float evaluationLifes = 0;
        float evaluationKills = 0;
        float evaluationEnemyLifetime = 0;

        int lifesGained = totalLifesCount - totalLifesCountPrevious;
        int additionalDeaths = deathsCount - deathsCountPrevious;

        float enemyKilledPrecentageThisRound = 100 * (float)(enemyKilledCount - enemyKilledCountPrevious) / (enemySpawnedCount - enemySpawnedCountPrevious);
        enemyKilledTotalPrecentage = 100 * (float)(enemyKilledCount / enemySpawnedCount);

        enemyLifetimeThisRound = enemyTotalLifetime - enemyTotalLifetimePrevious;

        enemyAverageLifetime = enemyLifetimeThisRound / (enemySpawnedCount - enemySpawnedCountPrevious);

        //je¿eli wiêcej razy umar³eœ ni¿ dosta³eœ ¿ycia wynik bêdzie ujemny, je¿eli mianownik mia³by wyjœæ ujemny to znaczy ¿e ju¿ przegra³eœ
        evaluationLifes = (float)(lifesGained - additionalDeaths) / (totalLifesCount - deathsCount); 

        //zak³adamy ¿e 50% zabitych wrogów to minimum, wszystko poni¿ej jest uatomatycznie -1
        if (enemyKilledPrecentageThisRound >= 50)
            evaluationKills = ((enemyKilledPrecentageThisRound - 50) / 25) - 1;
        else
            evaluationKills = -1;

        //zak³adamy ¿e max czas ¿ycia wrogów to 10s, wszystko powy¿ej jest automatycznie -1
        if (enemyAverageLifetime <= 7)
            evaluationEnemyLifetime = -((enemyAverageLifetime / 3.5f) - 1);
        else
            evaluationEnemyLifetime = -1;


        float evaluationScore = evaluationLifes + evaluationKills + evaluationEnemyLifetime;

        dynamicDifficultyMultiplier = evaluationScore / 6;

        Debug.Log("evaluations:  lifes - " + evaluationLifes + "; kills - " + evaluationKills + "; lifetime - " + evaluationEnemyLifetime);
        Debug.Log("evaluationScore: " + evaluationScore);
        Debug.Log("dynamic diff multip: " + dynamicDifficultyMultiplier);

        updatePreviousVariables();
    }

    private void updatePreviousVariables()
    {
        enemySpawnedCountPrevious = enemySpawnedCount;
        enemyKilledCountPrevious = enemyKilledCount;
        enemyKilledTotalPrecentagePrevious = enemyKilledTotalPrecentage;

        totalLifesCountPrevious = totalLifesCount;
        deathsCountPrevious = deathsCount;

        enemyTotalLifetimePrevious = enemyTotalLifetime;
        enemyLifetimeThisRoundPrevious = enemyLifetimeThisRound;
        enemyAverageLifetimePrevious = enemyAverageLifetime;
    }

    public float getDynamicDifficulty()
    {
        return dynamicDifficultyMultiplier;
    }
}

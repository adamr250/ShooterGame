using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DifficultyManager : MonoBehaviour
{

    public static int enemySpawnedCount = 0;
    private int enemySpawnedCountPrevious = 0;

    public static int enemyKilledCount = 0;
    private int enemyKilledCountPrevious = 0;

    
    public static float enemyTotalLifetime = 0;
    private float enemyTotalLifetimePrevious = 0;

    private float enemyLifetimeThisRound = 0;
    private float enemyAverageLifetime = 0;
    public static int scoreThreshold = 1000;
    public static float dynamicDifficultyMultiplier = 0;


    private void Start()
    {
        createLogs("\n##############################\nNEW GAME LOGS\n");
    }

    void Update()
    {
        if (scoreThreshold <= Score.scoreNum)
        {
            scoreThreshold += 1000;

            updateDifficulty();
        }
    }

    public void updateDifficulty()
    {
        if (Score.scoreNum < 1500)
        {
            updatePreviousVariables();
            return;
        }

        //zmienne oceniaj¹ce gre gracza na podstawie, odpowiednio, iloœci œmierci i zyskanych ¿yæ, procent zabitych wrogów oraz œredni czas ¿ycie wrogów
        float evaluationLifes = 0;
        float evaluationKills = 0;
        float evaluationEnemyLifetime = 0;


        float enemyKilledPrecentageThisRound = 100 * (float)(enemyKilledCount - enemyKilledCountPrevious) / (enemySpawnedCount - enemySpawnedCountPrevious);

        enemyLifetimeThisRound = enemyTotalLifetime - enemyTotalLifetimePrevious;
        if (enemySpawnedCount != enemySpawnedCountPrevious)
        {
            enemyAverageLifetime = enemyLifetimeThisRound / (enemySpawnedCount - enemySpawnedCountPrevious);
        }


        //zak³adamy ¿e 4 ¿ycia to granica. poni¿ej 4 ¿yæ, wynik ujemny, powy¿ej - dodatni
        int currentLifesCount = Life.lifeNum;
        if (currentLifesCount <= 4)
        {
            evaluationLifes = ((float)currentLifesCount / 3) - (4.0f / 3.0f);
        }
        else if (currentLifesCount <= 7)
        {
            evaluationLifes = Mathf.Tan(((float)currentLifesCount / 2.275f) - 1.758242f) / 4;
        }
        else
        {
            evaluationLifes = 1;
        }

        //zak³adamy ¿e 60% zabitych wrogów to minimum, wszystko poni¿ej jest uatomatycznie -1
        if (enemyKilledPrecentageThisRound >= 60)
            evaluationKills = ((enemyKilledPrecentageThisRound - 60) / 20) - 1;
        else
            evaluationKills = -1;

        //zak³adamy ¿e max czas ¿ycia wrogów to 6s, wszystko powy¿ej jest automatycznie -1
        if (enemyAverageLifetime <= 6)
            evaluationEnemyLifetime = -((enemyAverageLifetime / 3.0f) - 1);
        else
            evaluationEnemyLifetime = -1;


        float evaluationScore = evaluationLifes + evaluationKills + evaluationEnemyLifetime;

        dynamicDifficultyMultiplier = evaluationScore / 6;

        Debug.Log("evaluations:  lifes - " + evaluationLifes + "; kills - " + evaluationKills + "; lifetime - " + evaluationEnemyLifetime);
        //Debug.Log("evaluationScore: " + evaluationScore);
        Debug.Log("dynamic diff multip: " + dynamicDifficultyMultiplier);

        updatePreviousVariables();
        string logContent = "Login Date: " + System.DateTime.Now + "\nScore: " + Score.scoreNum.ToString() +
                            "\n Variables: " + "\n\tcurrentLifesCount:  " + currentLifesCount.ToString("F6") +
                            "\n\tenemyKilledPrecentageThisRound:  " + enemyKilledPrecentageThisRound.ToString("F6") +
                            "\n\tenemyAverageLifetime:  " + enemyAverageLifetime.ToString("F6") +
                            "\n Evaluation Values: " + 
                            "\n\tLifes:  " + evaluationLifes.ToString("F6") + 
                            "\n\tKills Precentage:  " + evaluationKills.ToString("F6") + 
                            "\n\tEnemy Lifetime:  " + evaluationEnemyLifetime + 
                            "\nDynamic Difficulty Multiplier:  " + dynamicDifficultyMultiplier.ToString("F6") + 
                            "\n-----------------------------------------------------------------------------------------\n";
        createLogs(logContent);
    }

    private void updatePreviousVariables()
    {
        enemySpawnedCountPrevious = enemySpawnedCount;
        enemyKilledCountPrevious = enemyKilledCount;


        enemyTotalLifetimePrevious = enemyTotalLifetime;
    }

    void createLogs(string content)
    {
        string path = Application.dataPath + "/Logs_DynamicDiffculty.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Dynamic Diffculty Logs\n");
        }
        File.AppendAllText(path, content);
        
    }

    public float getDynamicDifficulty()
    {
        return dynamicDifficultyMultiplier;
    }
}

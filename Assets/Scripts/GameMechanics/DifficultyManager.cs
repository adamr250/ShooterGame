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
    private float enemyLifetimeThisRoundPrevious = 0;

    private float enemyAverageLifetime = 0;
    private float enemyAverageLifetimePrevious = 0;

    private float enemyKilledTotalPrecentage = 0;
    private float enemyKilledTotalPrecentagePrevious = 0;

    
    private int scoreThreshold = 750;

    public static float dynamicDifficultyMultiplier = 0;


    //int tmpTotalLifesCountPrevious = 0;
    //int tmpDeathsCountPrevious = 0;
    private void Start()
    {
        createLogs("\n##############################\nNEW GAME LOGS\n");
        //totalLifesCount = Life.lifeNum;
        //tmpTotalLifesCountPrevious = totalLifesCount;
        //tmpDeathsCountPrevious = deathsCount;
        //Debug.Log("(InGame) default difficulty: " + OptionsMenu.defaultDifficultyMultiplier);
    }

    //int tmpScoreThreshold = 200;
    void Update()
    {
        //Debug.Log("total difficulty: " + (OptionsMenu.defaultDifficultyMultiplier + dynamicDifficultyMultiplier));
        //if (!(enemyKilledCount > enemySpawnedCount))
        //    Debug.Log("spawned: " + enemySpawnedCount + ";  killed: " + enemyKilledCount + ";  precentage: " + enemyKilledPrecentage);
        
        //if (tmpScoreThreshold <= Score.scoreNum)
        //{
        //   tmpScoreThreshold += 200;
        //    tmpLifeScore();
        //}


        if (scoreThreshold <= Score.scoreNum)
        {
            scoreThreshold += 750;

            //Debug.Log("lifes: " + totalLifesCount);
            updateDifficulty();
        }
    }

    
    /*public void tmpLifeScore()
    {
        float tmpEvaluationLifes = 0;
        int x = Life.lifeNum;

        if (x >= 1 && x <= 4)
        {
            tmpEvaluationLifes = ((float)x / 3) - (4.0f / 3.0f);
        } 
        else if (x <= 7)
        {
            tmpEvaluationLifes = Mathf.Tan(((float)x / 2.275f) - 1.758242f) / 4;
        } 
        else
        {
            tmpEvaluationLifes = 1;
        }
        Debug.Log("tmp life score: " + Mathf.Round(tmpEvaluationLifes * 100f) / 100f);
    }*/

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

        if (enemySpawnedCount > 0)
        {
            enemyKilledTotalPrecentage = 100 * (float)(enemyKilledCount / enemySpawnedCount);
            enemyAverageLifetime = enemyLifetimeThisRound / (enemySpawnedCount - enemySpawnedCountPrevious);
        }
        enemyLifetimeThisRound = enemyTotalLifetime - enemyTotalLifetimePrevious;


        //zak³adamy ¿e 4 ¿ycie to granica. poni¿ej 4 ¿yæ wynik ujemny, powy¿ej - dodatni
        int currentLifesCount = Life.lifeNum;
        if (currentLifesCount < 4)
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
                            "\n\tcurrentLifesCount:  " + currentLifesCount.ToString("F6") +
                            "\n Variables: " + "\n\tenemyKilledPrecentageThisRound:  " + enemyKilledPrecentageThisRound.ToString("F6") +
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
        enemyKilledTotalPrecentagePrevious = enemyKilledTotalPrecentage;


        enemyTotalLifetimePrevious = enemyTotalLifetime;
        enemyLifetimeThisRoundPrevious = enemyLifetimeThisRound;
        enemyAverageLifetimePrevious = enemyAverageLifetime;
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

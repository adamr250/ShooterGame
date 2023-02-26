using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetValues : MonoBehaviour
{
    public void reserValues()
    {
        Score.scoreNum = 0;
        Life.lifeNum = 4;
        Pause.isPaused = false;
        Bomb.bombCount = 0;

        BossBodyManager.bodyPartsDestroyedCounter = 0;
        BossBodyManager.bossBodyCompleted = false;
        BossBodyManager.bodyPartsCount = 0;

        DifficultyManager.dynamicDifficultyMultiplier = 0;
        DifficultyManager.enemySpawnedCount = 0;
        DifficultyManager.enemyKilledCount = 0;
        DifficultyManager.enemyTotalLifetime = 0;
        DifficultyManager.scoreThreshold = 1000;
    }
}

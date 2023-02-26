using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bomb : MonoBehaviour
{
    public static int bombCount = 2;
    public Text bombText;

    private void Start()
    {
        bombText.text = "0";
        bombCount = 0;
    }

    public void activateBomb()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
    }

    public void bombTextDisplay(int x)
    {
        bombCount += x;
        bombText.text = bombCount.ToString();

    }
}

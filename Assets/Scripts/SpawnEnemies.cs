using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;

    private Vector2[] enemyLocation = {
        new Vector2 (-3.0f, 3.25f),
        new Vector2 (1.5f, 1.7f),
        new Vector2 (0.0f, 3.25f),
        new Vector2 (-6.0f, 3.25f),
        new Vector2 (7.5f, 1.7f),
        new Vector2 (-4.5f, 1.7f),
        new Vector2 (-1.5f, 1.7f),
        new Vector2 (4.5f, 1.7f),
        new Vector2 (3.0f, 3.25f),
        new Vector2 (6.0f, 3.25f),

        //new Vector2 (-7.5f, 1.7f),
        //new Vector2 (10.5f, 1.7f),
        //new Vector2 (9.0f, 3.25f),
        //new Vector2 (-9.0f, 3.25f),
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemyPrefab, enemyLocation[i], Quaternion.identity);
        }
    }
}

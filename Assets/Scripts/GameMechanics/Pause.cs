using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool Paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {

        if (Paused)
        {
            Time.timeScale = 1.0f;
            Paused = false;
            Debug.Log("Unpaused");

        }
        else if (!Paused)
        {
            Time.timeScale = 0f;
            Paused = true;
            Debug.Log("Paused");
        }
    }
}

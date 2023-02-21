using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] GameObject pauseText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        isPaused = !isPaused;

        if (!isPaused)
        {
            Time.timeScale = 1.0f;
            pauseText.SetActive(true);
            Debug.Log("Unpaused");

        }
        else if (isPaused)
        {
            Time.timeScale = 0f;
            pauseText.SetActive(false);
            Debug.Log("Paused");

        }
    }
}

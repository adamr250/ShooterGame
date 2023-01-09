using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;

    public void playGame()
    {
        //Debug.Log("PLAY");
        SceneManager.LoadSceneAsync(1);
        //SceneManager.UnloadSceneAsync(0);
    }

    public void goToOptionsMenu()
    {
        optionMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void quitGame()
    {
        //Application.Quit();
        Debug.Log("Quit");
    }
}

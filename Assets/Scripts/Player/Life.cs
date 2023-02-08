using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    private int addLifeOnScore = 1000;

    public static int lifeNum = 4;
    public Text lifeText;
    //public GameObject playerObject;

    private void Start()
    {
        lifeText.text = lifeNum.ToString();
    }

    void Update()
    {
        if(Score.scoreNum > addLifeOnScore)
        {
            lifeChangeValue(1);
            addLifeOnScore += addLifeOnScore;
        }

        if(lifeNum <= 0)
        {

            SceneManager.LoadSceneAsync(0);
            //SceneManager.UnloadSceneAsync(1);
        }
    }

    public void lifeChangeValue(int x)
    {
        if(x > 0)
        {
            DifficultyManager.totalLifesCount += x;
        } else
        {
            DifficultyManager.deathsCount++;
        }
        lifeNum += x;
        lifeText.text = lifeNum.ToString();

    } 
}

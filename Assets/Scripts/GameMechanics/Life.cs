using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    private int addLifeOnScore = 1000;

    public int lifeNum = 2;
    public Text lifeText;
    //public GameObject playerObject;

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
        lifeNum += x;
        lifeText.text = lifeNum.ToString();

    } 
}

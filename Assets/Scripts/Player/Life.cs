using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    private int addLifeOnScore = 1500;

    public static int lifeNum = 4;
    public Text lifeText;

    private void Start()
    {
        lifeText.text = lifeNum.ToString();
    }

    void Update()
    {
        if(Score.scoreNum > addLifeOnScore)
        {
            lifeChangeValue(1);
            addLifeOnScore += 1500;
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

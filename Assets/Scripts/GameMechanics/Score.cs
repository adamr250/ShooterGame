using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	private float scoreTimer = 1.0f;
	private float scoreInterval = 1.0f;
	public static int scoreNum = 0;

	public Text scoreText;


    void Update()
    {
		if (Time.time > scoreTimer)
		{
			scoreTimer = Time.time + scoreInterval;
			increaseScore(10);
		}
	}

	public void increaseScore(int x)
    {
		scoreNum += x;
		//Debug.Log("score: " + scoreNum);
		scoreText.text = scoreNum.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	private float scoreTimer = 1.0f;
	private float scoreInterval = 1.0f;
	public static float scoreNum = 0.0f;

	public Text scoreText;
    void Update()
    {
		if (Time.time > scoreTimer)
		{
			scoreTimer = Time.time + scoreInterval;
			increaseScore(10);
		}
	}

	public void increaseScore(float x)
    {
		scoreNum += x;
		//Debug.Log("score: " + scoreNum);
		scoreText.text = (float.Parse(scoreText.text) + x).ToString();
    }
}

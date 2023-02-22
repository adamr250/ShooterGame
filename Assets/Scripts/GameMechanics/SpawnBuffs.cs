using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuffs : MonoBehaviour
{

	[SerializeField] GameObject[] buffType;
	int[] weights = { 20, 50, 30 };  //life, attack, bomb
	int weightsSum = 0;

	private void Start()
    {
		if (weights.Length != buffType.Length)
		{
			Debug.Log("Z³a iloœæ wzmocnieñ lub wag!!!");
			return;
		}

		foreach (int i in weights)
		{
			weightsSum += i;
		}
	}

    private void Update()
    {
		if(Life.lifeNum == 1)
        {
			weights[0] = 120;   //life = 60%; attack = 25%; bomb = 15%
		}
        else if(Life.lifeNum == 2 || Life.lifeNum == 3)
        {
			weights[0] = 65;  //life = 45%; attack = 34.5%; bomb = 20.5%
		} 
		else
        {
			weights[0] = 20;  //life = 20%; attack = 50%; bomb = 30%
		}
	}

    public void spawnBuffs(Vector3 spawnPoint)
	{
		if (weights.Length != buffType.Length)
		{
			Debug.Log("Z³a iloœæ wzmocnieñ lub wag!!!");
			return;
		}

		//losujemy czy buff sie pojawi
		if (Random.Range(0, 100) > 30)
        {
			return;
        }

		//losujemy, ktory buff sie pojawi
		int randomWeight = Random.Range(0, weightsSum);
		for (int i = 0; i < weights.Length; i++)
		{
			randomWeight -= weights[i];
			if (randomWeight < 0)
			{
				Instantiate(buffType[i], spawnPoint, Quaternion.Euler(0.0f, 0.0f, 0.0f));
				return;
			}
		}
		return;
	}
}

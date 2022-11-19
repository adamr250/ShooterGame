using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuffs : MonoBehaviour
{
    public GameObject heart;

	public void spawnBuffs(Vector3 spawnPoint)
	{
		if(Random.Range(0f, 100f) < 30)
		{
			Instantiate(heart, spawnPoint, Quaternion.identity);
		}
	}
}

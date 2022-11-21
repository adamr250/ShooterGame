using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuffs : MonoBehaviour
{
    public GameObject heart;
	public GameObject speed;

	public void spawnBuffs(Vector3 spawnPoint)
	{
		if(Random.Range(0f, 100f) < 50)
		{
			int buffID = Random.Range(1, 3);
			switch(buffID)
            {
				case 1:
					Debug.Log("heart spawned");
					Instantiate(heart, spawnPoint, Quaternion.identity);
					break;
				case 2:
					Debug.Log("speed spawned");
					Instantiate(speed, spawnPoint, Quaternion.identity);
					break;
			}
		}
	}
}

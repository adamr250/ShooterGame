using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuffs : MonoBehaviour
{
    public GameObject heart;
	public GameObject speed;
	public GameObject attack;
	public GameObject bomb;

	public void spawnBuffs(Vector3 spawnPoint)
	{
		if(Random.Range(0f, 100f) < 10)
		{
			int buffID = Random.Range(1, 4);
			switch(buffID)
            {
				case 1:
					Debug.Log("heart spawned");
					Instantiate(heart, spawnPoint, Quaternion.identity);
					break;
				case 2:
					Debug.Log("attac boost spawned");
					Instantiate(attack, spawnPoint, Quaternion.identity);
					break;
				case 3:
					Debug.Log("bomb spawned");
					Instantiate(bomb, spawnPoint, Quaternion.identity);
					break;
				/*case 4:
					Debug.Log("speed boost spawned");
					Instantiate(speed, spawnPoint, Quaternion.identity);
					break;
				*/
			}
		}
	}
}

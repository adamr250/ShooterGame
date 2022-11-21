using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdminPanel : MonoBehaviour
{
    Player player;
    public GameObject playerObject;

    SpawnEnemies spawnEnemies;

    public InputField inputField;
    public GameObject hommingEnemy;
    public GameObject sniperEnemy;

    private string textInput;
    private string[] commandsList = { "normal", "homming", "sniper", "kill_all", "godmode", "add_score"};


    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        spawnEnemies = gameObject.GetComponent<SpawnEnemies>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        { 
            inputField.ActivateInputField(); 
        }
    }

    public void normal()
    {
        spawnEnemies.spawnNormal();
        Debug.Log("Spawning normal");
    }

    public void homming()
    {
        spawnEnemies.spawnHomming();
        Debug.Log("Spawning homming");
    }

    public void sniper()
    {
        spawnEnemies.spawnSniper();
        Debug.Log("Spawning sniper");
    }

    /*public void bulletHellTime()
    {
        BulletHellTime.bulletHellTime();
    }*/

    public void kill_all()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
        Debug.Log("kill all");
    }

    public void godmode()
    {
        player.godmode();
    }

    public void readStringInput(string s)
    {
        textInput = inputField.text;
        inputField.text = "";

        if (Input.GetKeyDown(KeyCode.Return)) 
        { 
            developerCommands(textInput); 
        }
    }
     void developerCommands(string textInput)
    {
        foreach(string s in commandsList)
        {
            if(s == textInput)
            {
                Invoke(textInput, 0);
            }
        }
    }
    
}

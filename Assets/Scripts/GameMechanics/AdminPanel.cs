using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdminPanel : MonoBehaviour
{
    Player player;
    public GameObject playerObject;

    public InputField inputField;
    public GameObject hommingEnemy;
    public GameObject sniperEnemy;
    private Vector3 spawnPoint1 = new Vector3(10f, 3.0f, 0.0f);
    private string textInput;
    private string[] commandsList = { "normal", "homming", "sniper", "kill_all", "godmode", "add_score"};


    private void Start()
    {
        player = playerObject.GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote)) 
        { 
            inputField.ActivateInputField(); 
        }
    }

    public void homming()
    { 
        Instantiate(hommingEnemy, spawnPoint1, Quaternion.identity);
    }

    /*public void bulletHellTime()
    {
        BulletHellTime.bulletHellTime();
    }*/

    public void sniper()
    {
        Vector3 spawnPointSniper = new Vector3(Random.Range(-2f, 8f), Random.Range(-0.5f, 0.5f) + 4.25f, 0.0f);
        Instantiate(sniperEnemy, spawnPointSniper, Quaternion.identity);
    }

    public void kill_all()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        for (int i=0; i< enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < enemyBullets.Length; i++)
        {
            Destroy(enemyBullets[i]);
        }
    }

    public void godmode()
    {
        player.godmode();
    }

    public void readStringInput(string s)
    {
        textInput = inputField.text;
        //Debug.Log("text_input: " + textInput);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    BossHealthBar bossHealthBar;
    //[SerializeField] private GameObject bossHealth;

    void Start()
    {
        GameObject bossHealth = GameObject.Find("BossHealthBar");
        bossHealthBar = bossHealth.GetComponent<BossHealthBar>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            dmgTaken(10);
        }
    }

    public void dmgTaken(int dmg)
    {
        bossHealthBar.damageTaken(dmg);
        //Debug.Log("Boss damaged");
    }
}

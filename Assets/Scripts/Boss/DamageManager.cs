using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    BossHealthBar bossHealthBar;

    void Start()
    {
        GameObject bossHealth = GameObject.Find("BossHealthBar");
        bossHealthBar = bossHealth.GetComponent<BossHealthBar>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            dmgTaken(20);
        }
    }

    public void dmgTaken(int dmg)
    {
        bossHealthBar.damageTaken(dmg);
    }
}

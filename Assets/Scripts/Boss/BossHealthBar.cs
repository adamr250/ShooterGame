using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    BossBodyManager bossBodyManager;
    [SerializeField] private GameObject BossBody;

    private int bossMaxHealth = 1600;
    public Slider slider;
    private int healthSegmentValue;
    private int healthSegment;
    private int healthThreshold;
    
    void Start()
    {
        healthThreshold = bossMaxHealth;
        healthSegmentValue = bossMaxHealth / 8;
        healthSegment = healthSegmentValue;

        setMaxHealth(bossMaxHealth);

        bossBodyManager = BossBody.GetComponent<BossBodyManager>();
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void damageTaken(int damage)
    {
        if (BossBodyManager.isInvulnerable)
            return;
        healthSegment -= damage;

        if (healthSegment > 0)
        {
            slider.value -= damage;
            if (slider.value <= 0)
            {
                //bossDeath();
            }
        } 
        else
        {
            BossBodyManager.isInvulnerable = true;

            healthThreshold -= healthSegmentValue;
            slider.value = healthThreshold;
            healthSegment = healthSegmentValue;
            bossBodyManager.destoryBodySegment();
        }
        //Debug.Log(slider.value);
    }
}

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

    public static bool isInvulnerable = true;
    private float invulnerabilityDuration = 1.0f;
    private float invulnerabilityTimer = 0.0f;

    void Start()
    {
        healthThreshold = bossMaxHealth;
        healthSegmentValue = bossMaxHealth / 8;
        healthSegment = healthSegmentValue;

        setMaxHealth(bossMaxHealth);

        bossBodyManager = BossBody.GetComponent<BossBodyManager>();
    }

    private void Update()
    {
        if(Time.time > invulnerabilityTimer)
        {
            isInvulnerable = false;
        }
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void damageTaken(int damage)
    {
        if (isInvulnerable || !BossBodyManager.bossBodyCompleted)
            return;
        healthSegment -= damage;

        if (healthSegment > 0)
        {
            slider.value -= damage;
            
        } 
        else
        {
            isInvulnerable = true;
            invulnerabilityTimer = Time.time + invulnerabilityDuration;

            healthThreshold -= healthSegmentValue;
            slider.value = healthThreshold;
            healthSegment = healthSegmentValue;
            bossBodyManager.destoryBodySegment();

            Debug.Log("Body part destroyed");
        }

        Debug.Log("boss health: " + slider.value);

        if (slider.value <= 0)
        {
            bossBodyManager.death();
        }
    }
}

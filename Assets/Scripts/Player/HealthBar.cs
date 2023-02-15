using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Life life;

    private int playerMaxHealth = 100;
    private float invulnerabilityTimer = 0.0f;
    private float afterDeathInvulnerabilityDuration = 0.5f;
    private float afterDamageInvulnerabilityDuration = 0.1f;
    private bool isInvulnerable = false;

    public GameObject lifeHolder;
    public Slider slider;

    void Start()
    {
        life = lifeHolder.GetComponent<Life>();
        setMaxHealth(playerMaxHealth);
    }

    private void Update()
    {
        if (Time.time > invulnerabilityTimer)
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
        if (isInvulnerable)
            return;

        slider.value -= damage;
        if(slider.value <= 0)
        {
            life.lifeChangeValue(-1);
            setMaxHealth(playerMaxHealth);

            isInvulnerable = true;
            invulnerabilityTimer = Time.time + afterDeathInvulnerabilityDuration;

        } else
        {
            isInvulnerable = true;
            invulnerabilityTimer = Time.time + afterDamageInvulnerabilityDuration;
        }
    }
}

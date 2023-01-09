using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Life life;

    private int playerMaxHealth = 100;

    public GameObject lifeHolder;
    public Slider slider;

    void Start()
    {
        life = lifeHolder.GetComponent<Life>();
        setMaxHealth(playerMaxHealth);
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void damageTaken(int damage)
    {
        slider.value -= damage;
        if(slider.value <= 0)
        {
            life.lifeChangeValue(-1);
            setMaxHealth(playerMaxHealth);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public GameObject lifeHolder;
    public Slider slider;

    void Start()
    {
        setMaxHealth(maxHealth);
    }

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void damageTaken(int damage)
    {
        slider.value -= damage;
        if (slider.value <= 0)
        {
            //destory object
        }
    }
}

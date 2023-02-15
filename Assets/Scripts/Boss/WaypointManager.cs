using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private static float transformTimer;
    private static float transformCD;
    
    void Start()
    {
        transformCD = 5;
        transformTimer = transformCD + Time.time;
    }

    void Update()
    {
        if (Time.time > transformTimer)
        {
            changePosition(gameObject);
        }
    }

    public void changePosition(GameObject target)
    {
        transformTimer = Time.time + transformCD;
        target.transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
    }    

    public void refreshTimer()
    {
        transformTimer = Time.time + transformCD;
    }
}

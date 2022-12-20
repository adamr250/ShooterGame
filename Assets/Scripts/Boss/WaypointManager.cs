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

        //Debug.Log("timer 1: " + transformTimer);
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
        //Debug.Log("Waypoint_Pos: " + tmp++);
    }    

    public void refreshTimer()
    {
        transformTimer = Time.time + transformCD;
    }
}

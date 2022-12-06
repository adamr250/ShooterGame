using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private float transformTimer;
    private float transformCD;

    private bool showTime = true;

    private void Start()
    {
        transformCD = 5;
        transformTimer = transformCD + Time.time;

        Debug.Log("timer 1: " + transformTimer);
    }

    public void Update()
    {
        if(!showTime)
        {
            Debug.Log("T: " + (transformTimer - Time.time));
            //Debug.Log("transform time: " + (transformTimer - Time.time) + "---------------------------------------------");
            //showTime = true;
        }
        if(showTime)
        {
            Debug.Log("I DON'T KNOW WHAT IS GOING ON!!!");
        }
        if (Time.time > transformTimer)
        {
            transformTimer = Time.time + transformCD;
            //Debug.Log("transform time: " + (transformTimer - Time.time));
            transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
            Debug.Log("Waypoint_Pos: " + transform.position);
            //changePosition();
        }
    }

    /*public void changePosition()
    {
        transformTimer = Time.time + transformCD;
        Vector3 oldPosition = transform.position;
        transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
        Vector3 newPosition = transform.position;
        if(oldPosition == newPosition)
        {
            Debug.Log("WHAT THE... ?!");
        } else
        {
            Debug.Log("Old: " + oldPosition + ";     New: " + newPosition);
        }
        Debug.Log("here");
    }*/

    public void refreshTimer()
    {
        Debug.Log("timer 2: " + transformTimer);
        transformTimer = Time.time + transformCD;
        Debug.Log("refreshed timer: " + (transformTimer-Time.time));
        showTime = false;
    }

    
}

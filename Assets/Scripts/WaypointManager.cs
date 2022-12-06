using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    private static float transformTimer;
    private static float transformCD;

    private static bool showTimer = false;

    public static int hope = 10;

    private void Start()
    {
        transformCD = 5;
        transformTimer = transformCD + Time.time;

        Debug.Log("timer 1: " + transformTimer);
    }

    public void Update()
    {
        /*if(!showTimer)
         {
             Debug.Log("T: " + (transformTimer - Time.time));
             //Debug.Log("transform time: " + (transformTimer - Time.time) + "---------------------------------------------");
             //showTime = true;
         }
         if(showTimer == true)
         {
             Debug.Log("I DON'T KNOW WHAT IS GOING ON!!! PLEASE HELP!!!");
         }
         if (Time.time > transformTimer)
         {
             transformTimer = Time.time + transformCD;
             //Debug.Log("transform time: " + (transformTimer - Time.time));
             transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
             Debug.Log("Waypoint_Pos: " + transform.position);
             //changePosition();
         }*/
        changePosition();
    }

    public void changePosition()
    {
        //Debug.Log("HOPE: " + hope);
        if(showTimer)
        {
            Debug.Log("T: " + (transformTimer - Time.time));
            //showTimer = false;
        }
        if (showTimer == false)
        {
            Debug.Log("I DON'T KNOW WHAT IS GOING ON!!! PLEASE HELP!!!");
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

    public void refreshTimer()
    {
        //Debug.Log("timer 2: " + transformTimer);
        transformTimer = Time.time + transformCD;
        hope = 5;
        //Debug.Log("refreshed timer: " + (transformTimer-Time.time));
        showTimer = true;
    }

    
}

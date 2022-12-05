using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform waypoints;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Vector3 target;
    private Vector3 direction;

    void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    void FixedUpdate()
    {
        if (transform.position != waypoints.transform.position)
        {
            target = waypoints.transform.position;

            direction = target - transform.position;
            //float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, rotationSpeed * Time.deltaTime, 0.0f);
            Debug.DrawRay(transform.position, newDirection, Color.red);

        
            transform.position = Vector3.MoveTowards(transform.position, waypoints.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(newDirection, new Vector3(0,0,1));
        }
    }

}

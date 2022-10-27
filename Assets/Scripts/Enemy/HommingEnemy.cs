using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingEnemy : MonoBehaviour
{
    public float speed = 2;

    private float horizontalMovement;
    private float verticalMovement;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = GameObject.Find("Player").transform.position.x - transform.position.x;
        verticalMovement = GameObject.Find("Player").transform.position.y - transform.position.y;

        //float movement = speed * Time.deltaTime;
        //transform.Translate(0, -movement, 0);
        body.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);

        //Debug.Log("Player: " + GameObject.Find("Player").transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Kontakt");
        }
    }
}

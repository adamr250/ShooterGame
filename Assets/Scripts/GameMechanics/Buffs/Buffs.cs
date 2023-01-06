using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    Life life;

    private GameObject lifeHolder;
    private Rigidbody2D body; 

    void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            body = gameObject.AddComponent<Rigidbody2D>();
            body.gravityScale = 0;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        lifeHolder = GameObject.Find("LifesVal");
        life = lifeHolder.GetComponent<Life>();
        Destroy(gameObject, 5.0f);
    }

    /*private void FixedUpdate()
    {
        movement();
    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //life.lifeChangeValue(1);
        }
    }

   /* void movement()
    {
        //float speed = 2f;
        //float movementHorizontal = speed * Time.deltaTime;
        //transform.Translate(0, -movementHorizontal, 0);

        float speed = 50f;
        body.velocity = body.transform.up * speed * Time.deltaTime;
    }*/
}
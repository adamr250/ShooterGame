using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    Life life;

    private GameObject lifeHolder;

    void Start()
    {
        lifeHolder = GameObject.Find("LifesVal");
        life = lifeHolder.GetComponent<Life>();
    }

    private void FixedUpdate()
    {
        movement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            life.lifeChangeValue(1);
        }
    }

    void movement()
    {
        float speed = 2f;
        float movementHorizontal = speed * Time.deltaTime;
        transform.Translate(0, -movementHorizontal, 0);
    }
}
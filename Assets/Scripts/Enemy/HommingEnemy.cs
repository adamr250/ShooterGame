using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingEnemy : MonoBehaviour
{
    public float speed = 2;

    private Vector3 direction;
    //private Vector2 movement;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        direction = GameObject.Find("Player").transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.rotation = angle;
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        moveHommingEnemy(direction);
    }

    void moveHommingEnemy (Vector2 dir)
    {
        body.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet")
        {
            //Debug.Log("Kontakt");
            Destroy(gameObject);
        }
    }
}

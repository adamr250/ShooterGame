using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 directory;
    private Vector3 eulerAngles;

    public float speed;
    public float lifeTime;

    private Rigidbody2D body;

    private void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    /*private void Setup(Vector3 direction)
    {
        this.direction = direction;
        eulerAngles = transform.rotation.eulerAngles;
    }*/

    void FixedUpdate()
    {
        /*body.velocity = -body.transform.up * speed * Time.deltaTime;
        float movement = speed * Time.deltaTime;
		transform.Translate(0, movement, 0);
        */Destroy(gameObject, lifeTime);

        //float speed = 50f;
        body.velocity = body.transform.up * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player")
        {   
            //Debug.Log("transform.rotation angles x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z); 
            Destroy(gameObject);
        }
    }
}

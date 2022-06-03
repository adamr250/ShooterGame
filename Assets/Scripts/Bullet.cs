using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 directory;
    private Vector3 eulerAngles;
    
    /*private void Setup(Vector3 direction)
    {
        this.direction = direction;
        eulerAngles = transform.rotation.eulerAngles;
    }*/

    void Update()
    {
        float movement = speed * Time.deltaTime;
		transform.Translate(0, movement, 0);
        Destroy(gameObject, 4);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {   
            //Debug.Log("transform.rotation angles x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z); 
            Debug.Log("Hit wall");
            Destroy(gameObject);
        }
    }
}

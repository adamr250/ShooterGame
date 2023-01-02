using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 directory;
    private Vector3 eulerAngles;

    private float speed = 9.0f;
    private float lifeTime = 0.75f;
    /*private void Setup(Vector3 direction)
    {
        this.direction = direction;
        eulerAngles = transform.rotation.eulerAngles;
    }*/

    void Update()
    {
        float movement = speed * Time.deltaTime;
		transform.Translate(0, movement, 0);
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string objectTag = collision.gameObject.tag;
        if (objectTag == "Obstacle" || objectTag == "Enemy" || objectTag == "Boss")
        {   
            //Debug.Log("transform.rotation angles x: " + eulerAngles.x + " y: " + eulerAngles.y + " z: " + eulerAngles.z); 
            //Debug.Log("Hit wall");
            Destroy(gameObject);
        }
    }
}

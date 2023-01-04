using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    WaypointManager waypointManager;

    [SerializeField] private GameObject waypoint;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private float defaultSpeed;
    private float defaultRotationSpeed;

    private GameObject target;
    private Vector3 direction;

    Rigidbody2D body;

    void Start()
    {
        defaultSpeed = speed;
        defaultRotationSpeed = rotationSpeed;

        target = Instantiate(waypoint, new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);

        waypointManager = waypoint.GetComponent<WaypointManager>();

        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        speed = defaultSpeed * (1 + (float)BossBodyManager.bodyPartsDestroyedCounter/7);
        //Debug.Log("speed: " + speed);

        rotationSpeed = defaultRotationSpeed * (1 + (float)BossBodyManager.bodyPartsDestroyedCounter / 7);
    }

    void FixedUpdate()
    {
        body.velocity = body.transform.right * speed * Time.deltaTime;

        direction = target.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.MoveRotation(Mathf.LerpAngle(body.rotation, rotation, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //waypointManager.refreshTimer();
        //target.transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
        waypointManager.changePosition(target);
    }

    public void OnDestroy()
    {
        Destroy(target);
    }
}

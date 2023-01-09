using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    WaypointManager waypointManager;

    [SerializeField] private GameObject waypoint;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private float defaultRotationSpeed;

    private GameObject playerTarget;
    private GameObject waypointTarget;
    private GameObject currentTarget;

    private float changeTargetTimer = 0.0f;
    private float changeTargetCooldown = 5.0f;

    private Vector3 direction;

    Rigidbody2D body;

    void Start()
    {
        defaultRotationSpeed = rotationSpeed;

        waypointTarget = Instantiate(waypoint, new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);
        playerTarget = GameObject.Find("TargetForBoss");
        waypointManager = waypoint.GetComponent<WaypointManager>();

        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rotationSpeed = defaultRotationSpeed * (1 + (float)BossBodyManager.bodyPartsDestroyedCounter / 7);
    }

    void FixedUpdate()
    {
        body.velocity = body.transform.right * speed * Time.deltaTime;

        direction = currentTarget.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.MoveRotation(Mathf.LerpAngle(body.rotation, rotation, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //waypointManager.refreshTimer();
        //target.transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
        waypointManager.changePosition(currentTarget);
    }

    public void OnDestroy()
    {
        Destroy(currentTarget);
    }
}

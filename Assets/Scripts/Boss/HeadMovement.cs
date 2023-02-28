using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    WaypointManager waypointManager;

    [SerializeField] private GameObject waypoint;
    [SerializeField] private float speed;
    [SerializeField] private float defaultRotationSpeed = 2.25f;

    private float rotationSpeed;

    private GameObject playerTarget;
    private GameObject waypointTarget;
    private GameObject currentTarget;

    private float changeTargetTimer = 0.0f;
    private float changeTargetCooldown = 5.0f;

    private Vector3 direction;

    Rigidbody2D body;

    void Start()
    {
        rotationSpeed = defaultRotationSpeed;

        waypointTarget = Instantiate(waypoint, new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0), Quaternion.identity);
        playerTarget = GameObject.Find("Player");
        waypointManager = waypoint.GetComponent<WaypointManager>();

        body = GetComponent<Rigidbody2D>();

        currentTarget = waypointTarget;
    }

    private void Update()
    {
        rotationSpeed = defaultRotationSpeed * (1 + (10 - (float)BossBodyManager.bodyPartsCount) / 7);
    }

    void FixedUpdate()
    {
        //jesli poziom trudnosi jest odpowiednio niski boss czasami lecie na gracza zamiast w strone waypointu
        if ( ((OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier) <= 0.9f) && (changeTargetTimer < Time.time) )
        {
            changeTargetTimer = Time.time + changeTargetCooldown;
            if (Random.Range(0, 10) < 5)
            {
                currentTarget = playerTarget;
                Debug.Log("Target player");
            }
            else
            {
                currentTarget = waypointTarget;
                Debug.Log("Target waypoint");
            }
        } 
        else
        {
            currentTarget = waypointTarget;
        }

        body.velocity = body.transform.right * speed;

        direction = currentTarget.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.MoveRotation(Mathf.LerpAngle(body.rotation, rotation, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        waypointManager.changePosition(waypointTarget);
    }

    public void OnDestroy()
    {
        Destroy(waypointTarget);
        rotationSpeed = defaultRotationSpeed;
    }
}

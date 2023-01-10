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

        currentTarget = waypointTarget;
    }

    private void Update()
    {
        rotationSpeed = defaultRotationSpeed * (1 + (float)BossBodyManager.bodyPartsDestroyedCounter / 7);
    }

    void FixedUpdate()
    {
        if ( ((OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier) <= 0.9f) && (changeTargetTimer < Time.time) )
        {
            Debug.Log("diff multip: " + OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier);

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
        else if((OptionsMenu.defaultDifficultyMultiplier + DifficultyManager.dynamicDifficultyMultiplier) > 0.9f)
        {
            currentTarget = waypointTarget;
        }

        body.velocity = body.transform.right * speed * Time.deltaTime;

        direction = currentTarget.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        body.MoveRotation(Mathf.LerpAngle(body.rotation, rotation, rotationSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //waypointManager.refreshTimer();
        //target.transform.position = new Vector3(Random.Range(-2.0f, 8.0f), Random.Range(-5.0f, 5.0f), 0);
        waypointManager.changePosition(waypointTarget);
    }

    public void OnDestroy()
    {
        Destroy(waypointTarget);
    }
}

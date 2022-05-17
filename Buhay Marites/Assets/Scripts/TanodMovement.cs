using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TanodMovement : MonoBehaviour
{
    TanodFOV fov;
    public Transform target;
    public GameObject tanod;

    /*
     * Barangay Tanod Status:
     * 0: Idling
     * 1: Chasing
     * 2: Returning
     */
    private int tanodStatus = 0;

    // Rotation settings
    public float rotateInterval = 1f;
    public float rotateSpeed = 3f;

    float rInterval;
    float direction = 0f;

    // Walk/Patrol settings
    public float walkInterval = 1f;
    public float walkSpeed = 2f;

    float wInterval;

    // Chasing settings
    public float chaseSpeed = 3f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    public Seeker seeker;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<TanodFOV>();

        //InvokeRepeating("FindPath", 0f, 2f);

        ChangeDirection();
        rInterval = rotateInterval;
        transform.eulerAngles = new Vector3(0f, 0f, direction);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is visible in fov
        if (!fov.playerVisible)
        {
            // Rotation interval
            if (rInterval <= 0)
            {
                ChangeDirection();
                rInterval = rotateInterval;
            }
            else
            {
                // Rotation
                Quaternion rotation = Quaternion.AngleAxis(direction, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            }
            rInterval -= Time.deltaTime; // Interval counter
        }
    }

    private void FixedUpdate()
    {
        if(fov.playerVisible)
        {
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEnd = true;
                return;
            }
            else
            {
                reachedEnd = false;
            }

            Vector2 movedirection = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)tanod.transform.position).normalized;
            Vector2 force = movedirection * chaseSpeed * Time.deltaTime;

            tanod.transform.Translate(force);

            float distance = Vector2.Distance(tanod.transform.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
                currentWaypoint++;
        }
    }

    void FindPath()
    {
        if(seeker.IsDone()) seeker.StartPath(tanod.transform.position, target.position, delegate (Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        });
    }

    // Give a random direction of rotation
    private void ChangeDirection()
    {
        float[] directions = { 90f, -90f };
        direction = (direction + directions[Random.Range(0, directions.Length)]) % 360;
    }
}

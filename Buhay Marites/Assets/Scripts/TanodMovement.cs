using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TanodMovement : MonoBehaviour
{
    public TanodFOV fov;
    public TanodChangeView view;
    public Transform target;
    public TanodWaypoint currentPoint;

    // Rotation settings
    public float rotateSpeed = 1f;
    public int maxRotateCount = 10;

    float directionAngle = 0f;
    int rotateCounter = 0;

    // Walk/Patrol settings
    public float walkSpeed = 6f;

    TanodWaypoint nextPoint;
    int pointStepDirection;

    // Pathfinding (Chasing) settings
    float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEnd = false;

    public Seeker seeker;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // Start the constant pathfinding
        InvokeRepeating("FindPath", 0f, 0.5f);

        // Initialize next way point
        //pointStepDirection = Random.Range(0, 2);
        ChangeNextPoint();

        // Initialize view direction
        ChangeDirection();
        transform.eulerAngles = new Vector3(0f, 0f, directionAngle);
    }

    void FixedUpdate()
    {
        // Check if player is visible to tanod FOV
        if (fov.playerVisible)
        {
            // Rotate the view according the the direction facing
            Quaternion rotation = Quaternion.AngleAxis(view.rotationDirection * -90f, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 750 * Time.deltaTime);

            // Check if path is empty
            if (path == null)
                return;

            // Check if tanod reached player
            // Update the reachedEnd
            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEnd = true;
                return;
            }
            else
            {
                reachedEnd = false;
            }

            // Chase the player
            ChasePlayer();
        }
        // Start patroling
        else
        {
            // Patrol path has next waypoint
            if(currentPoint.nextWaypoint)
            {
                if (rotateCounter == maxRotateCount)
                    RotateToNextIdlePoint();
                else
                    RotateOnIdle();
            }
            // Patrol path is stationary
            else
            {
                RotateOnIdle();
                rotateCounter = rotateCounter == maxRotateCount ? 0 : rotateCounter;
            }
        }
    }

    void RotateToNextIdlePoint()
    {
        // Gets a direction angle according to the next waypoint using the point step direction
        directionAngle = pointStepDirection == 0 ? currentPoint.DirectionToPrevious() : currentPoint.DirectionToNext();
         
        // Rotate first before move
        if (transform.eulerAngles.z != directionAngle)
        {
            // Rotate to next waypoint
            Quaternion rotation = Quaternion.AngleAxis(directionAngle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * 100 * Time.deltaTime);
        }
        // Move after rotated
        else
        {
            MoveToNextIdlePoint();
        }
    }

    void MoveToNextIdlePoint()
    {
        // Get the direction from tanod to the next waypoint
        Vector2 walkDirection = ((Vector2)nextPoint.transform.position - rb.position).normalized;

        // Start walking
        Vector2 force = walkDirection * walkSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + force);

        // Identify the distance between the tanod and next waypoint
        float distance = Vector2.Distance(rb.position, (Vector2)nextPoint.transform.position);

        if (distance < 0.1f)
        {
            // Change the tanod position to the waypoint
            rb.position = nextPoint.transform.position;
            // Update the current point
            currentPoint = nextPoint;

            // Change the next point
            ChangeNextPoint();

            // Reset rotation counter
            rotateCounter = 0;
        }
    }

    void RotateOnIdle()
    {
        // Check if rotation is done
        if (transform.eulerAngles.z == directionAngle)
        {
            // Change the rotation direction
            ChangeDirection();
            // Increase rotation counter
            rotateCounter++;
        }
        else
        {
            // Rotate
            Quaternion rotation = Quaternion.AngleAxis(directionAngle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * 100 * Time.deltaTime);
        }
    }

    // Give a random direction of rotation
    void ChangeDirection()
    {
        float[] directions = { 90f, -90f };
        // Limit the value between 0 and 359;
        directionAngle = (directionAngle + directions[Random.Range(0, directions.Length)]) % 360;
        // If negative, add 360 to make it positive
        directionAngle += directionAngle < 0 ? 360 : 0;
    }

    // Update the next waypoint
    void ChangeNextPoint()
    {
        // Stationary
        if(currentPoint.nextWaypoint == null)
        {
            pointStepDirection = -1;
            nextPoint = null;
            return;
        }

        // Linear
        if(currentPoint.previousWaypoint == null)
        {
            pointStepDirection = 1;
            nextPoint = currentPoint.nextWaypoint;
            return;
        }

        // Circular
        pointStepDirection = Random.Range(0, 2);
        nextPoint = pointStepDirection == 0 ? currentPoint.previousWaypoint : currentPoint.nextWaypoint;
    }

    void ChasePlayer()
    {
        Vector2 movedirection = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = movedirection * (walkSpeed * 200) * Time.deltaTime;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    // Use the pathfinder to generate path to player
    void FindPath()
    {
        if (seeker.IsDone()) seeker.StartPath(rb.position, target.position, delegate (Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
            }
        });
    }
}

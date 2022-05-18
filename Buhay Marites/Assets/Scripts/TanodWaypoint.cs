using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanodWaypoint : MonoBehaviour
{
    /*
     * If tanod is stationary, both should be null
     * 
     * If tanod has only linear patrol path, previousWaypoint should be null
     * 
     * If tanod has circular patrol path, both shouldn't be null
     */
    public TanodWaypoint previousWaypoint;
    public TanodWaypoint nextWaypoint;

    public float DirectionToPrevious()
    {
        return direction(transform.position, previousWaypoint.transform.position);
    }

    public float DirectionToNext()
    {
        return direction(transform.position, nextWaypoint.transform.position);
    }

    private float direction(Vector2 current, Vector2 next)
    {
        if (current.y - next.y == 0 && current.x < next.x) return 270f;
        if (current.y - next.y == 0 && current.x > next.x) return 90f;
        if (current.x - next.x == 0 && current.y < next.y) return 0f;
        if (current.x - next.x == 0 && current.y > next.y) return 180f;

        return 180f;
    }
}

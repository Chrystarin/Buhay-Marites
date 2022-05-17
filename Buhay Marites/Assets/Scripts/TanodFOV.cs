using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanodFOV : MonoBehaviour
{
    // FOV settings
    public float radius = 2.5f;
    [Range(1, 360)]
    public float viewAngle = 60f;

    // Game Objects
    public LayerMask playerLayer, wallLayer;
    public GameObject player;

    // Player visibility
    public bool playerVisible;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Start the checking
        StartCoroutine(checkFOV(0.2f));
    }

    // This will limit the number of checks per second
    // instead of per frame to improve performance
    private IEnumerator checkFOV(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FOV();
        }
    }

    private void FOV()
    {
        // Get all collided objects
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        if (rangeCheck.Length > 0)
        {
            Transform found = rangeCheck[0].transform;
            // Get the direction of the Player
            Vector2 directionToPlayer = (found.position - transform.position).normalized;

            // Check if the Player is in FOV range
            if (Vector2.Angle(transform.up, directionToPlayer) < viewAngle / 2)
            {
                // Get the distance from the Player to this Tanod
                float distanceToPlayer = Vector2.Distance(transform.position, found.position);

                // Check if there is a wall in between the Player and this Tanod 
                if (!Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, wallLayer))
                    playerVisible = true;
                else
                    playerVisible = false;

                return;
            }
        }

        // Reset Player visibility
        if (playerVisible)
            playerVisible = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -viewAngle / 2);
        Vector3 angle2 = DirectionFromAngle(-transform.eulerAngles.z, viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle1 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * radius);

        if (playerVisible)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eY, float angleDeg)
    {
        angleDeg += eY;
        return new Vector2(Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
}

using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2f;

    [Header("Pathfinding")]
    public List<Waypoint> path = new List<Waypoint>();
    private int pathIndex = 0;

    private void Update()
    {
        if (path == null || pathIndex >= path.Count) return;

        Waypoint target = path[pathIndex];
        Vector3 direction = target.transform.position - transform.position;

        // Move towards current target waypoint
        transform.position += direction.normalized * speed * Time.deltaTime;

        // If we're close enough, move to the next waypoint
        if (direction.magnitude < 0.1f)
        {
            pathIndex++;
        }
    }
}


using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;

    private void OnDrawGizmos()
    {
      Gizmos.color = Color.yellow;
      if (nextWaypoint != null)
        Gizmos.DrawLine(transform.position, nextWaypoint.transform.position);
    }
}

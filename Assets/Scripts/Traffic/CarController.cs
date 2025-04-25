using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 2f;
    public Waypoint currentWaypoint;

    private void Update()
    {
      if (currentWaypoint == null) return;
      {
          Vector3 direction = currentWaypoint.transform.position - transform.position;
          transform.position += direction * speed * Time.deltaTime;

          if (direction.magnitude < 0.1f && currentWaypoint.nextWaypoint != null)
          {
            currentWaypoint = currentWaypoint.nextWaypoint;
          }
      }
    }
}

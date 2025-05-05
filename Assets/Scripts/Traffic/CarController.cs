using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
  public List<Waypoint> path;
  public float speed = 5f;
  public float turnSpeed = 20f; // Increased for sharper turns

  int currentIndex = 0;

  void Update()
  {
    if (path == null || currentIndex >= path.Count) return;

    Vector3 target = path[currentIndex].transform.position;
    Vector3 direction = target - transform.position;

    // Don't rotate until you're very close to current target (fixes cutting)
    if (direction.magnitude < 0.1f)
    {
      currentIndex++;
      return;
    }

    // Rotate toward target sharply
    Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime * 100f);

    // Move forward along current facing
    transform.position += transform.up * speed * Time.deltaTime;
  }
}

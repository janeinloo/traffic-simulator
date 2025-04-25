using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject carPrefab;
  public Waypoint spawnWaypoint;
  public float spawnInterval = 3f;

  void Start()
  {
    InvokeRepeating(nameof(SpawnCar), 2f, spawnInterval);
  }

  void SpawnCar()
  {
    if (carPrefab == null)
    {
      Debug.LogWarning("Spawner has no carPrefab assigned!");
      return;
    }

    GameObject car = Instantiate(carPrefab, spawnWaypoint.transform.position, Quaternion.identity);

    if (car != null && spawnWaypoint != null)
    {
      CarController carScript = car.GetComponent<CarController>();
      carScript.currentWaypoint = spawnWaypoint;
    }
  }
}

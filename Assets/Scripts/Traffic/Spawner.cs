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
    GameObject car = Instantiate(carPrefab, spawnWaypoint.transform.position, Quaternion.identity);
    CarController carScript = car.GetComponent<CarController>();
    carScript.currentWaypoint = spawnWaypoint;
  }
}

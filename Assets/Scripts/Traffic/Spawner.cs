using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
public GameObject carPrefab;
public Waypoint spawnWaypoint;
public float spawnInterval = 3f;

private Waypoint[] exitWaypoints;

void Start()
{
  GameObject[] exitObjects = GameObject.FindGameObjectsWithTag("Exit");
  exitWaypoints = new Waypoint[exitObjects.Length];

  for (int i = 0; i < exitObjects.Length; i++)
  {
    exitWaypoints[i] = exitObjects[i].GetComponent<Waypoint>();
  }

  InvokeRepeating(nameof(SpawnCar), 2f, spawnInterval);
}

void SpawnCar()
{
  if (carPrefab == null || spawnWaypoint == null || exitWaypoints.Length == 0)
  {
    Debug.LogWarning("Spawner is missing prefab or exit points!");
    return;
  }

  Waypoint chosenExit = GetRandomExit(spawnWaypoint);
  if (chosenExit == null)
  {
    Debug.LogWarning($"[Spawner] No valid exit found for {spawnWaypoint.name}");
    return;
  }

  var path = Pathfinding.FindPath(spawnWaypoint, chosenExit);
  if (path == null || path.Count < 2)
  {
    Debug.LogWarning($"[Spawner] No valid path from {spawnWaypoint.name} to {chosenExit.name}");
    return;
  }

  if (path.Count < 2) return;

  Vector3 nextDir = path[1].transform.position - path[0].transform.position;
  Quaternion instantRotation = Quaternion.LookRotation(Vector3.forward, nextDir.normalized);

  // Apply rotation first
  GameObject car = Instantiate(carPrefab);
  car.transform.rotation = instantRotation;

  // Now apply position offset based on new rotation
  Vector3 offset = -car.transform.up * 0.5f;
  car.transform.position = spawnWaypoint.transform.position + offset;

  CarController carScript = car.GetComponent<CarController>();
  carScript.path = path;

}

Waypoint GetRandomExit(Waypoint from)
{
  List<Waypoint> candidates = new List<Waypoint>();

  foreach (var exit in exitWaypoints)
  {
    if (exit == null) continue;
    if (exit.name.Contains(from.name.Split('_')[1])) continue;

    candidates.Add(exit);
  }

  if (candidates.Count == 0) return null;

  return candidates[Random.Range(0, candidates.Count)];
}
}

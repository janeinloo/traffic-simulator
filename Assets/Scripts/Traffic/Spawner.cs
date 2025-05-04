using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Waypoint spawnWaypoint;
    public float spawnInterval = 3f;

    private Waypoint[] exitWaypoints;

    void Start()
    {
        // Cache all exit waypoints (tagged "Exit")
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

        GameObject car = Instantiate(carPrefab, spawnWaypoint.transform.position, Quaternion.identity);
        CarController carScript = car.GetComponent<CarController>();

        // Choose the closest exit
        Waypoint closestExit = GetClosestExit(spawnWaypoint);

        // Generate the path using A*
        var path = Pathfinding.FindPath(spawnWaypoint, closestExit);
        carScript.path = path;
    }

    Waypoint GetClosestExit(Waypoint from)
    {
        Waypoint closest = null;
        float minDist = Mathf.Infinity;

        foreach (var exit in exitWaypoints)
        {
            if (exit == null) continue;

            float dist = Vector3.Distance(from.transform.position, exit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = exit;
            }
        }

        return closest;
    }
}


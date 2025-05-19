using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class WaypointValidator : EditorWindow
{
  private static List<Waypoint> isolatedWaypoints = new();

  [MenuItem("Tools/Waypoint Validator")]
  public static void ShowWindow()
  {
    GetWindow<WaypointValidator>("Waypoint Validator");
  }

  private void OnGUI()
  {
    if (GUILayout.Button("Validate Waypoints"))
    {
      ValidateWaypoints();
    }
  }

  private void OnEnable()
  {
    SceneView.duringSceneGui += OnSceneGUI;
  }

  private void OnDisable()
  {
    SceneView.duringSceneGui -= OnSceneGUI;
  }

  private void OnSceneGUI(SceneView sceneView)
  {
    Handles.color = Color.red;
    foreach (var wp in isolatedWaypoints)
    {
      if (wp == null) continue;
      Handles.DrawWireDisc(wp.transform.position, Vector3.forward, 0.5f);
    }
  }

  private static void ValidateWaypoints()
  {
    isolatedWaypoints.Clear();

    Waypoint[] allWaypoints = Object.FindObjectsByType<Waypoint>(FindObjectsSortMode.None);

    foreach (var wp in allWaypoints)
    {
      if (wp.CompareTag("Exit")) continue; // Skip exit nodes

      if (wp.connectedWaypoints == null || wp.connectedWaypoints.Count == 0)
      {
        isolatedWaypoints.Add(wp);
      }
    }

    if (isolatedWaypoints.Count == 0)
    {
      Debug.Log("<color=green>[Waypoint Validator]</color> All waypoints are connected ✅");
      EditorUtility.DisplayDialog("Waypoint Validator", "✅ All waypoints are connected!", "OK");
    }
    else
    {
      Debug.LogWarning($"<color=yellow>[Waypoint Validator]</color> Found {isolatedWaypoints.Count} unconnected waypoint(s):");
      foreach (var wp in isolatedWaypoints)
      {
        Debug.LogWarning($" - {wp.name}", wp.gameObject);
      }

      EditorUtility.DisplayDialog("Waypoint Validator", $"⚠ Found {isolatedWaypoints.Count} unconnected waypoint(s).\nSee Console and Scene view.", "OK");
    }

    SceneView.RepaintAll(); // Force gizmos to draw
  }
}

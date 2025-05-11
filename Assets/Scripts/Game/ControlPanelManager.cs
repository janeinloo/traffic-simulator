using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPanelManager : MonoBehaviour
{
  public TMP_InputField spawnIntervalInput;
  public Toggle toggleNorth;
  public Toggle toggleSouth;
  public Toggle toggleEast;
  public Toggle toggleWest;
  public Button applyButton;
  public Button resetButton;

  private Spawner[] allSpawners;
  private float defaultInterval = 3f;

  void Start()
  {
    allSpawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
    if (allSpawners.Length > 0)
      defaultInterval = allSpawners[0].spawnInterval;

    spawnIntervalInput.text = defaultInterval.ToString("0.0");

    applyButton.onClick.AddListener(ApplySettings);
    resetButton.onClick.AddListener(ResetSettings);
  }

  void ApplySettings()
  {
    float interval;
    if (!float.TryParse(spawnIntervalInput.text, out interval))
    {
      Debug.LogWarning("Invalid spawn interval. Using default.");
      interval = defaultInterval;
    }

    foreach (var spawner in allSpawners)
    {
      if (spawner == null || spawner.spawnWaypoint == null) continue;
      string name = spawner.spawnWaypoint.name.ToLower();

      bool isOn =
        (name.Contains("north") && toggleNorth.isOn) ||
        (name.Contains("south") && toggleSouth.isOn) ||
        (name.Contains("east") && toggleEast.isOn) ||
        (name.Contains("west") && toggleWest.isOn);

      spawner.gameObject.SetActive(isOn);
      spawner.SetSpawnInterval(interval);
    }
  }

  void ResetSettings()
  {
    spawnIntervalInput.text = defaultInterval.ToString("0.0");

    toggleNorth.isOn = true;
    toggleSouth.isOn = true;
    toggleEast.isOn = true;
    toggleWest.isOn = true;

    ApplySettings();
  }
}

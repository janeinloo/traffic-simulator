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

  private const float defaultInterval = 3f;

  void Start()
  {
    applyButton.onClick.AddListener(ApplySettings);
    resetButton.onClick.AddListener(ResetSettings);
    allSpawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
  }

  void ApplySettings()
  {
    float interval;
    if (!float.TryParse(spawnIntervalInput.text, out interval))
    {
      Debug.LogWarning("Invalid spawn interval input. Using default 3s.");
      interval = 3f;
    }

    foreach (var spawner in allSpawners)
    {
      if (spawner == null || spawner.spawnWaypoint == null) continue;

      string name = spawner.spawnWaypoint.name.ToLower();

      bool shouldBeActive =
        (name.Contains("north") && toggleNorth.isOn) ||
        (name.Contains("south") && toggleSouth.isOn) ||
        (name.Contains("east") && toggleEast.isOn) ||
        (name.Contains("west") && toggleWest.isOn);

      spawner.gameObject.SetActive(shouldBeActive);
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

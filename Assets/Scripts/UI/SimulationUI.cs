using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimulationUI : MonoBehaviour
{
  public Button playButton;
  public TextMeshProUGUI playButtonText;

  void Awake()
  {
    if (playButton == null)
      playButton = GetComponentInChildren<Button>();

    if (playButtonText == null)
      playButtonText = GetComponentInChildren<TextMeshProUGUI>();
  }

  void Start()
  {
    playButton.onClick.AddListener(ToggleSimulation);
    UpdateButtonText();
  }

  void UpdateButtonText()
  {
    playButtonText.text = SimulationManager.Instance.SimulationRunning ? "Peata" : "Käivita";
  }

  public void ToggleSimulation()
  {
    SimulationManager.Instance.ToggleSimulation();
    UpdateButtonText();

    if (SimulationManager.Instance.SimulationRunning)
    {
      var spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
      foreach (var spawner in spawners)
      {
        if (spawner.gameObject.activeInHierarchy)
          spawner.StartSpawning();
      }
    }
  }
}

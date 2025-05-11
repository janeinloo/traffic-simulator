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
    playButton.onClick.AddListener(() =>
    {
      SimulationManager.Instance.ToggleSimulation();
      UpdateButtonText();
    });

    UpdateButtonText();
  }

  void UpdateButtonText()
  {
    playButtonText.text = SimulationManager.Instance.SimulationRunning ? "Peata" : "Käivita";
  }
}

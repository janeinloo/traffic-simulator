using UnityEngine;

public class StopLineTrigger : MonoBehaviour
{
  private IntersectionManager manager;

  void Start()
  {
    manager = GetComponentInParent<IntersectionManager>();
    if (manager == null)
      Debug.LogWarning("StopLineTrigger: No IntersectionManager found in parent.");
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    CarController car = other.GetComponent<CarController>();
    if (car == null || manager == null) return;

    Direction entry = car.GetEntryDirection();
    Turn intent = car.GetTurnIntent();

    manager.RegisterCar(car.gameObject, entry, intent);
    car.BeginIntersectionWait(manager, entry, intent);
  }
}

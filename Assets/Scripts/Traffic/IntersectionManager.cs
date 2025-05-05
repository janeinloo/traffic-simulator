using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, East, South, West }
public enum Turn { Left, Right, Straight }

public class CarIntent
{
  public GameObject car;
  public Direction entryDirection;
  public Turn turn;
}

public class IntersectionManager : MonoBehaviour
{
  private List<CarIntent> carsInIntersection = new List<CarIntent>();

  public void RegisterCar(GameObject car, Direction from, Turn turn)
  {
    if (carsInIntersection.Exists(c => c.car == car)) return;

    carsInIntersection.Add(new CarIntent
    {
      car = car,
      entryDirection = from,
      turn = turn
    });
  }

  public void UnregisterCar(GameObject car)
  {
    carsInIntersection.RemoveAll(c => c.car == car);
  }

  public bool HasPriority(GameObject car, Direction from, Turn turn)
  {
    foreach (var other in carsInIntersection)
    {
      if (other.car == car) continue;

      if (IsToTheRightOf(from, other.entryDirection)) return false;

      if (turn == Turn.Left && other.turn != Turn.Left)
      {
        if (IsOpposite(from, other.entryDirection)) return false;
      }
    }

    return true;
  }

  private bool IsToTheRightOf(Direction me, Direction other)
  {
    return
    (me == Direction.North && other == Direction.East) ||
    (me == Direction.East && other == Direction.South) ||
    (me == Direction.South && other == Direction.West) ||
    (me == Direction.West && other == Direction.North);
  }

  private bool IsOpposite(Direction a, Direction b)
  {
    return
    (a == Direction.North && b == Direction.South) ||
    (a == Direction.South && b == Direction.North) ||
    (a == Direction.East && b == Direction.West) ||
    (a == Direction.West && b == Direction.East);
  }
}

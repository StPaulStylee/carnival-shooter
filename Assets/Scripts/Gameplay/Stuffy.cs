using CarnivalShooter.Gameplay.Behavior;
using UnityEngine;

public class Stuffy : MonoBehaviour {
  void Awake() {
    Scoreable.PointsScored += OnPointsScored;
  }

  private void OnDisable() {
    Scoreable.PointsScored -= OnPointsScored;
  }

  private void OnPointsScored(int score, string label, int eventId) {
    if (eventId == gameObject.GetInstanceID()) {
      Destroy(gameObject);
    }
  }
}

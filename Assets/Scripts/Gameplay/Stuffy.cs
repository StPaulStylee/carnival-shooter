using CarnivalShooter.Gameplay.Behavior;
using UnityEngine;

public class Stuffy : MonoBehaviour {
  void Awake() {
    Shootable.ShotHit += OnShotHit;
  }

  private void OnDisable() {
    Shootable.ShotHit -= OnShotHit;
  }

  private void OnShotHit(int eventId) {
    if (eventId == gameObject.GetInstanceID()) {
      Destroy(gameObject);
    }
  }
}

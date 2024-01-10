using CarnivalShooter.Gameplay.Behavior;
using UnityEngine;

public class Stuffy : MonoBehaviour {
  [SerializeField] private GameObject m_StuffyVisualGO;
  private CapsuleCollider m_CapsuleCollider;
  void Awake() {
    Shootable.ShotHit += OnShotHit;
    m_CapsuleCollider = GetComponent<CapsuleCollider>();
  }

  private void OnDisable() {
    Shootable.ShotHit -= OnShotHit;
  }

  private void OnShotHit(int eventId) {
    if (eventId == gameObject.GetInstanceID()) {
      m_StuffyVisualGO.SetActive(false);
      m_CapsuleCollider.enabled = false;
    }
  }
}

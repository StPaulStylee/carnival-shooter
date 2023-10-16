using UnityEngine;

namespace CarnivalShooter.Gameplay.Components {
  public class WeaponSway : MonoBehaviour {
    public Vector2 VelocityForSway { get; set; }
    [SerializeField] private float m_SwayMultiplier;
    [SerializeField] private float m_SwaySmoothSpeed;

    void Update() {
      SetWeaponSway();
    }

    private void SetWeaponSway() {
      Quaternion rotationX = Quaternion.AngleAxis(-VelocityForSway.y * m_SwayMultiplier, Vector3.right);
      Quaternion rotationY = Quaternion.AngleAxis(-VelocityForSway.x * m_SwayMultiplier, Vector3.up);
      Quaternion targetRotation = rotationX * rotationY;
      transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, m_SwaySmoothSpeed * Time.deltaTime);
    }
  }
}

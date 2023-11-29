using CarnivalShooter.Data;
using CarnivalShooter.Managers;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Components {
  public class WeaponSway : MonoBehaviour {
    public Vector2 VelocityForSway { get; set; }
    private bool m_IsWeaponLookInverted = false;
    [SerializeField] private float m_SwayMultiplier;
    [SerializeField] private float m_SwaySmoothSpeed;

    private void Awake() {
      SettingsManager.OnSettingsChanged += UpdateIsWeaponLookInverted;
    }

    private void OnDisable() {
      SettingsManager.OnSettingsChanged -= UpdateIsWeaponLookInverted;
    }

    private void Start() {
      m_IsWeaponLookInverted = SettingsManager.Instance.GetSettingsData().IsLookInverted;
    }

    void Update() {
      SetWeaponSway();
    }

    private void SetWeaponSway() {
      Vector2 velocityForSway = GetVelocityForSway(VelocityForSway);
      Quaternion rotationX = Quaternion.AngleAxis(velocityForSway.y * m_SwayMultiplier, Vector3.right);
      Quaternion rotationY = Quaternion.AngleAxis(velocityForSway.x * m_SwayMultiplier, Vector3.up);
      Quaternion targetRotation = rotationX * rotationY;
      transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, m_SwaySmoothSpeed * Time.deltaTime);
    }

    private void UpdateIsWeaponLookInverted(SettingsData data) {
      m_IsWeaponLookInverted = data.IsLookInverted;
    }

    private Vector2 GetVelocityForSway(Vector2 velocityForSway) {
      if (m_IsWeaponLookInverted) {
        return velocityForSway;
      }
      return new Vector2(-velocityForSway.x, -velocityForSway.y);
    }
  }
}

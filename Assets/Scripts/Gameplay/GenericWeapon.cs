using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Gameplay.Components;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class GenericWeapon : MonoBehaviour {
    public WeaponSway WeaponSway => m_WeaponSway;
    [Header("Child References")]
    [SerializeField] private WeaponSway m_WeaponSway;
    [SerializeField] private CurrentWeapon m_CurrentWeapon;
    private float m_fireTimer;
    private Camera m_povCamera;

    private void Start() {
      m_povCamera = Camera.main;
    }

    private void Update() {
      m_fireTimer += Time.deltaTime;
    }

    public void TryShoot() {
      if (CanShoot()) {
        Shoot();
      }
    }

    private bool CanShoot() {
      if (m_CurrentWeapon.IsReloading) {
        return false;
      }
      if (m_fireTimer >= m_CurrentWeapon.RefireRate && m_CurrentWeapon.RemainingAmmo > 0) {
        m_fireTimer = 0;
        return true;
      }
      if (m_CurrentWeapon.RemainingAmmo == 0) {
        m_CurrentWeapon.PlayMagEmptySfxClip();
      }
      return false;
    }

    private void Shoot() {
      m_CurrentWeapon.HandleRemainingAmmoDuringShot();
      m_CurrentWeapon.OnShoot();
      bool hasHit = Physics.Raycast(m_povCamera.transform.position, m_povCamera.transform.forward, out RaycastHit hit, m_CurrentWeapon.ShotDistance);
      if (hasHit && hit.transform.TryGetComponent(out Scoreable scoreable)) {
        hit.transform.GetComponent<Shootable>().TakeShot(hit);
        scoreable.OnPointsScored(hit.transform.position);
        return;
      }
    }

    public void Reload() {
      m_CurrentWeapon.OnReload();
    }

  }
}

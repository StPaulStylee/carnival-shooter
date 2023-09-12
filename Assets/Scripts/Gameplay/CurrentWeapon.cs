using CarnivalShooter.Managers;
using EZCameraShake;
using System;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class CurrentWeapon : MonoBehaviour {
    public static event Action AmmoChanged;
    public static event Action<int> AmmoReloaded;

    public Animator Animator => m_animator;
    public float RefireRate => m_refireRate;
    public bool IsReloading => m_isReloading;
    public int RemainingAmmo => m_remainingAmmo;
    public float ShotDistance => m_shotDistance;
    [Header("Weapon Configuration")]
    public GenericWeapon GenericWeapon;
    [SerializeField] private float m_refireRate = 0.2f;
    [SerializeField] private float m_shotDistance = 10f;
    [SerializeField] private int m_startingAmmo = 30;
    [Tooltip("The intensity of the Camera Shake when shooting")]
    [SerializeField] private float m_ShotShakeMagnitude = 4f;
    [Tooltip("The roughness of the Camera Shake when shooting. Smaller is smoother")]
    [SerializeField] private float m_ShotShakeRoughness = 4f;
    [SerializeField] private float m_ShotShakeFadeInTime = 0.1f;
    [SerializeField] private float m_ShotShakeFadeOutTime = 0.1f;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem m_shotParticle;
    [SerializeField] private ParticleSystem m_bulletEjectionParticle;
    [Header("Audio Configuration")]
    [SerializeField] private AudioSource m_weaponSfx;
    [SerializeField] private AudioClip m_shotSfxClip;
    [SerializeField] private AudioClip m_magEmptySfxClip;
    [SerializeField] private AudioClip m_reloadMagOutSfxClip;
    [SerializeField] private AudioClip m_reloadMagInSfxClip;
    [SerializeField] private AudioClip m_reloadSlideInSfxClip;
    [Header("Animation/VFX")]
    [SerializeField] private Animator m_animator;

    private int m_remainingAmmo;
    private bool m_isReloading;
    private void Awake() {
      GameManager.AmmoInitializing += SetStartingAmmo;
    }

    private void OnDisable() {
      GameManager.AmmoInitializing -= SetStartingAmmo;
    }

    private void Reload() {
      SetIsReloading(true);
      m_animator.SetBool("IsEmpty", false);
      m_remainingAmmo = m_startingAmmo;
      m_animator.SetTrigger("Reload");
      AmmoReloaded?.Invoke(m_startingAmmo);
    }

    public void OnReload() {
      Reload();
    }

    private void SetIsReloading(bool isReloading) {
      m_isReloading = isReloading;
    }

    private void SetStartingAmmo(int amount) {
      m_startingAmmo = amount;
      m_remainingAmmo = amount;
    }

    public void OnShoot() {
      m_shotParticle.Play();
      m_weaponSfx.PlayOneShot(m_shotSfxClip, 0.8f);
      m_animator.SetTrigger("Shoot");
      CameraShaker.Instance.ShakeOnce(m_ShotShakeMagnitude, m_ShotShakeRoughness, m_ShotShakeFadeInTime, m_ShotShakeFadeOutTime);
      AmmoChanged?.Invoke(); // Should this event be moved to GenericWeapon?
    }

    public void PlayMagEmptySfxClip() {
      m_weaponSfx.PlayOneShot(m_magEmptySfxClip, 0.8f);
    }

    public void HandleRemainingAmmoDuringShot() {
      m_remainingAmmo--;
      if (m_remainingAmmo == 0) {
        m_animator.SetBool("IsEmpty", true);
      }
    }

    // Animation Events
    private void PlayBulletEjection() {
      m_bulletEjectionParticle.Play();
    }

    private void PlayReloadMagOutSfx() {
      m_weaponSfx.PlayOneShot(m_reloadMagOutSfxClip);
    }

    private void PlayReloadMagInSfx() {
      m_weaponSfx.PlayOneShot(m_reloadMagInSfxClip);
    }

    private void PlayReloadSlideInSfx() {
      m_weaponSfx.PlayOneShot(m_reloadSlideInSfxClip, 0.8f);
    }

    private void SetIsReloadingToFalse() {
      SetIsReloading(false);
    }
  }
}

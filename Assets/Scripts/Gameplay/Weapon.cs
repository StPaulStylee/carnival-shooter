using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class Weapon : MonoBehaviour {
    public static event Action<int> AmmoChanged;
    [Header("Weapon Configuration")]
    [SerializeField] private float m_refireRate = 0.2f;
    [SerializeField] private float m_shotDistance = 10f;
    [SerializeField] private int m_startingAmmo = 30;
    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem m_shotParticle;
    [SerializeField] private ParticleSystem m_bulletEjectionParticle;
    [Header("Audio Configuration")]
    [SerializeField] private AudioSource m_weaponSfx;
    [SerializeField] private AudioClip m_shotSfxClip;
    [SerializeField] private AudioClip m_reloadMagOutSfxClip;
    [SerializeField] private AudioClip m_reloadMagInSfxClip;
    [SerializeField] private AudioClip m_reloadSlideInSfxClip;
    [SerializeField] private Animator m_animator;
    private float m_fireTimer;
    private Camera m_povCamera;
    private int m_remainingAmmo;

    private void Awake() {
      m_povCamera = Camera.main;
      GameManager.AmmoInitializing += SetStartingAmmo;
    }


    private void Update() {
      m_fireTimer += Time.deltaTime;

    }

    public void TryShoot() {
      if (CanShoot()) {
        Shoot();
      }
    }

    public void Reload() {
      m_remainingAmmo = m_startingAmmo;
      //m_shotSfx.PlayOneShot(m_reloadSfxClip, 0.8f);
      m_animator.SetTrigger("Reload");
      AmmoChanged?.Invoke(m_remainingAmmo);
    }

    private void SetStartingAmmo(int amount) {
      m_startingAmmo = amount;
      m_remainingAmmo = amount;
    }

    private void Shoot() {
      m_remainingAmmo--;
      m_shotParticle.Play();
      m_weaponSfx.PlayOneShot(m_shotSfxClip, 0.8f);
      m_animator.SetTrigger("Shoot");
      AmmoChanged?.Invoke(m_remainingAmmo);
      bool hasHit = Physics.Raycast(m_povCamera.transform.position, m_povCamera.transform.forward, out RaycastHit hit, m_shotDistance);
      if (hasHit && hit.transform.TryGetComponent(out Scoreable scoreable)) {
        hit.transform.parent.GetComponent<ChangeMaterial>().SwapMaterial(); // TODO: Remove this when no longer necessary
        hit.transform.GetComponent<Shootable>().TakeShot(hit);
        scoreable.OnPointsScored();
        return;
      }
    }


    private bool CanShoot() {
      if (m_fireTimer >= m_refireRate && m_remainingAmmo > 0) {
        m_fireTimer = 0;
        return true;
      }
      return false;
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
  }
}

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
    [SerializeField] private int m_totalAmmo = 30;
    [SerializeField] private ParticleSystem m_shotParticle;
    [SerializeField] private ParticleSystem m_bulletEjectionParticle;
    [SerializeField] private Animator m_animator;
    [SerializeField] private AudioSource m_shotSfx;
    [SerializeField] private AudioClip m_shotSfxClip;
    private float m_fireTimer;
    private Camera m_povCamera;

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

    private void SetStartingAmmo(int ammount) {
      m_totalAmmo = ammount;
    }

    private void Shoot() {
      m_totalAmmo--;
      m_shotParticle.Play();
      m_shotSfx.PlayOneShot(m_shotSfxClip, 0.8f);
      m_animator.SetTrigger("Shoot");
      AmmoChanged?.Invoke(m_totalAmmo);
      bool hasHit = Physics.Raycast(m_povCamera.transform.position, m_povCamera.transform.forward, out RaycastHit hit, m_shotDistance);
      if (hasHit && hit.transform.TryGetComponent(out Scoreable scoreable)) {
        hit.transform.GetComponent<Shootable>().TakeShot(hit);
        scoreable.OnPointsScored();
        return;
      }
    }

    private bool CanShoot() {
      if (m_fireTimer >= m_refireRate && m_totalAmmo > 0) {
        m_fireTimer = 0;
        return true;
      }
      return false;
    }

    // Animation Events
    private void PlayBulletEjection() {
      m_bulletEjectionParticle.Play();
    }
  }
}

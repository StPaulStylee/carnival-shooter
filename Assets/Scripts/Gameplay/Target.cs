using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public abstract class Target : MonoBehaviour {
    public static event Action<int> PointsScored;

    [SerializeField] private Transform m_hitEffectPrefab;
    [SerializeField] private AudioClip[] m_hitSfxClips;
    [SerializeField] private AudioSource m_hitSfx;
    private Animator m_animator;

    private void Awake() {
      m_animator = GetComponentInParent<Animator>();
      if (m_animator == null) {
        Debug.LogError($"No Animator component found in {name}'s parent");
      }
    }
    public virtual void TakeShot(RaycastHit hitInfo) {
      Transform hitEffect = Instantiate(m_hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), transform.parent);
      hitEffect.GetComponent<ParticleSystem>().Play();
      AudioClip audioClip = m_hitSfxClips[UnityEngine.Random.Range(0, m_hitSfxClips.Length)];
      m_hitSfx.PlayOneShot(audioClip, 1f);
      m_animator.SetTrigger("Hit");
      Destroy(hitEffect.gameObject, 5f);
    }

    protected void OnPointsScored(int points) {
      PointsScored?.Invoke(points);
    }
  }
}
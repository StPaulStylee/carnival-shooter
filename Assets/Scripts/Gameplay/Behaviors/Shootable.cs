using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(AudioSource))]
  public class Shootable : MonoBehaviour {
    //public static event Action<int> PointsScored;

    [SerializeField] private Transform m_hitEffectPrefab;
    [SerializeField] private AudioClip[] m_hitSfxClips;
    [SerializeField] private AudioSource m_hitSfx;
    private ShotAnimatable shotAnimatable;

    private void Awake() {
      shotAnimatable = GetComponentInParent<ShotAnimatable>();
      if (shotAnimatable == null) {
        Debug.LogError($"No Animatable component found in {name}'s parent");
      }
    }
    public virtual void TakeShot(RaycastHit hitInfo) {
      Transform hitEffect = Instantiate(m_hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), transform.parent);
      hitEffect.GetComponent<ParticleSystem>().Play();
      AudioClip audioClip = m_hitSfxClips[UnityEngine.Random.Range(0, m_hitSfxClips.Length)];
      m_hitSfx.PlayOneShot(audioClip, 1f);
      shotAnimatable.PlayTakeShot();
      Destroy(hitEffect.gameObject, 5f);
    }
  }
}
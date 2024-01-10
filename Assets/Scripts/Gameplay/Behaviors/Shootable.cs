using CarnivalShooter.Gameplay.Audio;
using CarnivalShooter.Managers.Data;
using System;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(AudioSource))]
  public class Shootable : AudioCore {
    public static event Action<int> ShotHit;

    [SerializeField] private Transform m_hitEffectPrefab;
    [SerializeField] private AudioClip[] m_hitSfxClips;
    [SerializeField] private AudioSource m_hitSfxSource;
    private int m_Id;
    private ShotAnimatable shotAnimatable;
    [SerializeField] private float m_hitSfxVolume = 1.0f;
    protected override void Awake() {
      base.Awake();
      m_Id = gameObject.GetInstanceID();
      shotAnimatable = GetComponentInParent<ShotAnimatable>();
      if (shotAnimatable == null) {
        Debug.LogWarning($"No Animatable component found in {name}'s parent");
      }
    }
    public virtual void TakeShot(RaycastHit hitInfo) {
      Transform hitEffect = Instantiate(m_hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), transform.parent);
      hitEffect.GetComponent<ParticleSystem>().Play();
      AudioClip audioClip = m_hitSfxClips[UnityEngine.Random.Range(0, m_hitSfxClips.Length)];
      m_hitSfxSource.PlayOneShot(audioClip, m_hitSfxVolume);
      if (shotAnimatable != null) shotAnimatable.PlayTakeShot();
      ShotHit?.Invoke(m_Id);
      Destroy(hitEffect.gameObject, 5f);
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_hitSfxVolume = data.GameplaySfxVolume;
    }
  }
}
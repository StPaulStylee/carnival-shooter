using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(AudioSource))]
  public class Shootable : MonoBehaviour {
    [SerializeField] private Transform m_hitEffectPrefab;
    [SerializeField] private AudioClip[] m_hitSfxClips;
    [SerializeField] private AudioSource m_hitSfx;
    private ShotAnimatable shotAnimatable;
    [SerializeField] private float m_hitSfxVolume = 1.0f;
    private void Awake() {
      AudioManager.AudioSettingsChanged += SetSfxVolume;
      shotAnimatable = GetComponentInParent<ShotAnimatable>();
      if (shotAnimatable == null) {
        Debug.LogError($"No Animatable component found in {name}'s parent");
      }
    }
    public virtual void TakeShot(RaycastHit hitInfo) {
      Transform hitEffect = Instantiate(m_hitEffectPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal), transform.parent);
      hitEffect.GetComponent<ParticleSystem>().Play();
      AudioClip audioClip = m_hitSfxClips[UnityEngine.Random.Range(0, m_hitSfxClips.Length)];
      m_hitSfx.PlayOneShot(audioClip, m_hitSfxVolume);
      shotAnimatable.PlayTakeShot();
      Destroy(hitEffect.gameObject, 5f);
    }

    private void SetSfxVolume(AudioSettingsData data) {
      m_hitSfxVolume = data.GameplaySfxVolume;
    }
  }
}
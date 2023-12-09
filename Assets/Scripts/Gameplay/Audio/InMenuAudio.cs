using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Audio {
  public class InMenuAudio : AudioCore {
    [SerializeField] private AudioSource m_MusicAudioSource;
    private float m_MusicSfxVolume;

    private void Start() {
      m_MusicAudioSource.Play();
    }

    private void PlayAudio() {
      if (m_MusicAudioSource != null) {
        m_MusicAudioSource.Play();
      }
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_MusicSfxVolume = data.MusicSfxVolume;
      m_MusicAudioSource.volume = m_MusicSfxVolume;
    }
  }
}

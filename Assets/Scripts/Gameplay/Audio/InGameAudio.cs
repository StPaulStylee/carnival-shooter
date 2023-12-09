using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Audio {
  public class InGameAudio : AudioCore {
    [SerializeField] private AudioSource m_MusicAudioSource;
    [SerializeField] private AudioSource m_BackgroundAudioSource;
    private float m_MusicSfxVolume;
    private float m_BackgroundSfxVolume;

    private void Start() {
      m_MusicAudioSource.Play();
      m_BackgroundAudioSource.Play();
    }

    private void PlayAudio() {
      if (m_MusicAudioSource != null) {
        m_MusicAudioSource.Play();
      }
      if (m_BackgroundAudioSource != null) {
        m_BackgroundAudioSource.Play();
      }
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_MusicSfxVolume = data.MusicSfxVolume;
      m_BackgroundSfxVolume = data.BackgroundSfxVolume;
      m_MusicAudioSource.volume = m_MusicSfxVolume;
      m_BackgroundAudioSource.volume = m_BackgroundSfxVolume;
    }
  }
}

using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Audio {
  public class BackgroundAudio : MonoBehaviour {
    [SerializeField] private AudioSource m_MusicAudioSource;
    [SerializeField] private AudioSource m_BackgroundAudioSource;
    private float m_MusicSfxVolume;
    private float m_BackgroundSfxVolume;

    private void Awake() {
      AudioManager.AudioSettingsChanged += SetSfxVolume;
    }

    private void OnDisable() {
      AudioManager.AudioSettingsChanged -= SetSfxVolume;
    }

    private void Start() {
      Debug.Log("Playing Audio");
      m_MusicAudioSource.Play();
      m_BackgroundAudioSource.Play();
      //m_AudioSource.PlayOneShot(m_MusicClip, m_MusicSfxVolume);
      //m_AudioSource.PlayOneShot(m_BackgroundClip, m_BackgroundSfxVolume);
    }

    private void SetSfxVolume(AudioSettingsData data) {
      m_MusicSfxVolume = data.MusicSfxVolume;
      m_BackgroundSfxVolume = data.BackgroundSfxVolume;
      m_MusicAudioSource.volume = m_MusicSfxVolume;
      m_BackgroundAudioSource.volume = m_BackgroundSfxVolume;
    }
  }
}

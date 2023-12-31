using CarnivalShooter.Data;
using CarnivalShooter.Managers.Data;
using System;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class AudioManager : MonoBehaviour {
    public static event Action<AudioSettingsData> AudioSettingsChanged;
    private const float VOLUME_DIVISOR = 100f;
    private bool m_IsAllAudioEnabled;
    private bool m_IsLookInversionAudioEnabled;
    [Range(0, 1f)] private float m_GameSfxVolume;
    [Range(0, 1f)] private float m_BackgroundSfxVolume;
    [Range(0, 1f)] private float m_MusicSfxVolume;
    [Range(0, 1f)] private float m_UiSfxVolume;
    private AudioSettingsData m_AudioSettings;
    private void Awake() {
      SettingsManager.OnSettingsChanged += SetAudioData;
    }

    private void OnDisable() {
      SettingsManager.OnSettingsChanged -= SetAudioData;
    }

    private void SetAudioData(SettingsData data) {
      m_IsAllAudioEnabled = data.IsAudioEnabled;
      m_IsLookInversionAudioEnabled = data.IsLookInverted;
      m_GameSfxVolume = m_IsAllAudioEnabled ? data.GameplaySfxVolume / VOLUME_DIVISOR : 0f;
      m_BackgroundSfxVolume = m_IsAllAudioEnabled ? data.BackgroundSfxVolume / VOLUME_DIVISOR : 0f;
      m_MusicSfxVolume = m_IsAllAudioEnabled ? data.MusicSfxVolume / VOLUME_DIVISOR : 0f;
      m_UiSfxVolume = m_IsAllAudioEnabled ? data.UiSfxVolume / VOLUME_DIVISOR : 0f;
      m_AudioSettings = new AudioSettingsData(m_IsAllAudioEnabled, m_IsLookInversionAudioEnabled, m_GameSfxVolume, m_MusicSfxVolume, m_BackgroundSfxVolume, m_UiSfxVolume);
      AudioSettingsChanged?.Invoke(m_AudioSettings);
    }
  }
}

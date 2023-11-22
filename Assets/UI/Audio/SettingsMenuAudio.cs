using CarnivalShooter.Data;
using CarnivalShooter.Gameplay.Audio;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.UI.Audio {
  public class SettingsMenuAudio : AudioCore {
    private AudioSource m_AudioSource;
    private float m_AudioVolume;
    private AudioSettingsData m_AudioSettingsData;
    [SerializeField] private AudioClip m_ButtonClickAudioClip;
    [SerializeField] private AudioClip m_ToggleEnabledAudioClip;
    [SerializeField] private AudioClip m_ToggleDisabledAudioClip;
    [SerializeField] private AudioClip m_BackButtonAudioClip;

    protected override void Awake() {
      base.Awake();
      SettingsMenu.SettingValueClicked += PlaySettingChanged;
      SettingsMenu.BackButtonClicked += PlayBackButtonClicked;
      m_AudioSource = GetComponent<AudioSource>();
    }

    protected override void OnDisable() {
      base.OnDisable();
      SettingsMenu.SettingValueClicked -= PlaySettingChanged;
      SettingsMenu.BackButtonClicked -= PlayBackButtonClicked;
    }

    private void PlayBackButtonClicked() {
      m_AudioSource.PlayOneShot(m_BackButtonAudioClip, m_AudioVolume);
    }

    private void PlaySettingChanged(SettingsMenuAction action, SettingsMenuType type) {
      if (action == SettingsMenuAction.DECREMENT || action == SettingsMenuAction.INCREMENT) {
        m_AudioSource.PlayOneShot(m_ButtonClickAudioClip, m_AudioVolume);
        return;
      }
      if (action == SettingsMenuAction.TOGGLE && type == SettingsMenuType.AUDIO_ENABLE) {
        if (m_AudioSettingsData.IsAllAudioEnabled) {
          m_AudioSource.PlayOneShot(m_ToggleEnabledAudioClip, m_AudioVolume);
        } else {
          m_AudioSource.PlayOneShot(m_ToggleDisabledAudioClip, m_AudioVolume);
        }
        return;
      }
      if (action == SettingsMenuAction.TOGGLE && type == SettingsMenuType.LOOK_INVERSION) {
        if (m_AudioSettingsData.IsLookInversionAudioEnabled) {
          m_AudioSource.PlayOneShot(m_ToggleEnabledAudioClip, m_AudioVolume);
        } else {
          m_AudioSource.PlayOneShot(m_ToggleDisabledAudioClip, m_AudioVolume);
        }
        return;
      }
      Debug.LogWarning($"PlaySettingChanged in {this.name} is being a called with data that has not been configured.");
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_AudioSettingsData = data;
      m_AudioVolume = data.UiSfxVolume;
    }
  }
}

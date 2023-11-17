using CarnivalShooter.Data;
using CarnivalShooter.Gameplay.Audio;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.UI.Audio {
  public class SettingsMenuAudio : AudioCore {
    private AudioSource m_AudioSource;
    private float m_AudioVolume;
    [SerializeField] private AudioClip m_ButtonClickAudioClip;
    [SerializeField] private AudioClip m_ToggleAudioClip;
    protected override void Awake() {
      base.Awake();
      SettingsMenu.SettingValueClicked += PlaySettingChanged;
      m_AudioSource = GetComponent<AudioSource>();
    }

    private void PlaySettingChanged(SettingsMenuAction action, SettingsMenuType type) {
      if (action == SettingsMenuAction.DECREMENT || action == SettingsMenuAction.INCREMENT) {
        m_AudioSource.PlayOneShot(m_ButtonClickAudioClip, m_AudioVolume);
        return;
      }
      m_AudioSource.PlayOneShot(m_ToggleAudioClip, m_AudioVolume);
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_AudioVolume = data.UiSfxVolume;
    }
  }
}

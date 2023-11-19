using CarnivalShooter.Gameplay.Audio;
using CarnivalShooter.Managers.Data;
using CarnivalShooter.UI;
using UnityEngine;

public class PauseMenuAudio : AudioCore {
  private float m_AudioVolume;
  private AudioSource m_AudioSource;
  [SerializeField] private AudioClip m_OptionClickedAudioClip;
  protected override void Awake() {
    base.Awake();
    m_AudioSource = GetComponent<AudioSource>();
    PauseMenu.OnReturnToGame += PlayPauseMenuOptionClicked;
    PauseMenu.SettingsMenuOpened += PlayPauseMenuOptionClicked;
  }

  private void PlayPauseMenuOptionClicked() {
    m_AudioSource.PlayOneShot(m_OptionClickedAudioClip, m_AudioVolume);
  }

  protected override void SetSfxVolume(AudioSettingsData data) {
    m_AudioVolume = data.UiSfxVolume;
  }
}

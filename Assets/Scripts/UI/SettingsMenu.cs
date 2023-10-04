using CarnivalShooter.Data;
using CarnivalShooter.Data.ScriptableObjects;
using CarnivalShooter.Managers;
using CarnivalShooter.UI.CustomControls;
using System;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class SettingsMenu : GameUIScreen {
    public static event Action BackButtonClicked;
    public static event Action<SettingsMenuAction, SettingsMenuType> SettingValueClicked;

    const string k_BackButton = "settings-menu--back-button";
    const string k_DecrementGameplaySfx = "settings-menu__gameplay-sfx--decrement";
    const string k_IncrementGameplaySfx = "settings-menu__gameplay-sfx--increment";
    const string k_DecrementMusicSfx = "settings-menu__music--decrement";
    const string k_IncrementMusicSfx = "settings-menu__music--increment";
    const string k_DecrementBackgroundSfx = "settings-menu__background-sfx--decrement";
    const string k_IncrementBackgroundSfx = "settings-menu__background-sfx--increment";
    const string k_AudioEnabledValue = "MenuToggle";
    const string k_GameplaySfxValue = "settings-menu__gameplay-sfx--value";
    const string k_MusicSfxValue = "settings-menu__music-sfx--value";
    const string k_BackgroundSfxValue = "settings-menu__background-sfx--value";

    private VisualElement m_BackButton, m_DecrementGameplaySfxBtn, m_IncrementGameplaySfxBtn, m_DecrementMusicSfxBtn, m_IncrementMusicSfxBtn, m_DecrementBackgroundSfxBtn, m_IncrementBackgroundSfxBtn;
    private Label m_GameplaySfxValue, m_MusicSfxValue, m_BackgroundSfxValue;
    private MenuToggle m_AudioEnabledValue;
    private void Awake() {
      SettingsManager.OnSettingsChanged += SetValues;
    }

    private void OnDisable() {
      SettingsManager.OnSettingsChanged -= SetValues;
    }

    private void OnEnable() {
      base.SetGameUIElements();
      m_BackButton = m_GameUIElement.Q(k_BackButton);
      m_DecrementGameplaySfxBtn = m_GameUIElement.Q(k_DecrementGameplaySfx);
      m_IncrementGameplaySfxBtn = m_GameUIElement.Q(k_IncrementGameplaySfx);
      m_DecrementMusicSfxBtn = m_GameUIElement.Q(k_DecrementMusicSfx);
      m_IncrementMusicSfxBtn = m_GameUIElement.Q(k_IncrementMusicSfx);
      m_DecrementBackgroundSfxBtn = m_GameUIElement.Q(k_DecrementBackgroundSfx);
      m_IncrementBackgroundSfxBtn = m_GameUIElement.Q(k_IncrementBackgroundSfx);
      m_AudioEnabledValue = m_GameUIElement.Q<MenuToggle>(k_AudioEnabledValue);

      m_GameplaySfxValue = m_GameUIElement.Q<Label>(k_GameplaySfxValue);
      m_MusicSfxValue = m_GameUIElement.Q<Label>(k_MusicSfxValue);
      m_BackgroundSfxValue = m_GameUIElement.Q<Label>(k_BackgroundSfxValue);

      m_BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
      m_DecrementGameplaySfxBtn.RegisterCallback<ClickEvent>(OnDecrementGameplaySfx);
      m_IncrementGameplaySfxBtn.RegisterCallback<ClickEvent>(OnIncrementGameplaySfx);
      m_DecrementMusicSfxBtn.RegisterCallback<ClickEvent>(OnDecrementMusicSfx);
      m_IncrementMusicSfxBtn.RegisterCallback<ClickEvent>(OnIncrementMusicSfx);
      m_DecrementBackgroundSfxBtn.RegisterCallback<ClickEvent>(OnDecrementBackgroundSfx);
      m_IncrementBackgroundSfxBtn.RegisterCallback<ClickEvent>(OIncrementBackgroundSfx);
      m_AudioEnabledValue.RegisterCallback<ClickEvent>(OnAudioEnabledToggle);
    }

    private void SetValues(Settings_SO values) {
      m_AudioEnabledValue.SetValueWithoutNotify(values.IsAudioEnabled);
      m_GameplaySfxValue.text = $"{values.GameplaySfxVolume}%";
      m_MusicSfxValue.text = $"{values.MusicSfxVolume}%";
      m_BackgroundSfxValue.text = $"{values.BackgroundSfxVolume}%";
    }

    private void OnBackButtonClicked(ClickEvent e) {
      BackButtonClicked?.Invoke();
    }

    private void OnDecrementGameplaySfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.DECREMENT, SettingsMenuType.GAMEPLAY_SFX);
    }

    private void OnIncrementGameplaySfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.INCREMENT, SettingsMenuType.GAMEPLAY_SFX);
    }

    private void OnDecrementMusicSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.DECREMENT, SettingsMenuType.MUSIC_SFX);
    }

    private void OnIncrementMusicSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.INCREMENT, SettingsMenuType.MUSIC_SFX);
    }

    private void OnDecrementBackgroundSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.DECREMENT, SettingsMenuType.BACKGROUND_SFX);
    }

    private void OIncrementBackgroundSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.INCREMENT, SettingsMenuType.BACKGROUND_SFX);
    }

    private void OnAudioEnabledToggle(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.TOGGLE, SettingsMenuType.AUDIO_ENABLE);
    }
  }
}

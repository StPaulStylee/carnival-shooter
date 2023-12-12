using CarnivalShooter.Data;
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
    const string k_DecrementUiSfx = "settings-menu__ui--decrement";
    const string k_IncrementUiSfx = "settings-menu__ui--increment";
    const string k_DecrementBackgroundSfx = "settings-menu__background-sfx--decrement";
    const string k_IncrementBackgroundSfx = "settings-menu__background-sfx--increment";
    const string k_DecrementLookSensitivity = "settings-menu__look-sensitivity--decrement";
    const string k_IncrementLookSensitivity = "settings-menu__look-sensitivity--increment";
    const string k_AudioEnabledValue = "AllSoundsToggle";
    const string k_GameplaySfxValue = "settings-menu__gameplay-sfx--value";
    const string k_MusicSfxValue = "settings-menu__music-sfx--value";
    const string k_UiSfxValue = "settings-menu__ui-sfx--value";
    const string k_BackgroundSfxValue = "settings-menu__background-sfx--value";
    const string k_LookSensitivityValue = "settings-menu__look-sensitivity--value";
    const string k_LookInversionValue = "LookInversionToggle";

    private VisualElement m_BackButton, m_DecrementGameplaySfxBtn, m_IncrementGameplaySfxBtn, m_DecrementMusicSfxBtn, m_IncrementMusicSfxBtn, m_DecrementUiSfxBtn, m_IncrementUiSfxBtn, m_DecrementBackgroundSfxBtn, m_IncrementBackgroundSfxBtn, m_IncrementLookSensitivityBtn, m_DecrementLookSensitivityBtn;
    private Label m_GameplaySfxValue, m_MusicSfxValue, m_UiSfxValue, m_BackgroundSfxValue, m_LookSensitivityValue;
    private MenuToggle m_AudioEnabledValue, m_LookInversionValue;

    private SettingsData m_settingsData;

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
      m_DecrementUiSfxBtn = m_GameUIElement.Q(k_DecrementUiSfx);
      m_IncrementUiSfxBtn = m_GameUIElement.Q(k_IncrementUiSfx);
      m_DecrementBackgroundSfxBtn = m_GameUIElement.Q(k_DecrementBackgroundSfx);
      m_IncrementBackgroundSfxBtn = m_GameUIElement.Q(k_IncrementBackgroundSfx);
      m_DecrementLookSensitivityBtn = m_GameUIElement.Q(k_DecrementLookSensitivity);
      m_IncrementLookSensitivityBtn = m_GameUIElement.Q(k_IncrementLookSensitivity);
      m_AudioEnabledValue = m_GameUIElement.Q<MenuToggle>(k_AudioEnabledValue);
      m_LookInversionValue = m_GameUIElement.Q<MenuToggle>(k_LookInversionValue);
      m_GameplaySfxValue = m_GameUIElement.Q<Label>(k_GameplaySfxValue);
      m_MusicSfxValue = m_GameUIElement.Q<Label>(k_MusicSfxValue);
      m_UiSfxValue = m_GameUIElement.Q<Label>(k_UiSfxValue);
      m_BackgroundSfxValue = m_GameUIElement.Q<Label>(k_BackgroundSfxValue);
      m_LookSensitivityValue = m_GameUIElement.Q<Label>(k_LookSensitivityValue);

      m_BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
      m_DecrementGameplaySfxBtn.RegisterCallback<ClickEvent>(OnDecrementGameplaySfx);
      m_IncrementGameplaySfxBtn.RegisterCallback<ClickEvent>(OnIncrementGameplaySfx);
      m_DecrementMusicSfxBtn.RegisterCallback<ClickEvent>(OnDecrementMusicSfx);
      m_IncrementMusicSfxBtn.RegisterCallback<ClickEvent>(OnIncrementMusicSfx);
      m_DecrementUiSfxBtn.RegisterCallback<ClickEvent>(OnDecrementUiSfx);
      m_IncrementUiSfxBtn.RegisterCallback<ClickEvent>(OnIncrementUiSfx);
      m_DecrementBackgroundSfxBtn.RegisterCallback<ClickEvent>(OnDecrementBackgroundSfx);
      m_IncrementBackgroundSfxBtn.RegisterCallback<ClickEvent>(OIncrementBackgroundSfx);
      m_AudioEnabledValue.RegisterCallback<ClickEvent>(OnAudioEnabledToggle);
      m_LookInversionValue.RegisterCallback<ClickEvent>(OnLookInversionEnabledToggle);
      m_DecrementLookSensitivityBtn.RegisterCallback<ClickEvent>(OnDecrementLookSensitivity);
      m_IncrementLookSensitivityBtn.RegisterCallback<ClickEvent>(OnIncrementLookSensitivity);
      SettingsManager.OnSettingsChanged += SetValues;
    }

    private void SetValues(SettingsData values) {
      m_settingsData = values;
      m_AudioEnabledValue.SetValueWithoutNotify(values.IsAudioEnabled);
      m_LookInversionValue.SetValueWithoutNotify(values.IsLookInverted);
      m_GameplaySfxValue.text = $"{values.GameplaySfxVolume}%";
      m_MusicSfxValue.text = $"{values.MusicSfxVolume}%";
      m_UiSfxValue.text = $"{values.UiSfxVolume}%";
      m_BackgroundSfxValue.text = $"{values.BackgroundSfxVolume}%";
      m_LookSensitivityValue.text = $"{values.LookSensitivity}";
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

    private void OnDecrementUiSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.DECREMENT, SettingsMenuType.UI_SFX);
    }

    private void OnIncrementUiSfx(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.INCREMENT, SettingsMenuType.UI_SFX);
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

    private void OnLookInversionEnabledToggle(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.TOGGLE, SettingsMenuType.LOOK_INVERSION);
    }

    private void OnIncrementLookSensitivity(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.INCREMENT, SettingsMenuType.LOOK_SENSITIVITY);
    }

    private void OnDecrementLookSensitivity(ClickEvent e) {
      SettingValueClicked?.Invoke(SettingsMenuAction.DECREMENT, SettingsMenuType.LOOK_SENSITIVITY);
    }
  }
}

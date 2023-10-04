using CarnivalShooter.Data;
using CarnivalShooter.Data.ScriptableObjects;
using CarnivalShooter.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class SettingsManager : MonoBehaviour {
    [SerializeField] private Settings_SO m_SettingsData;
    private Dictionary<SettingsMenuType, Action<SettingsMenuAction, Settings_SO>> m_MenuActions;

    public static event Action<Settings_SO> OnSettingsChanged;

    private void Awake() {
      SettingsMenu.SettingValueClicked += SetSettingsData;
      m_MenuActions = new Dictionary<SettingsMenuType, Action<SettingsMenuAction, Settings_SO>> {
        {SettingsMenuType.GAMEPLAY_SFX, OnGameplaySfxChange },
        {SettingsMenuType.MUSIC_SFX, OnMusicSfxChange },
        {SettingsMenuType.BACKGROUND_SFX, OnBackgroundSfxChange },
        {SettingsMenuType.AUDIO_ENABLE, OnAudioEnabledToggle },
      };
    }

    private void OnDisable() {
      SettingsMenu.SettingValueClicked -= SetSettingsData;
    }

    private void Start() {
      OnSettingsChanged?.Invoke(m_SettingsData);
    }

    private void SetSettingsData(SettingsMenuAction action, SettingsMenuType type) {
      SetSettingsData(action, type, m_SettingsData);
    }

    private void SetSettingsData(SettingsMenuAction settingsAction, SettingsMenuType settingsType, Settings_SO data) {
      if (m_MenuActions.TryGetValue(settingsType, out Action<SettingsMenuAction, Settings_SO> action)) {
        action(settingsAction, data);
      }
    }

    private void OnGameplaySfxChange(SettingsMenuAction action, Settings_SO data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValue(ref data.GameplaySfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValue(ref data.GameplaySfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnMusicSfxChange(SettingsMenuAction action, Settings_SO data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValue(ref data.MusicSfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValue(ref data.MusicSfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnBackgroundSfxChange(SettingsMenuAction action, Settings_SO data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValue(ref data.BackgroundSfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValue(ref data.BackgroundSfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnAudioEnabledToggle(SettingsMenuAction action, Settings_SO data) {
      data.IsAudioEnabled = !data.IsAudioEnabled;
      OnSettingsChanged?.Invoke(data);
    }

    private void IncrementValue(ref int value) {
      value = Mathf.Clamp(value + 1, 0, 100);
    }

    private void DecrementValue(ref int value) {
      value = Mathf.Clamp(value - 1, 0, 100);
    }
  }
}

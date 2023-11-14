using CarnivalShooter.Data;
using CarnivalShooter.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarnivalShooter.Managers {
  public class SettingsManager : MonoBehaviour {
    public static SettingsManager Instance;
    public static event Action<SettingsData> OnSettingsChanged;

    [SerializeField] private SettingsData m_SettingsData;
    private Dictionary<SettingsMenuType, Action<SettingsMenuAction, SettingsData>> m_MenuActions;

    private void Awake() {
      if (Instance == null) {
        Instance = this;
        m_SettingsData = new SettingsData();

        SettingsMenu.SettingValueClicked += SetSettingsData;
        SceneManager.sceneLoaded += OnSceneLoaded;

        m_MenuActions = new Dictionary<SettingsMenuType, Action<SettingsMenuAction, SettingsData>> {
          {SettingsMenuType.GAMEPLAY_SFX, OnGameplaySfxChange },
          {SettingsMenuType.MUSIC_SFX, OnMusicSfxChange },
          {SettingsMenuType.BACKGROUND_SFX, OnBackgroundSfxChange },
          {SettingsMenuType.AUDIO_ENABLE, OnAudioEnabledToggle },
          {SettingsMenuType.LOOK_INVERSION, OnLookInversionEnabledToggle },
          {SettingsMenuType.LOOK_SENSITIVITY, OnLookSensitivityChange },
        };
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
      OnSettingsChanged?.Invoke(m_SettingsData);
    }

    private void OnDisable() {
      SettingsMenu.SettingValueClicked -= SetSettingsData;
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void SetSettingsData(SettingsMenuAction action, SettingsMenuType type) {
      SetSettingsData(action, type, m_SettingsData);
    }

    private void SetSettingsData(SettingsMenuAction settingsAction, SettingsMenuType settingsType, SettingsData data) {
      if (m_MenuActions.TryGetValue(settingsType, out Action<SettingsMenuAction, SettingsData> action)) {
        action(settingsAction, data);
      }
    }

    private void OnGameplaySfxChange(SettingsMenuAction action, SettingsData data) {
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

    private void OnMusicSfxChange(SettingsMenuAction action, SettingsData data) {
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

    private void OnBackgroundSfxChange(SettingsMenuAction action, SettingsData data) {
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

    private void OnAudioEnabledToggle(SettingsMenuAction action, SettingsData data) {
      data.IsAudioEnabled = !data.IsAudioEnabled;
      OnSettingsChanged?.Invoke(data);
    }

    private void OnLookInversionEnabledToggle(SettingsMenuAction action, SettingsData data) {
      data.IsLookInverted = !data.IsLookInverted;
      OnSettingsChanged?.Invoke(data);
    }

    private void OnLookSensitivityChange(SettingsMenuAction action, SettingsData data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValue(ref data.LookSensitivity);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValue(ref data.LookSensitivity);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void IncrementValue(ref int value) {
      value = Mathf.Clamp(value + 5, 0, 100);
    }

    private void DecrementValue(ref int value) {
      value = Mathf.Clamp(value - 5, 0, 100);
    }
  }
}

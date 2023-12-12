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
    public SettingsData GetSettingsData() => m_SettingsData;
    private void Awake() {
      if (Instance == null) {
        Instance = this;
        m_SettingsData = LoadSettingsData();

        SettingsMenu.SettingValueClicked += SetSettingsData;
        SceneManager.sceneLoaded += OnSceneLoaded;

        m_MenuActions = new Dictionary<SettingsMenuType, Action<SettingsMenuAction, SettingsData>> {
          {SettingsMenuType.GAMEPLAY_SFX, OnGameplaySfxChange },
          {SettingsMenuType.MUSIC_SFX, OnMusicSfxChange },
          {SettingsMenuType.BACKGROUND_SFX, OnBackgroundSfxChange },
          {SettingsMenuType.UI_SFX, OnUiSfxChange },
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
      SaveSettingsData();
    }

    private void SaveSettingsData() {
      PlayerPrefs.SetInt("IsAudioEnabled", m_SettingsData.IsAudioEnabled ? 1 : 0);
      PlayerPrefs.SetInt("IsLookInverted", m_SettingsData.IsLookInverted ? 1 : 0);
      PlayerPrefs.SetInt("LookSensitivity", m_SettingsData.LookSensitivity);
      PlayerPrefs.SetInt("GameplaySfxVolume", m_SettingsData.GameplaySfxVolume);
      PlayerPrefs.SetInt("MusicSfxVolume", m_SettingsData.MusicSfxVolume);
      PlayerPrefs.SetInt("BackgroundSfxVolume", m_SettingsData.BackgroundSfxVolume);
      PlayerPrefs.SetInt("UiSfxVolume", m_SettingsData.UiSfxVolume);
      PlayerPrefs.Save();
    }

    private SettingsData LoadSettingsData() {
      SettingsData data = new SettingsData();
      // Check if a single key exists and if it does it's safe to assume all keys exist
      if (PlayerPrefs.HasKey("IsAudioEnabled")) {
        bool isAudioEnabled = PlayerPrefs.GetInt("IsAudioEnabled") == 1;
        bool isLookInverted = PlayerPrefs.GetInt("IsLookInverted") == 1;
        int lookSensitivity = PlayerPrefs.GetInt("LookSensitivity");
        int gameplaySfxVolume = PlayerPrefs.GetInt("GameplaySfxVolume");
        int musicSfxVolume = PlayerPrefs.GetInt("BackgroundSfxVolume");
        int backgroundSfxVolume = PlayerPrefs.GetInt("BackgroundSfxVolume");
        int uiSfxVolume = PlayerPrefs.GetInt("UiSfxVolume");
        data.IsAudioEnabled = isAudioEnabled;
        data.IsLookInverted = isLookInverted;
        data.LookSensitivity = lookSensitivity;
        data.GameplaySfxVolume = gameplaySfxVolume;
        data.MusicSfxVolume = musicSfxVolume;
        data.BackgroundSfxVolume = backgroundSfxVolume;
        data.UiSfxVolume = uiSfxVolume;
      }
      return data;
    }

    private void SetSettingsData(SettingsMenuAction settingsAction, SettingsMenuType settingsType, SettingsData data) {
      if (m_MenuActions.TryGetValue(settingsType, out Action<SettingsMenuAction, SettingsData> action)) {
        action(settingsAction, data);
      }
    }

    private void OnGameplaySfxChange(SettingsMenuAction action, SettingsData data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValueByFive(ref data.GameplaySfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValueByFive(ref data.GameplaySfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnMusicSfxChange(SettingsMenuAction action, SettingsData data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValueByFive(ref data.MusicSfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValueByFive(ref data.MusicSfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnBackgroundSfxChange(SettingsMenuAction action, SettingsData data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValueByFive(ref data.BackgroundSfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValueByFive(ref data.BackgroundSfxVolume);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void OnUiSfxChange(SettingsMenuAction action, SettingsData data) {
      if (action == SettingsMenuAction.INCREMENT) {
        IncrementValueByFive(ref data.UiSfxVolume);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValueByFive(ref data.UiSfxVolume);
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
        IncrementValueByOneLimitFive(ref data.LookSensitivity);
        OnSettingsChanged?.Invoke(data);
        return;
      }
      if (action == SettingsMenuAction.DECREMENT) {
        DecrementValueByOneLimitFive(ref data.LookSensitivity);
        OnSettingsChanged?.Invoke(data);
      }
    }

    private void IncrementValueByOneLimitFive(ref int value) {
      value = Mathf.Clamp(value + 1, 0, 5);
    }

    private void DecrementValueByOneLimitFive(ref int value) {
      value = Mathf.Clamp(value - 1, 0, 5);
    }

    private void IncrementValueByFive(ref int value) {
      value = Mathf.Clamp(value + 5, 0, 100);
    }

    private void DecrementValueByFive(ref int value) {
      value = Mathf.Clamp(value - 5, 0, 100);
    }
  }
}

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class MainMenu : GameUIScreen {
    public static event Action SettingsMenuOpened;

    const string k_StartButton = "main-menu--start-btn";
    const string k_SettingsButton = "main-menu--settings-btn";
    const string k_QuitButton = "main-menu--quit-btn";
    private VisualElement m_StartButton, m_SettingsButton, m_QuitButton;

    private void OnEnable() {
      base.SetGameUIElements();
      m_StartButton = m_GameUIElement.Q<Button>(k_StartButton);
      m_SettingsButton = m_GameUIElement.Q<Button>(k_SettingsButton);
      m_QuitButton = m_GameUIElement.Q<Button>(k_QuitButton);
      m_StartButton.RegisterCallback<ClickEvent>(OnStart);
      m_SettingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
      m_QuitButton.RegisterCallback<ClickEvent>(OnQuit);
    }

    private void OnStart(ClickEvent e) {
      SceneManager.LoadSceneAsync(1);
    }

    private void OnSettingsButtonClicked(ClickEvent e) {
      SettingsMenuOpened?.Invoke();
    }

    private void OnQuit(ClickEvent e) {
      if (EditorApplication.isPlaying) {
        EditorApplication.isPlaying = false;
        return;
      }
      Application.Quit();
    }
  }
}

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
    const string k_CreditsButton = "main-menu--credits-btn";
    const string k_QuitButton = "main-menu--quit-btn";
    private VisualElement m_StartButton, m_SettingsButton, m_CreditsButton, m_QuitButton;

    private void OnEnable() {
      base.SetGameUIElements();
      m_StartButton = m_GameUIElement.Q<Label>(k_StartButton);
      m_SettingsButton = m_GameUIElement.Q<Label>(k_SettingsButton);
      m_CreditsButton = m_GameUIElement.Q<Label>(k_CreditsButton);
      m_QuitButton = m_GameUIElement.Q<Label>(k_QuitButton);
      m_StartButton.RegisterCallback<ClickEvent>(OnStart);
      m_SettingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
      m_SettingsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
      m_QuitButton.RegisterCallback<ClickEvent>(OnQuit);
    }

    private void OnStart(ClickEvent e) {
      SceneManager.LoadSceneAsync(1);
    }

    private void OnSettingsButtonClicked(ClickEvent e) {
      SettingsMenuOpened?.Invoke();
    }

    private void OnCreditsButtonClicked(ClickEvent e) {
      Debug.Log("Credits clicked");
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

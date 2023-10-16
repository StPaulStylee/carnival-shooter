using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class PauseMenu : GameUIScreen {
    public static event Action OnReturnToGame;
    public static event Action SettingsMenuOpened;

    const string k_ReturnToGameButton = "pause-menu-button-container--return-to-game";
    const string k_SettingsButton = "pause-menu-button-container--settings";
    const string k_ExitToMenuButton = "pause-menu-button-container--exit-to-menu";

    private VisualElement m_ReturnToGameButton;
    private VisualElement m_SettingsButton;
    private VisualElement m_ExitToMenuButton;

    private void OnEnable() {
      base.SetGameUIElements();
      m_ReturnToGameButton = m_GameUIElement.Q<VisualElement>(className: k_ReturnToGameButton);
      m_SettingsButton = m_GameUIElement.Q<VisualElement>(className: k_SettingsButton);
      m_ExitToMenuButton = m_GameUIElement.Q<VisualElement>(className: k_ExitToMenuButton);

      m_ReturnToGameButton.RegisterCallback<ClickEvent>(OnReturnToTGameButtonClicked);
      m_SettingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
      m_ExitToMenuButton.RegisterCallback<ClickEvent>(OnExitToMenuButtonClicked);
    }

    private void OnReturnToTGameButtonClicked(ClickEvent e) {
      OnReturnToGame?.Invoke();
    }

    private void OnSettingsButtonClicked(ClickEvent e) {
      SettingsMenuOpened?.Invoke();
    }

    private void OnExitToMenuButtonClicked(ClickEvent e) {
      Time.timeScale = 1f;
      SceneManager.LoadSceneAsync(0);
    }
  }
}

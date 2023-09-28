using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.UI.Manager {
  public class GameUIManager : MonoBehaviour {
    private GameHud m_GameHud;
    private PostRoundStats m_PostRoundStats;
    private PauseMenu m_PauseMenu;

    [SerializeField] private List<GameUIScreen> m_GameUIScreens;
    private Stack<GameUIScreen> m_ActiveMenuScreensStack = new Stack<GameUIScreen>();

    private void Awake() {
      CountDownTimer.TimerPostCompleted += ShowPostRoundStats;
      GameManager.PauseStateToggled += OnIsPausedToggle;
      PauseMenu.SettingsMenuOpened += ShowSettingsMenu;
    }

    public void Start() {
      ShowSingleVisualElementByName(ScreenNameConstants.GameHud);
    }

    public void HideVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(false);
    }

    public void ShowVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(true);
    }

    private void ShowSingleVisualElementByName(string name) {
      foreach (GameUIScreen screen in m_GameUIScreens) {
        HideVisualAsset(screen);
      }
      GameUIScreen asset = m_GameUIScreens.Find(screen => screen.GameHudElementName == name);
      ShowVisualAsset(asset);
    }

    private void AddActiveMenuScreen(GameUIScreen screen) {
      if (m_ActiveMenuScreensStack.Count == 0) {
        m_ActiveMenuScreensStack.Push(screen);
        ShowVisualAsset(screen);
        return;
      }
      HideVisualAsset(m_ActiveMenuScreensStack.Peek());
      ShowVisualAsset(screen);
      m_ActiveMenuScreensStack.Push(screen);
    }

    private void RemoveActiveMenuScreen() {
      if (m_ActiveMenuScreensStack.Count == 0) {
        return;
      }
      GameUIScreen removedScreen = m_ActiveMenuScreensStack.Pop();
      HideVisualAsset(removedScreen);
    }

    private void ShowPostRoundStats(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        ShowSingleVisualElementByName(ScreenNameConstants.PostRoundStats);
      }
    }

    private void ShowSettingsMenu() {
      GameUIScreen settingsMenu = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.SettingsMenu);
      AddActiveMenuScreen(settingsMenu);
    }

    private void HideSettingsMenu() {
      RemoveActiveMenuScreen();
    }

    private void OnIsPausedToggle(bool isPaused) {
      GameUIScreen pauseMenu = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.PauseMenu);
      if (isPaused) {
        m_ActiveMenuScreensStack.Push(pauseMenu);
        pauseMenu.SetVisibility(true);
        return;
      }
      RemoveActiveMenuScreen();
      m_ActiveMenuScreensStack.Clear();
    }
  }
}

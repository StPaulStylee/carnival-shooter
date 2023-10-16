using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System.Collections.Generic;

namespace CarnivalShooter.UI.Manager {
  public class GameUIManager : UIManager, IHasMenu {
    private Stack<GameUIScreen> m_ActiveMenuScreensStack = new Stack<GameUIScreen>();

    private void Awake() {
      CountDownTimer.TimerPostCompleted += ShowPostRoundStats;
      GameManager.PauseStateToggled += OnIsPausedToggle;
      PauseMenu.SettingsMenuOpened += ShowSettingsMenu;
      SettingsMenu.BackButtonClicked += HideSettingsMenu;
    }

    private void OnDisable() {
      CountDownTimer.TimerPostCompleted -= ShowPostRoundStats;
      GameManager.PauseStateToggled -= OnIsPausedToggle;
      PauseMenu.SettingsMenuOpened -= ShowSettingsMenu;
      SettingsMenu.BackButtonClicked -= HideSettingsMenu;
    }

    public void Start() {
      ShowSingleVisualElementByName(ScreenNameConstants.GameHud);
    }

    public void AddActiveMenuScreen(GameUIScreen screen) {
      if (m_ActiveMenuScreensStack.Count == 0) {
        m_ActiveMenuScreensStack.Push(screen);
        ShowVisualAsset(screen);
        return;
      }
      HideVisualAsset(m_ActiveMenuScreensStack.Peek());
      ShowVisualAsset(screen);
      m_ActiveMenuScreensStack.Push(screen);
    }

    public void RemoveActiveMenuScreen() {
      if (m_ActiveMenuScreensStack.Count == 0) {
        return;
      }
      GameUIScreen removedScreen = m_ActiveMenuScreensStack.Pop();
      HideVisualAsset(removedScreen);
      if (m_ActiveMenuScreensStack.Count > 0) {
        GameUIScreen screenToActivate = m_ActiveMenuScreensStack.Peek();
        screenToActivate.SetVisibility(true);
      }
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

    private void HidePauseMenu() {
      foreach (GameUIScreen screen in m_ActiveMenuScreensStack) {
        screen.SetVisibility(false);
      }
      m_ActiveMenuScreensStack.Clear();
    }

    private void OnIsPausedToggle(bool isPaused) {
      GameUIScreen pauseMenu = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.PauseMenu);
      if (isPaused) {
        m_ActiveMenuScreensStack.Push(pauseMenu);
        pauseMenu.SetVisibility(true);
        return;
      }
      HidePauseMenu();
    }
  }
}

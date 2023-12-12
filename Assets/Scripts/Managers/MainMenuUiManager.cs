using CarnivalShooter.Data;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace CarnivalShooter.UI.Manager {
  // Rename this class to MenuUiManager
  public class MainMenuUiManager : UIManager, IHasMenu {
    private Stack<GameUIScreen> m_ActiveMenuScreensStack = new Stack<GameUIScreen>();
    private GameControls m_GameControls;
    private void Awake() {
      m_GameControls = new GameControls();
      EnableGameControls();
      SettingsMenu.BackButtonClicked += HideActiveScreen;
      MainMenu.SettingsMenuOpened += ShowSettingsMenu;
      MainMenu.CreditsScreenOpened += ShowCreditsScreen;
      CreditsScreen.BackButtonClicked += HideActiveScreen;
    }

    private void OnDisable() {
      DisableGameControls();
      SettingsMenu.BackButtonClicked -= HideActiveScreen;
      MainMenu.SettingsMenuOpened -= ShowSettingsMenu;
      MainMenu.CreditsScreenOpened -= ShowCreditsScreen;
      CreditsScreen.BackButtonClicked -= HideActiveScreen;
    }

    private void Start() {
      GameUIScreen screen = ShowSingleVisualElementByName(ScreenNameConstants.MainMenu);
      AddActiveMenuScreen(screen);
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
      // Only the MainMenuUi is active
      if (m_ActiveMenuScreensStack.Count == 1) {
        return;
      }
      GameUIScreen removedScreen = m_ActiveMenuScreensStack.Pop();
      HideVisualAsset(removedScreen);
      if (m_ActiveMenuScreensStack.Count > 0) {
        GameUIScreen screenToActivate = m_ActiveMenuScreensStack.Peek();
        screenToActivate.SetVisibility(true);
      }
    }

    private void ShowSettingsMenu() {
      GameUIScreen settingsMenu = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.SettingsMenu);
      AddActiveMenuScreen(settingsMenu);
    }

    private void ShowCreditsScreen() {
      GameUIScreen creditsScreen = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.CreditsScreen);
      AddActiveMenuScreen(creditsScreen);
    }

    private void HideActiveScreen() {
      RemoveActiveMenuScreen();
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx) {
      RemoveActiveMenuScreen();
    }

    private void EnableGameControls() {
      m_GameControls.GameController.Enable();
      m_GameControls.GameController.Pause.performed += OnPausePerformed;
    }

    private void DisableGameControls() {
      m_GameControls.GameController.Pause.performed -= OnPausePerformed;
      m_GameControls = null;
    }
  }
}

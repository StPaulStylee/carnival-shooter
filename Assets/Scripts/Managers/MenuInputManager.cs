using CarnivalShooter.Data;
using System.Collections.Generic;

namespace CarnivalShooter.UI.Manager {
  // Rename this class to MenuUiManager
  public class MenuInputManager : UIManager, IHasMenu {
    private Stack<GameUIScreen> m_ActiveMenuScreensStack = new Stack<GameUIScreen>();

    private void Awake() {
      SettingsMenu.BackButtonClicked += HideActiveScreen;
      MainMenu.SettingsMenuOpened += ShowSettingsMenu;
      MainMenu.CreditsScreenOpened += ShowCreditsScreen;
      CreditsScreen.BackButtonClicked += HideActiveScreen;
    }

    private void OnDisable() {
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
  }
}

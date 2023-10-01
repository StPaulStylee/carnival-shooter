using System;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class SettingsMenu : GameUIScreen {
    public static event Action BackButtonClicked;
    const string k_BackButton = "settings-menu--back-button";

    private VisualElement m_BackButton;

    private void OnEnable() {
      base.SetGameUIElements();
      m_BackButton = m_GameUIElement.Q<VisualElement>(k_BackButton);

      m_BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
    }

    private void OnBackButtonClicked(ClickEvent e) {
      BackButtonClicked?.Invoke();
    }
  }
}

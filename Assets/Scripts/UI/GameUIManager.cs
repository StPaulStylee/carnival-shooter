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

    private void Awake() {
      CountDownTimer.TimerPostCompleted += ShowPostRoundStats;
      GameManager.PauseStateToggled += OnIsPausedToggle;
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

    private void ShowVisualElementByName(string name) {

    }

    private void ShowPostRoundStats(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        ShowSingleVisualElementByName(ScreenNameConstants.PostRoundStats);
      }
    }

    private void OnIsPausedToggle(bool isPaused) {
      GameUIScreen asset = m_GameUIScreens.Find(screen => screen.GameHudElementName == ScreenNameConstants.PauseMenu);
      if (isPaused) {
        asset.SetVisibility(true);
        return;
      }
      asset.SetVisibility(false);
    }
  }
}

using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Input;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class GameInputManager : MonoBehaviour {
    private GameControls gameControls;

    [Header("In Scene Dependencies")]
    [SerializeField] private Player m_Player;
    [SerializeField] private InGameGlobalInput m_GameGlobalInput;

    private void Awake() {
      gameControls = new GameControls();
      gameControls.GameController.Enable();
      // Differentiating here because the player will need to be able to pause before the round actually starts
      GameManager.OnGameStarted += SetupGlobalInputActions;
      GameManager.InitializationCompleted += SetupGameplayInputActions;
      GameManager.PauseStateToggled += ToggleGameplayInputActions;
      CountDownTimer.TimerCompleted += TeardownGameplayInputActions;
    }

    private void OnDisable() {
      GameManager.OnGameStarted -= SetupGlobalInputActions;
      GameManager.InitializationCompleted -= SetupGameplayInputActions;
      GameManager.PauseStateToggled -= ToggleGameplayInputActions;
      CountDownTimer.TimerCompleted -= TeardownGameplayInputActions;
      TeardownGlobalInputActions();
      TearDownInputActions();
    }

    private void SetupGlobalInputActions() {
      m_GameGlobalInput.SetupGlobalInputActions(gameControls);
    }

    private void SetupGameplayInputActions(GameType _) {
      m_Player.SetupInputActions(gameControls);
    }

    private void TeardownGlobalInputActions() {
      m_GameGlobalInput.TeardownGlobalInputActions(gameControls);
    }

    private void TeardownGameplayInputActions(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        m_Player.TearDownInputActions(gameControls);
      }
    }

    private void ToggleGameplayInputActions(bool isPaused) {
      if (isPaused) {
        m_Player.TearDownInputActions(gameControls);
        return;
      }
      m_Player.SetupInputActions(gameControls);
    }

    private void TearDownInputActions() {
      m_Player.TearDownInputActions(gameControls);
    }
  }
}

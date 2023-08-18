using Assets.Scripts.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class InputManager : MonoBehaviour {
    private GameControls gameControls;

    [Header("In Scene Dependencies")]
    [SerializeField] private Player m_Player;

    private void Awake() {
      gameControls = new GameControls();
      // Differentiating here because the player will need to be able to pause before the round actually starts
      GameManager.InitializationCompleted += SetupGameplayInputActions;
      CountDownTimer.TimerCompleted += TeardownGameplayInputActions;
    }

    private void OnDisable() {
      GameManager.InitializationCompleted -= SetupGameplayInputActions;
      TearDownInputActions();
    }

    private void SetupGameplayInputActions(GameType _) {
      m_Player.SetupInputActions(gameControls);
    }

    private void TeardownGameplayInputActions(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) ;
      m_Player.TearDownInputActions(gameControls);
    }

    private void TearDownInputActions() {
      m_Player.TearDownInputActions(gameControls);
    }
  }
}

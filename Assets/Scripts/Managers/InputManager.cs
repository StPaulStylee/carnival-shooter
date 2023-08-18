using CarnivalShooter.Gameplay;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class InputManager : MonoBehaviour {
    private GameControls gameControls;

    [Header("In Scene Dependencies")]
    [SerializeField] private Player m_Player;

    private void Awake() {
      gameControls = new GameControls();
      SetupInputActions();
    }

    private void OnDisable() {
      TearDownInputActions();
    }

    private void SetupInputActions() {
      m_Player.SetupInputActions(gameControls);
    }

    private void TearDownInputActions() {
      m_Player.TearDownInputActions(gameControls);
    }
  }
}
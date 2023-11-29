using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarnivalShooter.Input {
  public class CursorManager : MonoBehaviour {
    public static event Action OnPause;
    private bool m_IsPaused;
    private bool m_IsPostRoundStatsActive = false;

    private void Awake() {
      GameManager.PauseStateToggled += OnIsPausedToggle;
      CountDownTimer.TimerPostCompleted += EnableCursorForPostRoundStats;

    }
    private void OnDisable() {
      GameManager.PauseStateToggled -= OnIsPausedToggle;
      CountDownTimer.TimerPostCompleted -= EnableCursorForPostRoundStats;

    }

    private void EnableCursorForPostRoundStats(string timerType) {
      if (!timerType.Equals(TimerConstants.RoundTimerKey)) {
        return;
      }
      Cursor.lockState = CursorLockMode.Confined;
      Cursor.visible = true;
      m_IsPostRoundStatsActive = true;
      return;
    }


    private void OnIsPausedToggle(bool isPaused) {
      m_IsPaused = isPaused;
      if (m_IsPostRoundStatsActive) {
        return;
      }
      if (m_IsPaused) {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        return;
      }
      Time.timeScale = 1f;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    public void SetupGlobalInputActions(GameControls gameInput) {
      gameInput.GameController.Enable();
      gameInput.GameController.Pause.performed += OnPausePerformed;
    }

    public void TeardownGlobalInputActions(GameControls gameInput) {
      gameInput.GameController.Disable();
      gameInput.GameController.Pause.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx) {
      OnPause?.Invoke();
    }
  }
}

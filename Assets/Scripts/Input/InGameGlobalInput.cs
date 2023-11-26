using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarnivalShooter.Input {
  public class InGameGlobalInput : MonoBehaviour {
    public static event Action OnPause;
    private bool m_IsPaused;
    private bool m_IsCursorVisible = false;
    private void Awake() {
      GameManager.PauseStateToggled += OnIsPausedToggle;
      //StatManager.PostRoundStatsCompleted += ToggleCursorState;
      CountDownTimer.TimerPostCompleted += ToggleCursorState;

    }
    private void OnDisable() {
      GameManager.PauseStateToggled -= OnIsPausedToggle;
      //StatManager.PostRoundStatsCompleted -= ToggleCursorState;
      CountDownTimer.TimerPostCompleted -= ToggleCursorState;

    }

    //private void ToggleCursorState(PostRoundStatsData _) {
    //  ToggleCursorState();
    //}

    // This is terrible. Don't name the method this. Or don't actually rely on this event
    // Maybe its ok since it needs to disable at the start of the game anyways
    private void ToggleCursorState(string timerType) {
      if (!timerType.Equals(TimerConstants.RoundTimerKey)) {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_IsCursorVisible = false;
        return;
      }
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      m_IsCursorVisible = true;
      return;
    }


    private void OnIsPausedToggle(bool isPaused) {
      m_IsPaused = isPaused;
      if (m_IsPaused) {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
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

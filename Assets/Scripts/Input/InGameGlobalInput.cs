using CarnivalShooter.Managers;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarnivalShooter.Input {
  public class InGameGlobalInput : MonoBehaviour {
    public static event Action OnPause;
    private bool m_IsPaused;
    private void Awake() {
      GameManager.PauseStateToggled += OnIsPausedToggle;
    }

    private void OnIsPausedToggle(bool isPaused) {
      m_IsPaused = isPaused;
      if (m_IsPaused) {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        return;
      }
      Time.timeScale = 1f;
      Cursor.lockState = CursorLockMode.Locked;
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

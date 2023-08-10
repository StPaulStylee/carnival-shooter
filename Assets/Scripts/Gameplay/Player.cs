using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarnivalShooter.Gameplay {
  public class Player : MonoBehaviour {
    private GameControls gameControls;
    private Camera mainCamera;
    [SerializeField] private Weapon weapon;

    private void Awake() {
      gameControls = new GameControls();
      mainCamera = Camera.main;
      weapon = GetComponentInChildren<Weapon>(false);
      Cursor.lockState = CursorLockMode.Locked;

      if (weapon == null) {
        Debug.LogError($"The {name} component does not have a child Weapon component!");
      }
      SetupInputActions();
    }

    private void Update() {
      CameraLook();
    }

    private void OnDisable() {
      TearDownInputActions();
    }


    [Header("Look Configuration")]
    [Range(0, 1)]public float LookSensitivity = 0.5f; // mouse look sensitivity
    public float MinLookX = -80f; // Lowest we can look
    public float MaxLookX = 80f; // Highest we can look
    public float MinLookY = -70f;
    public float MaxLookY = 70f;
    private float currentRotationX; // current x rotation of the mainCamera
    private float currentRotationY;

    private void CameraLook() {
      Vector2 currentInput = gameControls.GameController.Look.ReadValue<Vector2>();
      bool isMoving = currentInput.y != 0f || currentInput.x != 0f;
      if (isMoving) {
        currentRotationY += currentInput.x * LookSensitivity; // Get Y rotation AROUND the x axis
        currentRotationY = Mathf.Clamp(currentRotationY, MinLookY, MaxLookY);
        currentRotationX += currentInput.y * LookSensitivity; // Get X rotation AROUND the y axis
        currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
        mainCamera.transform.localRotation = Quaternion.Euler(-currentRotationX, currentRotationY, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
      }
      //transform.eulerAngles += Vector3.up * currentRotationY; // Apply the y rotation to the player (Not the mainCamera) so the player is always facing the correct way
    }

    private void OnFirePerformed(InputAction.CallbackContext ctx) {
      weapon.TryShoot();
    }

    private void OnReloadPerformed(InputAction.CallbackContext ctx) {
      weapon.Reload();
    }

    private void SetupInputActions() {
      gameControls.GameController.Fire.performed += OnFirePerformed;
      gameControls.GameController.Reload.performed += OnReloadPerformed;
      gameControls.GameController.Enable();
    }

    private void TearDownInputActions() {
      gameControls.GameController.Fire.performed -= OnFirePerformed;
      gameControls.GameController.Reload.performed -= OnReloadPerformed;
      gameControls.GameController.Disable();
    }
  }
}

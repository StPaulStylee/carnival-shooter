using CarnivalShooter.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CarnivalShooter.Gameplay {
  public class Player : MonoBehaviour {
    private GameControls gameControls;
    private CameraHolder m_MainCamera;
    [SerializeField] private GenericWeapon m_Weapon;

    private void Awake() {
      m_MainCamera = GetComponentInChildren<CameraHolder>();
      m_Weapon = GetComponentInChildren<GenericWeapon>(false);
      Cursor.lockState = CursorLockMode.Locked;

      if (m_Weapon == null) {
        Debug.LogError($"The {name} component does not have a child Weapon component!");
      }
    }

    private void Update() {
      if (gameControls != null) {
        CameraLook();
      }
    }

    [Header("Look Configuration")]
    [Range(0, 0.5f)] public float LookSensitivity = 0.5f; // mouse look sensitivity
    public float MinLookX = -80f; // Lowest we can look
    public float MaxLookX = 80f; // Highest we can look
    public float MinLookY = -70f;
    public float MaxLookY = 70f;
    private float currentRotationX; // current x rotation of the mainCamera
    private float currentRotationY;

    private void CameraLook() {
      Vector2 currentInput = gameControls.GameController.Look.ReadValue<Vector2>();
      m_Weapon.WeaponSway.VelocityForSway = currentInput;
      bool isMoving = currentInput.y != 0f || currentInput.x != 0f;
      if (isMoving) {
        currentRotationY += currentInput.x * LookSensitivity; // Get Y rotation AROUND the x axis
        currentRotationY = Mathf.Clamp(currentRotationY, MinLookY, MaxLookY);
        currentRotationX += currentInput.y * LookSensitivity; // Get X rotation AROUND the y axis
        currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
        m_MainCamera.transform.localRotation = Quaternion.Euler(-currentRotationX, currentRotationY, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
      }
    }

    private void OnFirePerformed(InputAction.CallbackContext ctx) {
      m_Weapon.TryShoot();
    }

    private void OnReloadPerformed(InputAction.CallbackContext ctx) {
      m_Weapon.Reload();
    }

    public void SetupInputActions(GameControls gameInput) {
      gameInput.GameController.Fire.performed += OnFirePerformed;
      gameInput.GameController.Reload.performed += OnReloadPerformed;
      gameControls = gameInput;
    }

    public void TearDownInputActions(GameControls gameInput) {
      gameInput.GameController.Fire.performed -= OnFirePerformed;
      gameInput.GameController.Reload.performed -= OnReloadPerformed;
      gameControls = null;
    }
  }
}

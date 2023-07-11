using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class Player : MonoBehaviour {
    private Camera mainCamera;
    [SerializeField] private Weapon weapon;

    private void Awake() {
      mainCamera = Camera.main;
      weapon = GetComponentInChildren<Weapon>(false);
      Cursor.lockState = CursorLockMode.Locked;

      if (weapon == null ) {
        Debug.LogError($"The {name} component does not have a child Weapon component!");
      }
    }

    private void Update() {
      if (Input.GetButton("Fire1")) {
        weapon.TryShoot();
      }
      CameraLook();
    }


    [Header("Look Configuration")]
    public float LookSensitivity = 4f; // mouse look sensitivity
    public float MinLookX = -80f; // Lowest we can look
    public float MaxLookX = 80f; // Highest we can look
    public float MinLookY = -70f;
    public float MaxLookY = 70f;
    private float currentRotationX; // current x rotation of the mainCamera
    private float currentRotationY;

    private void CameraLook() {
      currentRotationY += Input.GetAxis("Mouse X") * LookSensitivity; // Get Y rotation AROUND the x axis
      currentRotationY = Mathf.Clamp(currentRotationY, MinLookY, MaxLookY);
      currentRotationX += Input.GetAxis("Mouse Y") * LookSensitivity; // Get X rotation AROUND the y axis
      currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
      mainCamera.transform.localRotation = Quaternion.Euler(-currentRotationX, currentRotationY, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
      //transform.eulerAngles += Vector3.up * currentRotationY; // Apply the y rotation to the player (Not the mainCamera) so the player is always facing the correct way
    }
  }
}

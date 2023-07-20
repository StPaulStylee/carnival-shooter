using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class Target : ShotAnimatable {
    public bool IsStanding => m_isStanding;
    private readonly string TAKE_SHOT_TRIGGER = "Hit";
    private readonly string RESET_TRIGGER = "Reset";

    private bool m_isStanding = false;
    private MeshCollider[] m_meshColliders;

    private void Awake() {
      m_Animator = GetComponent<Animator>();
      m_meshColliders = GetComponentsInChildren<MeshCollider>();
      GameManager.InitializationCompleted += SetRoundStartAnimationState;
    }

    public override void PlayTakeShot() {
      DisableMeshColliders();
      m_isStanding = false;
      m_Animator.SetTrigger(TAKE_SHOT_TRIGGER);
    }

    public override void ResetToDefault() {
      EnableMeshColliders();
      m_isStanding = true;
      m_Animator.SetTrigger(RESET_TRIGGER);
    }

    private void DisableMeshColliders() {
      foreach (var collider in m_meshColliders) {
        collider.enabled = false;
      }
    }

    private void EnableMeshColliders() {
      foreach (var collider in m_meshColliders) {
        collider.enabled = true;
      }
    }

    private void SetRoundStartAnimationState(GameType gameType) {
      switch (gameType) {
        case GameType.AllStanding:
          // Fire ready event to start countdown
          break;
        case GameType.WhackAMole:
          PlayTakeShot();
          // Fire ready event to start countdown
          break;
        default:
          // Fire ready event to start countdown
          break;
      }
    }
  }
}

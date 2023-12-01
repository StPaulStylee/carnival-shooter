using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class Target : ShotAnimatable {
    public bool IsStanding => m_isStanding;
    private readonly string TAKE_SHOT_TRIGGER = "Hit";
    private readonly string RESET_TRIGGER = "Reset";
    public static int DisableCount = 0;

    [SerializeField] private bool m_isStanding = false;
    private MeshCollider[] m_meshColliders;

    private void Awake() {
      m_isStanding = false;
      m_meshColliders = null; // Do I need this?
      DisableCount = 0;
      m_Animator = GetComponent<Animator>();
      m_meshColliders = GetComponentsInChildren<MeshCollider>();
      GameManager.OnSetInitialTargetState += SetRoundStartAnimationState;
    }

    private void OnDisable() {
      GameManager.OnSetInitialTargetState -= SetRoundStartAnimationState;
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
      Target.DisableCount++;
      foreach (var meshCollider in m_meshColliders) {
        meshCollider.enabled = false;
      }
    }

    private void EnableMeshColliders() {
      foreach (MeshCollider meshCollider in m_meshColliders) {
        meshCollider.enabled = true;
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

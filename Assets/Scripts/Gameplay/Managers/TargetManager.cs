using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Managers {
  public class TargetManager : MonoBehaviour {
    private List<Target> targetsInScene = new();
    private Target m_CurrentActiveTarget;

    [SerializeField] private float targetStandingTime = 2f;
    private bool m_isRoundActive;

    private void Awake() {
      GameManager.InitializationCompleted += OnInitializationCompleted;
    }

    private Target GetTargetToActivate() {
      int indexToActivate = Random.Range(0, targetsInScene.Count);
      Target targetToActivate = targetsInScene[indexToActivate];
      if (targetToActivate.IsStanding) {
        print("Recursion! Look for weirdness.");
        GetTargetToActivate();
      }
      return targetToActivate;
    }

    private void OnInitializationCompleted(GameType gametype) {
      Target[] targets = FindObjectsOfType<Target>();
      foreach (Target target in targets) {
        targetsInScene.Add(target);
      }
      StartCoroutine(StartGame(gametype));
    }

    private IEnumerator StartGame(GameType gametype) {
      m_isRoundActive = true;
      while (m_isRoundActive) {
        if (m_CurrentActiveTarget != null) {
          m_CurrentActiveTarget.PlayTakeShot();
        }
        float standingTimeRemaining = targetStandingTime;
        m_CurrentActiveTarget = GetTargetToActivate();
        m_CurrentActiveTarget.ResetToDefault();
        yield return new WaitForSeconds(targetStandingTime);
      }

    }
  }
}

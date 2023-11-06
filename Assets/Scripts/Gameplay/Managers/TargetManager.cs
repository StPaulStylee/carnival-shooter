using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Managers {
  public class TargetManager : MonoBehaviour {
    private List<Target> targetsInScene = new();
    private List<Target> m_CurrentActiveTargets = new();

    [SerializeField] private float targetStandingTime = 2f;
    private bool m_isRoundActive;

    private void Awake() {
      GameManager.InitializationCompleted += OnInitializationCompleted;
    }

    private void OnDisable() {
      GameManager.InitializationCompleted -= OnInitializationCompleted;
    }

    private Target GetTargetToActivate() {
      int indexToActivate = UnityEngine.Random.Range(0, targetsInScene.Count);
      Target targetToActivate = targetsInScene[indexToActivate];
      if (targetToActivate.IsStanding) {
        print("Recursion! Look for weirdness.");
        GetTargetToActivate();
      }
      return targetToActivate;
    }

    private void GetTargetsToActivate(int amount) {
      for (int i = 0; i < amount; i++) {
        m_CurrentActiveTargets.Add(GetTargetToActivate());
      }
    }

    private void OnInitializationCompleted(GameType gametype) {
      Target[] targets = FindObjectsOfType<Target>();
      foreach (Target target in targets) {
        targetsInScene.Add(target);
      }
      StartCoroutine(StartGame(gametype));
    }

    private bool HasActiveTargetsRemaining() {
      bool hasTargetRemaining = m_CurrentActiveTargets.Find(target => target.IsStanding);
      return hasTargetRemaining;
    }

    private List<Target> GetAllActiveTargets() {
      var targets = m_CurrentActiveTargets.FindAll(target => target.IsStanding);
      return targets;
    }

    private IEnumerator StartGame(GameType gametype) {
      m_isRoundActive = true;
      while (m_isRoundActive) {
        if (HasActiveTargetsRemaining()) {
          foreach (Target target in GetAllActiveTargets()) {
            target.PlayTakeShot();
          }
        }
        float standingTimeRemaining = targetStandingTime;
        m_CurrentActiveTargets.Clear();
        GetTargetsToActivate(5);
        foreach (Target target in m_CurrentActiveTargets) {
          target.ResetToDefault();
        }
        yield return new WaitForSeconds(targetStandingTime);
      }

    }
  }
}

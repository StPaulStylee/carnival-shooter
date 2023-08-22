using Assets.Scripts.Data;
using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Gameplay.Behavior;
using UnityEngine;

public class StatManager : MonoBehaviour {
  private int m_totalShotsFired = 0;
  private int m_totalOuterZoneHits = 0;
  private int m_totalInnerZoneHits = 0;
  private int m_totalBullseyeHits = 0;

  private void Awake() {
    Scoreable.PointsScored += OnScoreableHit;
    CountDownTimer.TimerCompleted += OnRoundCompleted;
    Weapon.AmmoChanged += OnWeaponShot;
  }

  private void OnDisable() {
    Scoreable.PointsScored -= OnScoreableHit;
    CountDownTimer.TimerCompleted -= OnRoundCompleted;
    Weapon.AmmoChanged -= OnWeaponShot;
  }

  private void OnRoundCompleted(string timerType) {
    if (timerType.Equals(TimerConstants.RoundTimerKey)) {
      Debug.Log($"Shots Fired: {m_totalShotsFired}");
      Debug.Log($"Outzone Hits: {m_totalOuterZoneHits}");
      Debug.Log($"Inner Zone Hits: {m_totalInnerZoneHits}");
      Debug.Log($"Bullseye Hits: {m_totalBullseyeHits}");
    }
  }

  private void OnScoreableHit(int _, string scoreableLabel) {
    switch (scoreableLabel) {
      case ScoreConstants.OuterZoneLabel: m_totalOuterZoneHits++; break;
      case ScoreConstants.InnerZoneLabel: m_totalInnerZoneHits++; break;
      case ScoreConstants.BullseyeLabel: m_totalBullseyeHits++; break;
      default: break;
    }
  }

  private void OnWeaponShot() {
    m_totalShotsFired++;
  }

}

using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers;
using System;
using UnityEngine;

public class StatManager : MonoBehaviour {
  public static event Action<int> ScoreUpdated;
  public static event Action<PostRoundStatsData> PostRoundStatsCompleted;

  private int m_TotalShotsFired = 0;
  private float m_TotalShotsHit {
    get { return m_TotalOuterZoneHits + m_TotalInnerZoneHits + m_TotalBullseyeHits; }
  }
  private float m_ShotAccuracy {
    get { return (m_TotalShotsHit / m_TotalShotsFired) * 100f; }
  }

  private float getShotAccuracy() => Mathf.RoundToInt((m_TotalShotsHit / m_TotalShotsFired) * 100f);
  private float m_TotalOuterZoneHits = 0;
  private float m_TotalInnerZoneHits = 0;
  private float m_TotalBullseyeHits = 0;
  private int m_TotalScore = 0;

  private void Awake() {
    Scoreable.PointsScored += OnScoreableHit;
    CountDownTimer.TimerCompleted += OnRoundCompleted;
    Weapon.AmmoChanged += OnWeaponShot;
    GameManager.ScoreInitializing += (int score) => m_TotalScore = score;
  }

  private void OnDisable() {
    Scoreable.PointsScored -= OnScoreableHit;
    CountDownTimer.TimerCompleted -= OnRoundCompleted;
    Weapon.AmmoChanged -= OnWeaponShot;
  }

  private void OnRoundCompleted(string timerType) {
    if (timerType.Equals(TimerConstants.RoundTimerKey)) {
      Debug.Log($"Shots Fired: {m_TotalShotsFired}");
      Debug.Log($"Bullseye Hits: {m_TotalBullseyeHits}");
      Debug.Log($"Inner Zone Hits: {m_TotalInnerZoneHits}");
      Debug.Log($"Outzone Hits: {m_TotalOuterZoneHits}");
      Debug.Log($"Shots Hit: {m_TotalShotsHit}");
      Debug.Log($"Hit Accuracy: {getShotAccuracy()}");
      Debug.Log($"Total Score: {m_TotalScore}");
      PostRoundStatsData stats = new PostRoundStatsData(m_TotalShotsFired, (int)m_TotalBullseyeHits, (int)m_TotalInnerZoneHits, (int)m_TotalOuterZoneHits, (int)m_TotalShotsHit, getShotAccuracy(), m_TotalScore);
      PostRoundStatsCompleted?.Invoke(stats);
      // Calculate some sort of accuracy bonus
    }
  }

  private void OnScoreableHit(int points, string scoreableLabel) {
    switch (scoreableLabel) {
      case ScoreConstants.OuterZoneLabel: {
          m_TotalScore += points;
          ScoreUpdated?.Invoke(m_TotalScore);
          m_TotalOuterZoneHits++;
          break;
        }
      case ScoreConstants.InnerZoneLabel: {
          m_TotalScore += points;
          ScoreUpdated?.Invoke(m_TotalScore);
          m_TotalInnerZoneHits++;
          break;
        }
      case ScoreConstants.BullseyeLabel: {
          m_TotalScore += points;
          ScoreUpdated?.Invoke(m_TotalScore);
          m_TotalBullseyeHits++;
          break;
        }
      default: break;
    }
  }

  private void OnWeaponShot() {
    m_TotalShotsFired++;
  }

}

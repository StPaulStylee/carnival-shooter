using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers;
using System;
using UnityEngine;

public class StatManager : MonoBehaviour {
  public static event Action<int> ScoreUpdated;
  public static event Action<PostRoundStatsData> PostRoundStatsCompleted;

  public float OuterZoneBonusWeight = 0.08f;
  public float InnerZoneBonusWeight = 0.02f;
  public float BullseyeZoneBonusWeight = 0.1f;
  private float OverallAccuracyBonusWeight;

  private int m_TotalShotsFired = 0;
  private float m_TotalShotsHit {
    get { return m_TotalOuterZoneHits + m_TotalInnerZoneHits + m_TotalBullseyeHits; }
  }
  private float m_ShotAccuracy {
    get { return (m_TotalShotsHit / m_TotalShotsFired) * 100f; }
  }

  private float getShotAccuracyPercentage() => Mathf.RoundToInt((m_TotalShotsHit / m_TotalShotsFired) * 100f);
  private float getShotAccuracyDecimal() => m_TotalShotsHit / m_TotalShotsFired;

  private float m_TotalOuterZoneHits = 0;
  private int m_TotalOutzonePoints = 0;
  private float m_TotalInnerZoneHits = 0;
  private int m_TotalInnerZonePoints = 0;
  private float m_TotalBullseyeHits = 0;
  private int m_TotalBullseyePoints = 0;
  private int m_RoundBonus = 300;
  private int m_TotalScore = 0;

  private void Awake() {
    Scoreable.PointsScored += OnScoreableHit;
    CountDownTimer.TimerCompleted += OnRoundCompleted;
    CurrentWeapon.AmmoChanged += OnWeaponShot;
    GameManager.ScoreInitializing += (int score) => m_TotalScore = score;
    OverallAccuracyBonusWeight = 1f - (OuterZoneBonusWeight + InnerZoneBonusWeight);
    Debug.Log(OverallAccuracyBonusWeight);
  }

  private void OnDisable() {
    Scoreable.PointsScored -= OnScoreableHit;
    CountDownTimer.TimerCompleted -= OnRoundCompleted;
    CurrentWeapon.AmmoChanged -= OnWeaponShot;
  }

  private void OnRoundCompleted(string timerType) {
    if (timerType.Equals(TimerConstants.RoundTimerKey)) {
      Debug.Log($"Shots Fired: {m_TotalShotsFired}");
      Debug.Log($"Bullseye Hits: {m_TotalBullseyeHits}");
      Debug.Log($"Inner Zone Hits: {m_TotalInnerZoneHits}");
      Debug.Log($"Outzone Hits: {m_TotalOuterZoneHits}");
      Debug.Log($"Shots Hit: {m_TotalShotsHit}");
      Debug.Log($"Hit Accuracy: {getShotAccuracyPercentage()}");
      Debug.Log($"Total Score: {m_TotalScore}");
      PostRoundStatsData stats = new PostRoundStatsData(
        totalBullseyeHits: (int)m_TotalBullseyeHits,
        totalBullseyeScore: m_TotalBullseyePoints,
        totalInnerZoneHits: (int)m_TotalInnerZoneHits,
        totalInnerZoneScore: m_TotalInnerZonePoints,
        totalOuterZoneHits: (int)m_TotalOuterZoneHits,
        totalOuterZoneScore: m_TotalOutzonePoints,
        totalShotsFired: m_TotalShotsFired,
        totalHits: (int)m_TotalShotsHit,
        accuracy: getShotAccuracyPercentage(),
        roundBonus: GetRoundBonus(),
        roundScore: m_TotalScore + m_RoundBonus
      );
      PostRoundStatsCompleted?.Invoke(stats);
      // Calculate some sort of accuracy bonus
    }
  }

  private void OnScoreableHit(int points, string scoreableLabel) {
    switch (scoreableLabel) {
      case ScoreConstants.OuterZoneLabel: {
          m_TotalOutzonePoints += points;
          m_TotalScore += points;
          ScoreUpdated?.Invoke(m_TotalScore);
          m_TotalOuterZoneHits++;
          break;
        }
      case ScoreConstants.InnerZoneLabel: {
          m_TotalInnerZonePoints += points;
          m_TotalScore += points;
          ScoreUpdated?.Invoke(m_TotalScore);
          m_TotalInnerZoneHits++;
          break;
        }
      case ScoreConstants.BullseyeLabel: {
          m_TotalBullseyePoints += points;
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


  private int GetRoundBonus() {
    float bonus = (0.05f * m_TotalInnerZonePoints + 0.15f * m_TotalOutzonePoints + 0.65f * m_TotalBullseyePoints + (0.15f * getShotAccuracyDecimal() * m_TotalScore));
    Debug.Log(bonus);
    return Mathf.RoundToInt(bonus);
  }

}

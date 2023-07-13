using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers.Data;
using System;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class GameManager : MonoBehaviour {
    public static event Action<int> AmmoInitializing;
    public static event Action<float> RoundDurationInitializing;
    public static event Action<int> ScoreInitializing;
    public static event Action<int> ScoreUpdated;
    public static event Action<GameType> InitializationCompleted;

    private int m_TotalScore;
    public int TotalScore => m_TotalScore;

    [Header("Start of Round Data")]
    [Tooltip("The amount of Ammo to be given to the weapon")]
    [SerializeField] private int m_StartingAmmo;
    [Tooltip("The amount of time in seconds the round will last")]
    [SerializeField] private float m_RoundDuration;
    [Tooltip("The score the player will start the round with")]
    [SerializeField] private int m_InitialScore = 0;

    private void Awake() {
      Scoreable.PointsScored += SetPointsScored;
    }

    private void OnEnable() {
      OnInitializeRound();
    }

    private void Start() {
      OnInitializationComplete();
    }

    private void OnInitializeRound() {
      AmmoInitializing?.Invoke(m_StartingAmmo);
      RoundDurationInitializing?.Invoke(m_RoundDuration);
      ScoreInitializing?.Invoke(m_InitialScore);
    }

    private void OnInitializationComplete() {
      InitializationCompleted?.Invoke(GameType.WhackAMole);
    }

    private void SetPointsScored(int points) {
      m_TotalScore += points;
      ScoreUpdated?.Invoke(m_TotalScore);
    }
  }
}

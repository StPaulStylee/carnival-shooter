using Assets.Scripts.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Gameplay.Behavior;
using CarnivalShooter.Managers.Data;
using System;
using System.Collections;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class GameManager : MonoBehaviour {
    public static event Action<int> AmmoInitializing;
    public static event Action<string, float> CountdownTimerInitializing;
    // RoundStartCountdown
    public static event Action<string> CountdownTimerStarted;
    public static event Action<int> ScoreInitializing;
    public static event Action<int> ScoreUpdated;
    public static event Action<GameType> InitializationCompleted;

    private int m_TotalScore;
    // Right now this is completely controller by CountdownTimer. This will need to be changed if
    // this value depends on more than just the countdown timer sending its ready event
    private bool m_IsInitialized = false;
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
      CountDownTimer.TimerBlockingExecution += SetInitialized;
    }

    private void OnEnable() {
      OnInitializeRound();
    }

    private void Start() {
      StartCoroutine(WaitUntilInitializationComplete());
      //OnInitializationComplete();
    }

    private void OnInitializeRound() {
      AmmoInitializing?.Invoke(m_StartingAmmo);
      CountdownTimerInitializing?.Invoke(TimerConstants.RoundTimerKey, m_RoundDuration);
      CountdownTimerInitializing?.Invoke(TimerConstants.RoundStartCountdownKey, 3f); // Remove this constant float
      ScoreInitializing?.Invoke(m_InitialScore);
      CountdownTimerStarted?.Invoke(TimerConstants.RoundStartCountdownKey);
    }

    private void OnInitializationComplete() {
      InitializationCompleted?.Invoke(GameType.WhackAMole);
      CountdownTimerStarted?.Invoke(TimerConstants.RoundTimerKey);
    }

    private void SetPointsScored(int points) {
      m_TotalScore += points;
      ScoreUpdated?.Invoke(m_TotalScore);
    }

    private void SetInitialized(bool isInitialized) {
      Debug.Log(isInitialized);
      m_IsInitialized = !isInitialized;
    }

    private IEnumerator WaitUntilInitializationComplete() {
      while (m_IsInitialized == false) {
        Debug.Log("GameManager in coroutine");
        yield return null;
      }
      OnInitializationComplete();
    }
  }
}

using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers.Data;
using System;
using System.Collections;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class GameManager : MonoBehaviour {
    public static event Action<int> AmmoInitializing;
    //public static event Action<string> CountdownTimerInitializing;
    // RoundStartCountdown
    public static event Action<string> CountdownTimerStarted;
    public static event Action<int> ScoreInitializing;
    public static event Action<GameType> InitializationCompleted;

    private int m_TotalScore;
    // Right now this is completely controller by CountdownTimer. This will need to be changed if
    // this value depends on more than just the countdown timer sending its ready event
    private bool m_IsInitialized = false;
    public int TotalScore => m_TotalScore;

    [Header("Start of Round Data")]
    [Tooltip("The amount of Ammo to be given to the weapon")]
    [SerializeField] private int m_StartingAmmo;
    [Tooltip("The score the player will start the round with")]
    [SerializeField] private int m_InitialScore = 0;
    [SerializeField] private GameType m_GameType = GameType.WhackAMole;
    private void Awake() {
      CountDownTimer.TimerBlockingExecution += SetInitialized;
    }

    private void OnDisable() {
      CountDownTimer.TimerBlockingExecution -= SetInitialized;
    }

    private void OnEnable() {
      OnInitializeRound();
    }

    private void Start() {
      StartCoroutine(CompleteInitialization());
    }

    private void OnInitializeRound() {
      AmmoInitializing?.Invoke(m_StartingAmmo);
      CountdownTimerStarted?.Invoke(TimerConstants.RoundStartCountdownKey);
    }

    private void OnInitializationComplete() {
      InitializationCompleted?.Invoke(m_GameType);
      CountdownTimerStarted?.Invoke(TimerConstants.RoundTimerKey);
    }

    private void SetInitialized(bool isInitialized) {
      m_IsInitialized = !isInitialized;
    }

    private IEnumerator CompleteInitialization() {
      while (m_IsInitialized == false) {
        yield return null;
      }
      OnInitializationComplete();
    }
  }
}

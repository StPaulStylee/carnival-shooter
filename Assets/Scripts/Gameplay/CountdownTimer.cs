using Assets.Scripts.Data;
using CarnivalShooter.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour {
  public static event Action<float> TimerChanged;

  //private float m_totalTime = 30f;
  //private float m_timeRemaining;
  //private bool m_isRunning;

  private Dictionary<string, CountDownTimer> timerInstances = new();

  private void Awake() {
    GameManager.RoundDurationInitializing += SetTimer;
    GameManager.CountdownTimerStarted += StartTimer;
  }

  private void Start() {
    //TimerChanged?.Invoke(timeRemaining.ToString());
    //StartCoroutine(StartCountdown());
  }

  public void Pause(CountDownTimer timer) { timer.m_IsRunning = false; }

  private void SetTimer(string timerType, float time) {
    CountDownTimer timer = new CountDownTimer(time);
    timerInstances.Add(timerType, timer);
    //m_totalTime = time;
  }

  private void StartTimer(string timerKey) {
    bool hasTimer = timerInstances.TryGetValue(timerKey, out CountDownTimer timer);
    if (hasTimer) {
      StartCoroutine(StartCountdown(timer));
    }
  }

  private IEnumerator StartCountdown(CountDownTimer timer) {
    float timeRemaining = timer.m_TotalTime;
    timer.m_IsRunning = true;

    if (timeRemaining > 0 && !timer.m_IsRunning) {
      yield return null;
    }

    while (timeRemaining > 0f && timer.m_IsRunning) {
      yield return new WaitForSeconds(1f); // Wait for 1 second

      timeRemaining--;

      // Update UI or perform other actions based on the remaining time
      TimerChanged?.Invoke(timeRemaining);
    }

    // Countdown has reached zero, perform actions or end the game
    Debug.Log("Countdown Finished!");
  }

}

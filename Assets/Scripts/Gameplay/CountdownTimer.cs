using UnityEngine;
using System.Collections;
using CarnivalShooter.UI.Manager;
using System;
using CarnivalShooter.Managers;

public class CountdownTimer : MonoBehaviour {
  public static event Action<float> TimerChanged;

  private float m_totalTime = 30f;
  private float m_timeRemaining;
  private bool m_isRunning;

  private void Awake() {
    GameManager.RoundDurationInitializing += SetTimer;
  }

  private void Start() {
    //TimerChanged?.Invoke(m_timeRemaining.ToString());
    StartCoroutine(StartCountdown());
  }

  public void Pause() { m_isRunning = false; }

  private void SetTimer(float time) {
    m_totalTime = time;
  }

  private IEnumerator StartCountdown() {
    float m_timeRemaining = m_totalTime;
    m_isRunning = true;

    if (m_timeRemaining > 0 && !m_isRunning) {
      yield return null;
    }

    while (m_timeRemaining > 0f && m_isRunning) {
      yield return new WaitForSeconds(1f); // Wait for 1 second

      m_timeRemaining--;

      // Update UI or perform other actions based on the remaining time
      TimerChanged?.Invoke(m_timeRemaining);
      //Debug.Log("Remaining Time: " + m_timeRemaining);
    }

    // Countdown has reached zero, perform actions or end the game
    Debug.Log("Countdown Finished!");
  }

}

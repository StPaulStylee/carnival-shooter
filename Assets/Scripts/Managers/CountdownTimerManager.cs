using CarnivalShooter.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class CountdownTimerManager : MonoBehaviour {
    //public static event Action<float> TimerChanged;

    private Dictionary<string, CountDownTimer> timerInstances = new();

    private void Awake() {
      GameManager.CountdownTimerInitializing += SetTimer;
      GameManager.CountdownTimerStarted += StartTimer;
    }

    //public void Pause(CountDownTimer timer) { timer.m_IsRunning = false; }

    private void SetTimer(string timerType, float time) {
      CountDownTimer timer = new CountDownTimer(timerType, time);
      timerInstances.Add(timerType, timer);
    }

    private void StartTimer(string timerKey) {
      bool hasTimer = timerInstances.TryGetValue(timerKey, out CountDownTimer timer);
      if (hasTimer) {
        StartCoroutine(timer.StartCountdown());
      }
    }

    //private IEnumerator StartCountdown(CountDownTimer timer) {
    //  //float timeRemaining = timer.m_TotalTime;
    //  timer.m_IsRunning = true;

    //  if (timer.m_TimeRemaining > 0 && !timer.m_IsRunning) {
    //    yield return null;
    //  }

    //  while (timer.m_TimeRemaining > 0f && timer.m_IsRunning) {
    //    Debug.Log($"{timer.m_TimerType}: {timer.m_TimeRemaining}");
    //    yield return new WaitForSeconds(1f); // Wait for 1 second

    //    timer.m_TimeRemaining--;

    //    // Update UI or perform other actions based on the remaining time
    //    TimerChanged?.Invoke(timer.m_TimeRemaining);
    //  }

    //  // Countdown has reached zero, perform actions or end the game
    //  Debug.Log($"{timer.m_TimerType}: Countdown Finished!");
    //}
  }

}

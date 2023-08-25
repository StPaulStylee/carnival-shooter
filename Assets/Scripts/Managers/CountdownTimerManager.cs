using CarnivalShooter.Data.ScriptableObjects;
using CarnivalShooter.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class CountdownTimerManager : MonoBehaviour {
    //public static event Action<float> TimerChanged;
    [SerializeField] private CountdownTimerData_SO[] timers;

    private Dictionary<string, CountDownTimer> timerInstances = new();

    private void Awake() {
      //GameManager.CountdownTimerInitializing += SetTimer;
      GameManager.CountdownTimerStarted += StartTimer;
      SetAllTimers();
    }

    private void OnDisable() {
      //GameManager.CountdownTimerInitializing -= SetTimer;
      GameManager.CountdownTimerStarted -= StartTimer;
    }

    private void SetAllTimers() {
      foreach (var timer in timers) {
        SetTimer(timer.TimerType, timer.TotalTime, timer.PostCompletedDelay);
      }
    }

    private void SetTimer(string timerType, float time, float postCompletedDelay) {
      CountDownTimer timer = new CountDownTimer(timerType, time, postCompletedDelay);
      timerInstances.Add(timerType, timer);
    }

    private void StartTimer(string timerKey) {
      bool hasTimer = timerInstances.TryGetValue(timerKey, out CountDownTimer timer);
      if (hasTimer) {
        StartCoroutine(timer.StartCountdown());
      }
    }
  }
}

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
  }

}

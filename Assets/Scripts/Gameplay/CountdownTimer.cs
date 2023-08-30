using CarnivalShooter.Data;
using System;
using System.Collections;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class CountDownTimer {
    public static event Action<string, string> TimerChanged;
    public static event Action<bool> TimerBlockingExecution;
    public static event Action<string> TimerCompleted;
    public static event Action<string> TimerPostCompleted;

    private string m_TimerType;
    private float m_PostCompletedDelay;
    private float m_TimeRemaining;
    private bool m_IsRunning;
    private bool m_IsExecutionBlocking;
    private string m_IsCompletedMessage;

    public CountDownTimer(string timerType, float totalTime, float postCompletedDelay = 1f) {
      m_TimerType = timerType;
      m_PostCompletedDelay = postCompletedDelay;
      m_TimeRemaining = totalTime;
      m_IsRunning = false;
      m_IsExecutionBlocking = SetIsBlockingExecution(timerType);
      m_IsCompletedMessage = SetIsCompletedMessage(timerType);
    }

    public IEnumerator StartCountdown() {
      m_IsRunning = true;
      TimerChanged?.Invoke(m_TimerType, m_TimeRemaining.ToString());
      if (m_IsExecutionBlocking) {
        TimerBlockingExecution?.Invoke(true);
      }

      if (m_TimeRemaining > 0 && !m_IsRunning) {
        yield return null;
      }

      while (m_TimeRemaining > 0f && m_IsRunning) {
        yield return new WaitForSeconds(1f);

        m_TimeRemaining--;
        TimerChanged?.Invoke(m_TimerType, m_TimeRemaining.ToString());
      }

      while (m_TimeRemaining == 0f && m_IsRunning) {
        TimerChanged?.Invoke(m_TimerType, m_IsCompletedMessage);
        TimerCompleted?.Invoke(m_TimerType);
        m_IsRunning = false;
        yield return new WaitForSeconds(m_PostCompletedDelay);
      }

      // Countdown has reached zero, perform actions or end the game
      Debug.Log($"{m_TimerType}: Countdown Finished!");
      TimerPostCompleted?.Invoke(m_TimerType);
      TimerBlockingExecution?.Invoke(false);
    }

    public void Pause(CountDownTimer timer) { timer.m_IsRunning = false; }

    private bool SetIsBlockingExecution(string timerType) {
      if (timerType == TimerConstants.RoundStartCountdownKey) {
        return true;
      }
      return false;
    }

    private string SetIsCompletedMessage(string timerType) {
      if (timerType == TimerConstants.RoundTimerKey) {
        return "FINISH!";
      }
      return "START!";
    }
  }
}
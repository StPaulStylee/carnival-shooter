﻿using Assets.Scripts.Data;
using System;
using System.Collections;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class CountDownTimer {
    public static event Action<string, float> TimerChanged;
    public static event Action<bool> TimerBlockingExecution;
    private string m_TimerType;
    private float m_TotalTime;
    private float m_TimeRemaining;
    private bool m_IsRunning;
    private bool m_IsExecutionBlocking;
    public CountDownTimer(string timerType, float totalTime) {
      m_TimerType = timerType;
      m_TotalTime = totalTime;
      m_TimeRemaining = totalTime;
      m_IsRunning = false;
      m_IsExecutionBlocking = SetIsBlockingExecution(timerType);
    }

    public IEnumerator StartCountdown() {
      m_IsRunning = true;
      if (m_IsExecutionBlocking) {
        TimerBlockingExecution?.Invoke(true);
      }

      if (m_TimeRemaining > 0 && !m_IsRunning) {
        yield return null;
      }

      while (m_TimeRemaining > 0f && m_IsRunning) {
        yield return new WaitForSeconds(1f);

        m_TimeRemaining--;

        TimerChanged?.Invoke(m_TimerType, m_TimeRemaining);
      }

      while (m_TimeRemaining == 0f && m_IsRunning) {
        TimerChanged?.Invoke(m_TimerType, m_TimeRemaining);
        m_IsRunning = false;
        yield return new WaitForSeconds(1f);
      }

      // Countdown has reached zero, perform actions or end the game
      Debug.Log($"{m_TimerType}: Countdown Finished!");
      TimerBlockingExecution?.Invoke(false);
    }
    public void Pause(CountDownTimer timer) { timer.m_IsRunning = false; }

    private bool SetIsBlockingExecution(string timerType) {
      if (timerType == TimerConstants.RoundStartCountdownKey) {
        return true;
      }
      return false;
    }
  }
}
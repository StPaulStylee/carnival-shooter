namespace Assets.Scripts.Data {
  public struct CountDownTimer {
    public float m_TotalTime;
    public float m_TimeRemaining;
    public bool m_IsRunning;
    public CountDownTimer(float totalTime) {
      m_TotalTime = totalTime;
      m_TimeRemaining = totalTime;
      m_IsRunning = false;
    }
  }
}
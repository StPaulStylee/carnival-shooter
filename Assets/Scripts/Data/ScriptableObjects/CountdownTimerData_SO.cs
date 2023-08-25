using UnityEngine;

namespace CarnivalShooter.Data.ScriptableObjects {
  [CreateAssetMenu(fileName = "CountdownTimerData", menuName = "ScriptableObjects/CountdownTimerData")]
  public class CountdownTimerData_SO : ScriptableObject {
    [TimerType]
    public string TimerType;
    public float TotalTime;
    public float PostCompletedDelay;
  }
}

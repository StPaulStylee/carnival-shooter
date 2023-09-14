using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using UnityEngine;

namespace CarnivalShooter.UI.Manager {
  public class GameUIManager : MonoBehaviour {
    [SerializeField] private GameHud m_GameHud;
    [SerializeField] PostRoundStats m_PostRoundStats;

    private void Awake() {
      CountDownTimer.TimerPostCompleted += ShowPostRoundStats;
    }

    public void Start() {
      HideVisualAsset(m_PostRoundStats);
    }

    public void HideVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(false);
    }

    public void ShowVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(true);
    }

    private void ShowPostRoundStats(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        HideVisualAsset(m_GameHud);
        ShowVisualAsset(m_PostRoundStats);
      }
    }
  }
}

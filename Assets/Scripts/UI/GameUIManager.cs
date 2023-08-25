using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using UnityEngine;

namespace CarnivalShooter.UI.Manager {
  public class GameUIManager : MonoBehaviour {
    public GameHud GameHud;
    public PostRoundStats PostRoundStats;

    private void Awake() {
      CountDownTimer.TimerPostCompleted += ShowPostRoundStats;
    }

    public void Start() {
      HideVisualAsset(PostRoundStats);
    }

    public void HideVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(false);
    }

    public void ShowVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(true);
    }

    private void ShowPostRoundStats(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        HideVisualAsset(GameHud);
        ShowVisualAsset(PostRoundStats);
      }
    }
  }
}

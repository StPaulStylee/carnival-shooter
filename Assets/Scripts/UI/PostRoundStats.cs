using CarnivalShooter.Data;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class PostRoundStats : GameUIScreen {
    const string k_ShotsFiredData = "post-round-stats__shots-fired-data";
    const string k_BullseyeHitData = "post-round-stats__bullseye-hit-data";
    const string k_InnerZoneHitData = "post-round-stats__inner-zone-data";
    const string k_OuterZoneHitData = "post-round-stats__outer-zone-data";
    const string k_ShotsHitData = "post-round-stats__shots-hit-data";
    const string k_HitAccuracyData = "post-round-stats__hit-accuracy-data";
    const string k_RoundScoreData = "post-round-stats__round-score-data";

    private Label m_ShotsFiredDataLabel;
    private Label m_BullseyeHitDataLabel;
    private Label m_InnerZoneHitDataLabel;
    private Label m_OuterZoneHitDataLabel;
    private Label m_ShotsHitDataLabel;
    private Label m_HitAccuracyDataLabel;
    private Label m_RoundScoreDataLabel;

    private void Awake() {
      StatManager.PostRoundStatsCompleted += SetLabels;
    }

    private void OnEnable() {
      base.SetGameUIElements();
      m_ShotsFiredDataLabel = m_GameUIElement.Q<Label>(k_ShotsFiredData);
      m_BullseyeHitDataLabel = m_GameUIElement.Q<Label>(k_BullseyeHitData);
      m_InnerZoneHitDataLabel = m_GameUIElement.Q<Label>(k_InnerZoneHitData);
      m_OuterZoneHitDataLabel = m_GameUIElement.Q<Label>(k_OuterZoneHitData);
      m_ShotsHitDataLabel = m_GameUIElement.Q<Label>(k_ShotsHitData);
      m_HitAccuracyDataLabel = m_GameUIElement.Q<Label>(k_HitAccuracyData);
      m_RoundScoreDataLabel = m_GameUIElement.Q<Label>(k_RoundScoreData); ;
    }

    private void SetLabels(PostRoundStatsData data) {
      m_ShotsFiredDataLabel.text = data.TotalShotsFired.ToString();
      m_BullseyeHitDataLabel.text = data.TotalBullseyeHits.ToString();
      m_InnerZoneHitDataLabel.text = data.TotalInnerZoneHits.ToString();
      m_OuterZoneHitDataLabel.text = data.TotalOuterZoneHits.ToString();
      m_ShotsHitDataLabel.text = data.TotalShotsHit.ToString();
      m_HitAccuracyDataLabel.text = data.HitAccuracy.ToString() + "%";
      m_RoundScoreDataLabel.text = data.RoundScore.ToString();
    }
  }
}

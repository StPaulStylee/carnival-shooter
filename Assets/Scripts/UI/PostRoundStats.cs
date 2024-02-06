using CarnivalShooter.Data;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class PostRoundStats : GameUIScreen {
    const string k_BullseyeHitData = "post-round-stats__bullseye-hit-data";
    const string k_BullseyeScoreData = "post-round-stats__bullseye-score-data";
    const string k_InnerZoneHitData = "post-round-stats__inner-zone-hit-data";
    const string k_InnerZoneScoreData = "post-round-stats__inner-zone-score-data";
    const string k_OuterZoneHitData = "post-round-stats__outer-zone-hit-data";
    const string k_OuterZoneScoreData = "post-round-stats__outer-zone-score-data";
    const string k_StuffiesScoreData = "post-round-stats__stuffies-score-data";
    const string k_StuffiesHitData = "post-round-stats__stuffies-hit-data";
    const string k_ShotsHitData = "post-round-stats__shots-hit-data";
    const string k_ShotsFiredData = "post-round-stats__shots-fired-data";
    const string k_HitAccuracyData = "post-round-stats__hit-accuracy-data";
    const string k_RoundScoreData = "post-round-stats__round-score-data";
    const string k_RoundBonusData = "post-round-stats__bonus-data";
    const string k_TotalScoreData = "post-round-stats__round-total-score-data";
    const string k_PlayAgainButton = "post-round-stats__play-again";
    const string k_ExitToMenuButton = "post-round-stats__exit-to-menu";
    const string k_HighScoreContainer = "post-round-stats__high-score-container";

    private Label m_BullseyeHitDataLabel;
    private Label m_BulleyeScoreDataLabel;
    private Label m_InnerZoneHitDataLabel;
    private Label m_InnerZoneScoreDataLabel;
    private Label m_OuterZoneHitDataLabel;
    private Label m_OuterZoneScoreDataLabel;
    private Label m_StuffiesHitDataLabel;
    private Label m_StuffiesHitScoreLabel;
    private Label m_ShotsFiredDataLabel;
    private Label m_ShotsHitDataLabel;
    private Label m_HitAccuracyDataLabel;
    private Label m_RoundScoreDataLabel;
    private Label m_RoundBonusDataLabel;
    private Label m_TotalScoreDataLabel;
    private VisualElement m_HighScoreContainer;
    private Button m_PlayAgainButton;
    private Button m_ExitToMenuButton;

    private void Awake() {
      StatManager.PostRoundStatsCompleted += SetLabels;
    }

    private void OnDisable() {
      StatManager.PostRoundStatsCompleted -= SetLabels;
    }

    private void OnEnable() {
      base.SetGameUIElements();
      m_BullseyeHitDataLabel = m_GameUIElement.Q<Label>(k_BullseyeHitData);
      m_BulleyeScoreDataLabel = m_GameUIElement.Q<Label>(k_BullseyeScoreData);
      m_InnerZoneHitDataLabel = m_GameUIElement.Q<Label>(k_InnerZoneHitData);
      m_InnerZoneScoreDataLabel = m_GameUIElement.Q<Label>(k_InnerZoneScoreData);
      m_OuterZoneHitDataLabel = m_GameUIElement.Q<Label>(k_OuterZoneHitData);
      m_OuterZoneScoreDataLabel = m_GameUIElement.Q<Label>(k_OuterZoneScoreData);
      m_StuffiesHitDataLabel = m_GameUIElement.Q<Label>(k_StuffiesHitData);
      m_StuffiesHitScoreLabel = m_GameUIElement.Q<Label>(k_StuffiesScoreData);
      m_ShotsFiredDataLabel = m_GameUIElement.Q<Label>(k_ShotsFiredData);
      m_ShotsHitDataLabel = m_GameUIElement.Q<Label>(k_ShotsHitData);
      m_HitAccuracyDataLabel = m_GameUIElement.Q<Label>(k_HitAccuracyData);
      m_RoundScoreDataLabel = m_GameUIElement.Q<Label>(k_RoundScoreData);
      m_RoundBonusDataLabel = m_GameUIElement.Q<Label>(k_RoundBonusData);
      m_TotalScoreDataLabel = m_GameUIElement.Q<Label>(k_TotalScoreData);
      m_PlayAgainButton = m_GameUIElement.Q<Button>(k_PlayAgainButton);
      m_HighScoreContainer = m_GameUIElement.Q<VisualElement>(className: k_HighScoreContainer);
      m_PlayAgainButton.RegisterCallback<ClickEvent>(OnPlayAgain);
      m_ExitToMenuButton = m_GameUIElement.Q<Button>(k_ExitToMenuButton);
      m_ExitToMenuButton.RegisterCallback<ClickEvent>(OnExitToMenu);
    }

    private void OnPlayAgain(ClickEvent e) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnExitToMenu(ClickEvent e) {
      SceneManager.LoadSceneAsync(0);
    }

    private void SetLabels(PostRoundStatsData data) {
      m_BullseyeHitDataLabel.text = data.TotalBullseyeHits.ToString();
      m_BulleyeScoreDataLabel.text = data.TotalBullseyeScore.ToString();
      m_InnerZoneHitDataLabel.text = data.TotalInnerZoneHits.ToString();
      m_InnerZoneScoreDataLabel.text = data.TotalInnerZoneScore.ToString();
      m_OuterZoneHitDataLabel.text = data.TotalOuterZoneHits.ToString();
      m_OuterZoneScoreDataLabel.text = data.TotalOuterZoneScore.ToString();
      m_StuffiesHitDataLabel.text = data.TotalStuffieScore.ToString();
      m_StuffiesHitScoreLabel.text = data.TotalStuffieScore.ToString();
      m_ShotsFiredDataLabel.text = data.TotalShotsFired.ToString();
      m_ShotsHitDataLabel.text = data.TotalShotsHit.ToString();
      m_HitAccuracyDataLabel.text = data.HitAccuracy.ToString() + "%";
      m_RoundScoreDataLabel.text = data.RoundScore.ToString();
      m_RoundBonusDataLabel.text = data.RoundBonus.ToString();
      m_TotalScoreDataLabel.text = data.TotalScore.ToString();
      if (!data.IsHighScore) {
        m_HighScoreContainer.style.display = DisplayStyle.None;
      } else {
        m_HighScoreContainer.style.display = DisplayStyle.Flex;
      }
    }
  }
}

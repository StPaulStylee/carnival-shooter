using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class GameHud : GameUIScreen {
    const string k_ammoContainerName = "game-hud__ammo-container";
    const string k_ammoIconVisualElementName = "game-hud__ammo-icon";
    const string k_ammoEnabledClassName = "ammo-enabled";
    const string k_ammoDisabledClassName = "ammo-disabled";
    const string k_scoreLabelName = "game-hud__score-text";
    const string k_highScoreLabelName = "game-hud__high-score-text";
    const string k_roundDurationTimerLabelName = "game-hud__game-timer-text";
    const string k_roundStartDurationTimerLabelName = "game-hud__round-start-timer-text";

    [Tooltip("The UXML template to create ammo icons")]
    [SerializeField] private VisualTreeAsset m_ammoTemplate;

    private VisualElement m_AmmoContainer;
    private Label m_ScoreLabel;
    private Label m_RoundDurationLabel;
    private Label m_RoundStartDurationLabel;
    private Label m_HighScoreLabel;

    private Stack<VisualElement> m_EnabledAmmoIcons = new();
    private Stack<VisualElement> m_DisabledAmmoIcons = new();
    private void Awake() {
      GameManager.CountdownTimerStarted += ShowTimerLabel;
      CountDownTimer.TimerChanged += SetTimerLabel;
      GameManager.AmmoInitializing += SetAmmoIcons;
      CurrentWeapon.AmmoChanged += UpdateAmmoIcons;
      CurrentWeapon.AmmoReloaded += SetAmmoIcons;
      GameManager.ScoreInitializing += SetScoreLabel;
      StatManager.ScoreUpdated += SetScoreLabel;
      StatManager.HighScoreInitialized += SetHighScoreLabel;
    }

    private void OnDisable() {
      GameManager.CountdownTimerStarted -= ShowTimerLabel;
      CountDownTimer.TimerChanged -= SetTimerLabel;
      GameManager.AmmoInitializing -= SetAmmoIcons;
      CurrentWeapon.AmmoChanged -= UpdateAmmoIcons;
      CurrentWeapon.AmmoReloaded -= SetAmmoIcons;
      GameManager.ScoreInitializing -= SetScoreLabel;
      StatManager.ScoreUpdated -= SetScoreLabel;
      StatManager.HighScoreInitialized -= SetHighScoreLabel;
    }

    private void OnEnable() {
      base.SetGameUIElements();
      m_AmmoContainer = m_GameUIElement.Query<VisualElement>(k_ammoContainerName);
      m_ScoreLabel = m_GameUIElement.Query<Label>(k_scoreLabelName);
      m_RoundDurationLabel = m_GameUIElement.Query<Label>(k_roundDurationTimerLabelName);
      m_RoundStartDurationLabel = m_GameUIElement.Query<Label>(k_roundStartDurationTimerLabelName);
      m_HighScoreLabel = m_GameUIElement.Query<Label>(k_highScoreLabelName);
    }

    private void Start() {
      HideTimerLabels(new Label[] { m_RoundDurationLabel });
    }

    private void SetAmmoIcons(int ammoCount) {
      m_AmmoContainer.Clear();
      m_EnabledAmmoIcons.Clear();
      m_DisabledAmmoIcons.Clear();
      for (int i = 0; i < ammoCount; i++) {
        VisualElement ammoIcon = m_ammoTemplate.CloneTree();
        m_AmmoContainer.Add(ammoIcon);
        m_EnabledAmmoIcons.Push(ammoIcon);
      }
    }

    private void UpdateAmmoIcons() {
      VisualElement ammoIconToDisable;
      bool hasAmmoRemaining = m_EnabledAmmoIcons.TryPop(out ammoIconToDisable);
      if (hasAmmoRemaining) {
        VisualElement ammoIconVisualElement = ammoIconToDisable.Query<VisualElement>(k_ammoIconVisualElementName);
        ammoIconVisualElement.AddToClassList(k_ammoDisabledClassName);
        ammoIconVisualElement.RemoveFromClassList(k_ammoEnabledClassName);
        m_DisabledAmmoIcons.Push(ammoIconToDisable);
      }
    }

    private void SetScoreLabel(int score) {
      m_ScoreLabel.text = score.ToString();
    }


    private void SetTimerLabel(string timerType, string label) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        m_RoundDurationLabel.text = label;
        return;
      }
      m_RoundStartDurationLabel.text = label;
    }

    private void SetHighScoreLabel(int highScore) {
      m_HighScoreLabel.text = highScore.ToString();
    }

    private void ShowTimerLabel(string timerType) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        m_RoundDurationLabel.style.opacity = 1f;
        m_RoundStartDurationLabel.style.opacity = 0f;
        return;
      }
      m_RoundStartDurationLabel.style.opacity = 1f;
      m_RoundDurationLabel.style.opacity = 0f;
    }

    private void HideTimerLabels(Label[] labels) {
      foreach (Label label in labels) {
        label.style.opacity = 0f;
      }
    }
  }
}

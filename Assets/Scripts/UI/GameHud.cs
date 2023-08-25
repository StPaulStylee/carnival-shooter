using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class GameHud : GameUIScreen {
    const string ammoContainerName = "game-hud__ammo-container";
    const string ammoIconVisualElementName = "game-hud__ammo-icon";
    const string ammoEnabledClassName = "ammo-enabled";
    const string ammoDisabledClassName = "ammo-disabled";
    const string scoreLabelName = "game-hud__score-text";
    const string roundDurationTimerLabelName = "game-hud__game-timer-text";
    const string roundStartDurationTimerLabelName = "game-hud__round-start-timer-text";

    [Tooltip("The UXML template to create ammo icons")]
    [SerializeField] private VisualTreeAsset m_ammoTemplate;

    private VisualElement m_AmmoContainer;
    private Label m_ScoreLabel;
    private Label m_RoundDurationLabel;
    private Label m_RoundStartDurationLabel;

    private Stack<VisualElement> m_EnabledAmmoIcons = new();
    private Stack<VisualElement> m_DisabledAmmoIcons = new();
    private void Awake() {
      //GameManager.CountdownTimerInitializing += SetTimerLabel;
      //GameManager.CountdownTimerInitializing += HideTimerLabel;
      GameManager.CountdownTimerStarted += ShowTimerLabel;
      CountDownTimer.TimerChanged += SetTimerLabel;
      GameManager.AmmoInitializing += SetAmmoIcons;
      Weapon.AmmoChanged += UpdateAmmoIcons;
      Weapon.AmmoReloaded += SetAmmoIcons;
      GameManager.ScoreInitializing += SetScoreLabel;
      StatManager.ScoreUpdated += SetScoreLabel;
    }

    private void OnDisable() {
      //GameManager.CountdownTimerInitializing -= SetTimerLabel;
      //GameManager.CountdownTimerInitializing -= HideTimerLabel;
      GameManager.CountdownTimerStarted -= ShowTimerLabel;
      CountDownTimer.TimerChanged -= SetTimerLabel;
      GameManager.AmmoInitializing -= SetAmmoIcons;
      Weapon.AmmoChanged -= UpdateAmmoIcons;
      Weapon.AmmoReloaded -= SetAmmoIcons;
      GameManager.ScoreInitializing -= SetScoreLabel;
      StatManager.ScoreUpdated -= SetScoreLabel;
    }

    private void OnEnable() {
      base.SetGameUIElements();
      m_AmmoContainer = m_GameUIElement.Query<VisualElement>(ammoContainerName);
      m_ScoreLabel = m_GameUIElement.Query<Label>(scoreLabelName);
      m_RoundDurationLabel = m_GameUIElement.Query<Label>(roundDurationTimerLabelName);
      m_RoundStartDurationLabel = m_GameUIElement.Query<Label>(roundStartDurationTimerLabelName);
    }

    private void Start() {
      HideTimerLabels();
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
        VisualElement ammoIconVisualElement = ammoIconToDisable.Query<VisualElement>(ammoIconVisualElementName);
        ammoIconVisualElement.AddToClassList(ammoDisabledClassName);
        ammoIconVisualElement.RemoveFromClassList(ammoEnabledClassName);
        m_DisabledAmmoIcons.Push(ammoIconToDisable);
      }
    }

    private void SetScoreLabel(int score) {
      m_ScoreLabel.text = score.ToString();
    }

    //private void SetTimerLabel(string label) {
    //  m_RoundDurationLabel.text = label;
    //}

    private void SetTimerLabel(string timerType, string label) {
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        m_RoundDurationLabel.text = label;
        return;
      }
      //if (timerType.Equals(TimerConstants.RoundTimerKey)) {
      //  m_RoundDurationLabel.text = label.ToString();
      //  return;
      //}
      //if (label == 0f) {
      //  m_RoundStartDurationLabel.text = "START!";
      //  return;
      //}
      m_RoundStartDurationLabel.text = label;
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

    private void HideTimerLabels() {
      //m_RoundStartDurationLabel.style.opacity = 0f;
      m_RoundDurationLabel.style.opacity = 0f;
    }
  }
}

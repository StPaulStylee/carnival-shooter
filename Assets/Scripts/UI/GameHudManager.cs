using Assets.Scripts.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI.Manager {
  public class GameHudManager : MonoBehaviour {
    const string ammoContainerName = "game-hud__ammo-container";
    const string ammoIconVisualElementName = "game-hud__ammo-icon";
    const string ammoEnabledClassName = "ammo-enabled";
    const string ammoDisabledClassName = "ammo-disabled";
    const string scoreLabelName = "game-hud__score-text";
    const string roundDurationTimerLabelName = "game-hud__game-timer-text";
    const string roundStartDurationTimerLabelName = "game-hud__round-start-timer-text";

    [Header("UXML Documents/Templates")]
    [Tooltip("The Root UI Document")]
    [SerializeField] private UIDocument m_UIDocument;
    [Tooltip("The UXML template to create ammo icons")]
    [SerializeField] private VisualTreeAsset m_ammoTemplate;

    private VisualElement m_AmmoContainer;
    private Label m_ScoreLabel;
    private Label m_RoundDurationLabel;
    private Label m_RoundStartDurationLabel;

    private Stack<VisualElement> m_EnabledAmmoIcons = new();
    private Stack<VisualElement> m_DisabledAmmoIcons = new();
    private void Awake() {
      GameManager.CountdownTimerInitializing += SetTimerLabel;
      GameManager.CountdownTimerInitializing += HideTimerLabel;
      GameManager.CountdownTimerStarted += ShowTimerLabel;
      CountDownTimer.TimerChanged += SetTimerLabel;
      GameManager.AmmoInitializing += SetAmmoIcons;
      Weapon.AmmoChanged += UpdateAmmoIcons;
      Weapon.AmmoReloaded += SetAmmoIcons;
      GameManager.ScoreInitializing += SetScoreLabel;
      StatManager.ScoreUpdated += SetScoreLabel;
    }

    private void OnDisable() {
      GameManager.CountdownTimerInitializing -= SetTimerLabel;
      GameManager.CountdownTimerInitializing -= HideTimerLabel;
      GameManager.CountdownTimerStarted -= ShowTimerLabel;
      CountDownTimer.TimerChanged -= SetTimerLabel;
      GameManager.AmmoInitializing -= SetAmmoIcons;
      Weapon.AmmoChanged -= UpdateAmmoIcons;
      Weapon.AmmoReloaded -= SetAmmoIcons;
      GameManager.ScoreInitializing -= SetScoreLabel;
      StatManager.ScoreUpdated -= SetScoreLabel;
    }

    private void OnEnable() {
      var rootElement = m_UIDocument.rootVisualElement;
      m_AmmoContainer = rootElement.Query<VisualElement>(ammoContainerName);
      m_ScoreLabel = rootElement.Query<Label>(scoreLabelName);
      m_RoundDurationLabel = rootElement.Query<Label>(roundDurationTimerLabelName);
      m_RoundStartDurationLabel = rootElement.Query<Label>(roundStartDurationTimerLabelName);
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

    private void SetTimerLabel(float label) {
      m_RoundDurationLabel.text = label.ToString();
    }

    private void SetTimerLabel(string timerType, float label) {
      if (timerType.Equals(TimerConstants.RoundTimerKey) && label == 0f) {
        m_RoundDurationLabel.text = "FINISH!";
        return;
      }
      if (timerType.Equals(TimerConstants.RoundTimerKey)) {
        m_RoundDurationLabel.text = label.ToString();
        return;
      }
      if (label == 0f) {
        m_RoundStartDurationLabel.text = "START!";
        return;
      }
      m_RoundStartDurationLabel.text = label.ToString();
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

    private void HideTimerLabel(string _, float label) {
      m_RoundStartDurationLabel.style.opacity = 0f;
      m_RoundDurationLabel.style.opacity = 0f;
    }
  }
}

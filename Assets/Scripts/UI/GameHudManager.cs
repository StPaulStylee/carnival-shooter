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
    const string timerLabelName = "game-hud__game-timer-text";

    //public static event Action<string> TimerChanged;
    [Header("UXML Documents/Templates")]
    [Tooltip("The Root UI Document")]
    [SerializeField] private UIDocument m_UIDocument;
    [Tooltip("The UXML template to create ammo icons")]
    [SerializeField] private VisualTreeAsset m_ammoTemplate;

    private VisualElement m_AmmoContainer;
    private Label m_ScoreLabel;
    private Label m_TimerLabel;

    private Stack<VisualElement> m_EnabledAmmoIcons = new();
    private Stack<VisualElement> m_DisabledAmmoIcons = new();
    private void Awake() {
      GameManager.RoundDurationInitializing += SetTimerLabel;
      CountdownTimer.TimerChanged += SetTimerLabel;
      GameManager.AmmoInitializing += SetAmmoIcons;
      Weapon.AmmoChanged += UpdateAmmoIcons;
      Weapon.AmmoReloaded += SetAmmoIcons;
      GameManager.ScoreInitializing += SetScoreLabel;
      GameManager.ScoreUpdated += SetScoreLabel;
    }

    private void OnEnable() {
      var rootElement = m_UIDocument.rootVisualElement;
      m_AmmoContainer = rootElement.Query<VisualElement>(ammoContainerName);
      m_ScoreLabel = rootElement.Query<Label>(scoreLabelName);
      m_TimerLabel = rootElement.Query<Label>(timerLabelName);
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

    private void SetScoreLabel(int label) {
      m_ScoreLabel.text = label.ToString();
    }

    private void SetTimerLabel(float label) {
      m_TimerLabel.text = label.ToString();
    }

    private void SetTimerLabel(string _, float label) {
      m_TimerLabel.text = label.ToString();
    }
  }
}


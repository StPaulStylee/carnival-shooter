using CarnivalShooter.Gameplay;
using CarnivalShooter.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI.Manager {
  public class GameHudManager : MonoBehaviour {
    const string ammoLabelName = "game-hud__ammo-count-text";
    const string scoreLabelName = "game-hud__score-text";
    const string timerLabelName = "game-hud__game-timer-text";

    //public static event Action<string> TimerChanged;

    [SerializeField] private UIDocument m_UIDocument;

    private Label m_AmmoLabel;
    private Label m_ScoreLabel;
    private Label m_TimerLabel;

    private void Awake() {
      GameManager.RoundDurationInitializing += SetTimerLabel;
      CountdownTimer.TimerChanged += SetTimerLabel;
      GameManager.AmmoInitializing += SetAmmoLabel;
      Weapon.AmmoChanged += SetAmmoLabel;
      GameManager.ScoreInitializing += SetScoreLabel;
      GameManager.ScoreUpdated += SetScoreLabel;
    }

    private void OnEnable() {
      var rootElement = m_UIDocument.rootVisualElement;
      m_AmmoLabel = rootElement.Query<Label>(ammoLabelName);
      m_ScoreLabel = rootElement.Query<Label>(scoreLabelName);
      m_TimerLabel = rootElement.Query<Label>(timerLabelName);    
    }

    private void SetAmmoLabel(int label) {
      m_AmmoLabel.text = $"x{label}";
    }

    private void SetScoreLabel(int label) {
      print("SetScoreLabel");
      m_ScoreLabel.text = label.ToString();
    }

    private void SetTimerLabel(float label) {
      m_TimerLabel.text = label.ToString();
    }
  }
}


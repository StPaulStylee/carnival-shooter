using CarnivalShooter.Data;
using CarnivalShooter.Gameplay;
using CarnivalShooter.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(Shootable))]
  public class Scoreable : MonoBehaviour {
    public static event Action<int> PointsScored;
    Transform m_PointsEarnedSpawnTransform;

    [ScoreValue]
    public int m_Score;

    private void Awake() {
      m_PointsEarnedSpawnTransform = GetComponentInParent<Transform>();
      Debug.Log(m_PointsEarnedSpawnTransform.name);
    }

    public void OnPointsScored(Vector3 popupPosition) {
      PointsScored?.Invoke(m_Score);
      PointsEarnedPopup.Create(popupPosition, m_Score);
    }
  }
}

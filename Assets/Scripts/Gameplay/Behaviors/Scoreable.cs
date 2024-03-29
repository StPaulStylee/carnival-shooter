using CarnivalShooter.Data;
using CarnivalShooter.UI;
using System;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(Shootable))]
  public class Scoreable : MonoBehaviour {
    public static event Action<int, string> PointsScored;

    Transform m_PointsEarnedSpawnTransform;

    [ScoreableLabel]
    public string m_Label;

    [ScoreValue]
    public int m_Score;

    [ColorValue]
    public string m_ColorHex;
    private Color m_Color;

    private void Awake() {
      ColorUtility.TryParseHtmlString(m_ColorHex, out m_Color);
      m_PointsEarnedSpawnTransform = GetComponentInParent<Transform>();
    }

    public void OnPointsScored(Vector3 popupPosition) {
      PointsScored?.Invoke(m_Score, m_Label);
      PointsEarnedPopup.Create(popupPosition, m_Score, m_Color);
    }
  }
}

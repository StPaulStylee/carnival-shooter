using CarnivalShooter.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CarnivalShooter.UI {
  public class PointsEarnedPopup : MonoBehaviour {
    private TextMeshPro m_PointsEarnedText;
    private Color m_textColor;
    [Header("Movement Configuration")]
    [Tooltip("The speed at which the popup will move on the Y Axis after instantiating")]
    [SerializeField] private float m_MoveYSpeed = 1f;
    [Tooltip("The amount of time before the popup begins to fade away")]
    [SerializeField] private float m_Lifetime = 2f;
    [Tooltip("The speed at which the popup fades away after it's lifetime as expired")]
    [SerializeField] private float m_FadeawaySpeed = 2f;
    private void Awake() {
      m_PointsEarnedText = GetComponent<TextMeshPro>();
    }

    private void Update() {
      transform.position += new Vector3(0, m_MoveYSpeed, 0) * Time.deltaTime;
      m_Lifetime -= Time.deltaTime;
      if (m_Lifetime < 0 ) {
        m_textColor.a -= m_FadeawaySpeed * Time.deltaTime;
        m_PointsEarnedText.color = m_textColor;
      }
      if (m_textColor.a < 0) {
        Destroy(gameObject);
      }
    }

    public static PointsEarnedPopup Create(Vector3 position, int pointsEarned) {
      Transform pointsEarnedTransform = Instantiate(GameAssets.i.m_PointsEarnedPopupPrefab, position, Quaternion.AngleAxis(180f, Vector3.up));
      PointsEarnedPopup popup = pointsEarnedTransform.GetComponent<PointsEarnedPopup>();
      popup.Setup(pointsEarned);
      return popup;
    }

    public void Setup(int pointsAmount) {
      m_PointsEarnedText.text = pointsAmount.ToString();
      m_textColor = m_PointsEarnedText.color;
    }
  }
}

using CarnivalShooter.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsEarnedTesting : MonoBehaviour
{
  [SerializeField] private Transform m_PointsEarnedPrefab;
  private void Start() {
    Transform pointsEarnedTransform = Instantiate(m_PointsEarnedPrefab, new Vector3(0, 3.21f), Quaternion.AngleAxis(180f, Vector3.up));
    pointsEarnedTransform.GetComponent<PointsEarnedPopup>().Setup(124);
  }
}

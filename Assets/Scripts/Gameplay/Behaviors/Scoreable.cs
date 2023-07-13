using CarnivalShooter.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(Shootable))]
  public class Scoreable : MonoBehaviour {
    public static event Action<int> PointsScored;
    [SerializeField] private int m_Score;
    
    public void OnPointsScored() {
      PointsScored?.Invoke(m_Score);
    }
  }
}

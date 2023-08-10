using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Utilities {
  public class GameAssets : MonoBehaviour {
    public Transform m_PointsEarnedPopupPrefab;
    private static GameAssets _i;
    public static GameAssets i {
      get {
        if (_i == null) {
          _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
        }
        return _i;
      }
    }
  }
}

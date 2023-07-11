using CarnivalShooter.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay {
  public class TargetSegment : Target {
    [SerializeField] private int score;
    public override void TakeShot(RaycastHit hitInfo) {
      base.TakeShot(hitInfo);
      OnPointsScored(score);
    }
  }
}

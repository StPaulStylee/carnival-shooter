using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Behavior {
  [RequireComponent(typeof(Animator))]
  public abstract class ShotAnimatable : MonoBehaviour {
    [SerializeField ]protected Animator m_Animator;

    public abstract void PlayTakeShot();
    public abstract void ResetToDefault();
  }
}

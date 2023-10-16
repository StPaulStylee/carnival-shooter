using CarnivalShooter.Managers;
using CarnivalShooter.Managers.Data;
using UnityEngine;

namespace CarnivalShooter.Gameplay.Audio {
  [RequireComponent(typeof(AudioSource))]
  public abstract class AudioCore : MonoBehaviour {
    protected virtual void Awake() {
      AudioManager.AudioSettingsChanged += SetSfxVolume;
    }

    protected virtual void OnDisable() {
      AudioManager.AudioSettingsChanged -= SetSfxVolume;
    }

    protected abstract void SetSfxVolume(AudioSettingsData data);
  }
}

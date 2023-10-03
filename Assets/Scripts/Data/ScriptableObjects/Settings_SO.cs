using UnityEngine;

namespace CarnivalShooter.Data.ScriptableObjects {
  [CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
  public class Settings_SO : ScriptableObject {
    public bool IsAudioEnabled = true;
    public bool IsLookInverted = false;
    [Range(0, 100)] public int LookSensitivity = 80;
    [Range(0, 100)] public int GameplaySfxVolume = 80;
    [Range(0, 100)] public int MusicSfxVolume = 80;
    [Range(0, 100)] public int BackgroundSfxVolume = 80;
  }
}

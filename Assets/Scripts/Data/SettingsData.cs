using System;
using UnityEngine;
namespace CarnivalShooter.Data {
  [Serializable]
  public class SettingsData {
    public bool IsAudioEnabled = true;
    public bool IsLookInverted = false;
    [Range(0, 100)] public int LookSensitivity = 3;
    [Range(0, 100)] public int GameplaySfxVolume = 80;
    [Range(0, 100)] public int MusicSfxVolume = 25;
    [Range(0, 100)] public int BackgroundSfxVolume = 55;
    [Range(0, 100)] public int UiSfxVolume = 80;
    public SettingsData() { }
  }
}

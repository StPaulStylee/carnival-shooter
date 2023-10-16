using System;
using UnityEngine;
namespace CarnivalShooter.Data {
  [Serializable]
  public class SettingsData {
    public bool IsAudioEnabled = true;
    public bool IsLookInverted = false;
    //[Range(0, 100)] public int LookSensitivity = 80;
    //[Range(0, 100)] public int GameplaySfxVolume = 80;
    //[Range(0, 100)] public int MusicSfxVolume = 30;
    //[Range(0, 100)] public int BackgroundSfxVolume = 50;
    [Range(0, 100)] public int LookSensitivity = 80;
    [Range(0, 100)] public int GameplaySfxVolume = 80;
    [Range(0, 100)] public int MusicSfxVolume = 30;
    [Range(0, 100)] public int BackgroundSfxVolume = 55;
    public SettingsData() { }
  }
}
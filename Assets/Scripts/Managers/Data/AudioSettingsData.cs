namespace CarnivalShooter.Managers.Data {
  public struct AudioSettingsData {
    public bool IsAllAudioEnabled;
    public bool IsLookInversionAudioEnabled;
    public float GameplaySfxVolume;
    public float MusicSfxVolume;
    public float BackgroundSfxVolume;
    public float UiSfxVolume;
    public AudioSettingsData(bool isAudioEnabled, bool isLookInversionAudioEnabled, float gameplaySfxVolume, float musicSfxVolume, float backgroundSfxVolume, float uiSfxVolume) {
      IsAllAudioEnabled = isAudioEnabled;
      IsLookInversionAudioEnabled = isLookInversionAudioEnabled;
      GameplaySfxVolume = gameplaySfxVolume;
      MusicSfxVolume = musicSfxVolume;
      BackgroundSfxVolume = backgroundSfxVolume;
      UiSfxVolume = uiSfxVolume;
    }
  }
}
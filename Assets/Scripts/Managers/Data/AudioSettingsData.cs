namespace CarnivalShooter.Managers.Data {
  public struct AudioSettingsData {
    public bool IsAudioEnabled;
    public float GameplaySfxVolume;
    public float MusicSfxVolume;
    public float BackgroundSfxVolume;
    public AudioSettingsData(bool isAudioEnabled, float gameplaySfxVolume, float musicSfxVolume, float backgroundSfxVolume) {
      IsAudioEnabled = isAudioEnabled;
      GameplaySfxVolume = gameplaySfxVolume;
      MusicSfxVolume = musicSfxVolume;
      BackgroundSfxVolume = backgroundSfxVolume;
    }
  }
}
using CarnivalShooter.Data;
using CarnivalShooter.Managers.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarnivalShooter.Gameplay.Audio {
  public class SceneAudio : AudioCore {
    public static SceneAudio Instance;
    [SerializeField] private AudioSource m_InGameMusicSource;
    [SerializeField] private AudioSource m_InGameBackgroundAudioSource;
    [SerializeField] private AudioSource m_MainMenuMusicAudioSource;

    private string m_CurrentSceneName;
    private float m_MusicSfxVolume;
    private float m_BackgroundSfxVolume;

    protected override void Awake() {
      if (Instance == null) {
        base.Awake();
        Instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
      } else {
        Destroy(gameObject);
      }
    }

    //private void OnEnable() {
    //  SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    protected override void OnDisable() {
      base.OnDisable();
      SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //private void Start() {
    //  m_InGameMusicSource.Play();
    //  m_InGameBackgroundAudioSource.Play();
    //}

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
      if (m_CurrentSceneName == scene.name) {
        return;
      }
      if (m_CurrentSceneName == null || m_CurrentSceneName != scene.name) {
        m_CurrentSceneName = scene.name;
        StopAllAudio();
        if (scene.name.Equals(SceneNameConstants.Game)) {
          m_InGameMusicSource.Play();
          m_InGameBackgroundAudioSource.Play();
          return;
        }
        if (scene.name.Equals(SceneNameConstants.MainMenu)) {
          m_MainMenuMusicAudioSource.Play();
          return;
        }
      }
      Debug.LogWarning($"This scene isn't configured in {this.name}");
    }

    private void PlayAudio() {
      if (m_InGameMusicSource != null) {
        m_InGameMusicSource.Play();
      }
      if (m_InGameBackgroundAudioSource != null) {
        m_InGameBackgroundAudioSource.Play();
      }
    }

    protected override void SetSfxVolume(AudioSettingsData data) {
      m_MusicSfxVolume = data.MusicSfxVolume;
      m_BackgroundSfxVolume = data.BackgroundSfxVolume;
      m_InGameMusicSource.volume = m_MusicSfxVolume;
      m_InGameBackgroundAudioSource.volume = m_BackgroundSfxVolume;
    }

    private void StopAllAudio() {
      m_InGameMusicSource.Stop();
      m_InGameBackgroundAudioSource.Stop();
      m_MainMenuMusicAudioSource.Stop();
    }
  }
}

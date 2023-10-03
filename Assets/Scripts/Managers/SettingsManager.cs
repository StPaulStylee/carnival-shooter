using CarnivalShooter.Data.ScriptableObjects;
using System;
using UnityEngine;

namespace CarnivalShooter.Managers {
  public class SettingsManager : MonoBehaviour {
    [SerializeField] private Settings_SO SettingsData;

    public static event Action<Settings_SO> OnSettingsInitialized;
    // Start is called before the first frame update
    void Start() {
      OnSettingsInitialized?.Invoke(SettingsData);
    }

    // Update is called once per frame
    void Update() {

    }
  }
}

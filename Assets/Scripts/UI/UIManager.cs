using System.Collections.Generic;
using UnityEngine;

namespace CarnivalShooter.UI.Manager {
  public abstract class UIManager : MonoBehaviour {
    [SerializeField] protected List<GameUIScreen> m_GameUIScreens;

    protected void HideVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(false);
    }

    protected void ShowVisualAsset(GameUIScreen screen) {
      screen.SetVisibility(true);
    }

    protected GameUIScreen ShowSingleVisualElementByName(string name) {
      foreach (GameUIScreen screen in m_GameUIScreens) {
        HideVisualAsset(screen);
      }
      GameUIScreen asset = m_GameUIScreens.Find(screen => screen.GameHudElementName == name);
      ShowVisualAsset(asset);
      return asset;
    }
  }
}

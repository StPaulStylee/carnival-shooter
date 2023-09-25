using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public abstract class GameUIScreen : MonoBehaviour {
    [HideInInspector] public string GameHudElementName => m_GameHudElementName;
    [Header("UXML Documents/Templates")]
    [Tooltip("The Root UI Document")]
    [SerializeField] protected UIDocument m_UIDocument;

    [Header("Game UI Screen Name")]
    [Tooltip("The name of the screen that will be used to Query the UI Document")]
    [SerializeField] protected string m_GameHudElementName;

    protected VisualElement m_RootElement;
    protected VisualElement m_GameUIElement;

    public void SetVisibility(bool isVisible) {
      if (isVisible) {
        m_GameUIElement.style.display = DisplayStyle.Flex;
        return;
      }
      m_GameUIElement.style.display = DisplayStyle.None;
    }

    protected virtual void SetGameUIElements() {
      if (m_RootElement == null) {
        m_RootElement = m_UIDocument.rootVisualElement;
      }
      m_GameUIElement = m_RootElement.Q<VisualElement>(m_GameHudElementName);
    }
  }
}

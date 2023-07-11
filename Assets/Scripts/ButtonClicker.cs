using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonClicker : MonoBehaviour
{
  private UIDocument buttonDocument;
  private Button uiButton;
  private void OnEnable() {
    buttonDocument = GetComponent<UIDocument>();
    if (buttonDocument == null) {
      Debug.LogError("No buttonDocument found.");
    }
    uiButton = buttonDocument.rootVisualElement.Q("TestButton") as Button;

    if (uiButton != null) {
      Debug.Log("Button Found!");
    }

    uiButton.RegisterCallback<ClickEvent>(OnButtonClick);
  }

  private void OnButtonClick(ClickEvent e) {
    Debug.Log("Clicked");
  }
}

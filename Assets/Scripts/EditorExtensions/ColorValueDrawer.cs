#if UNITY_EDITOR
using CarnivalShooter.Data;
using UnityEditor;
using UnityEngine;

namespace CarnivalShooter.EditorExtensions {
  [CustomPropertyDrawer(typeof(ColorValueAttribute))]
  public class ColorValueDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      EditorGUI.BeginProperty(position, label, property);

      // Get the string values from ScoreConstants
      string[] values = new string[]
      {
        ScoreConstants.Green,
        ScoreConstants.Yellow,
        ScoreConstants.Orange
      };

      string[] options = new string[] {
        nameof(ScoreConstants.Green),
        nameof(ScoreConstants.Yellow),
        nameof(ScoreConstants.Orange),
      };

      // Find the index of the currently selected value
      int selectedIndex = -1;
      string currentValue = property.stringValue;
      for (int i = 0; i < values.Length; i++) {
        if (values[i].Equals(currentValue)) {
          selectedIndex = i;
          break;
        }
      }

      // Display the dropdown in the Inspector
      int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, options);

      // If the selected value changed, update the property value
      if (newIndex != selectedIndex) {
        property.stringValue = values[newIndex];
      }

      EditorGUI.EndProperty();
    }
  }
}
#endif

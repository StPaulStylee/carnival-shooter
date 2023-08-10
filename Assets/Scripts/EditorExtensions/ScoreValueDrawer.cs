#if UNITY_EDITOR
using CarnivalShooter.Data;
using UnityEditor;
using UnityEngine;

namespace CarnivalShooter.EditorExtensions {
  [CustomPropertyDrawer(typeof(ScoreValueAttribute))]
  public class ScoreValueDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      EditorGUI.BeginProperty(position, label, property);

      // Get the integer values from ScoreConstants
      int[] values = new int[]
      {
        ScoreConstants.Five,
        ScoreConstants.Ten,
        ScoreConstants.Fifteen
      };

      // Convert them to strings for the dropdown options
      string[] options = new string[values.Length];
      for (int i = 0; i < values.Length; i++) {
        options[i] = values[i].ToString();
      }

      // Find the index of the currently selected value
      int selectedIndex = -1;
      int currentValue = property.intValue;
      for (int i = 0; i < values.Length; i++) {
        if (values[i] == currentValue) {
          selectedIndex = i;
          break;
        }
      }

      // Display the dropdown in the Inspector
      int newIndex = EditorGUI.Popup(position, label.text, selectedIndex, options);

      // If the selected value changed, update the property value
      if (newIndex != selectedIndex) {
        property.intValue = values[newIndex];
      }

      EditorGUI.EndProperty();
    }
  }
}
#endif

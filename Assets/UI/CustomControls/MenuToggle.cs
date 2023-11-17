using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI.CustomControls {
  public class MenuToggle : BaseField<bool> {
    public new class UxmlFactory : UxmlFactory<MenuToggle, UxmlTraits> { }
    public new class UxmlTraits : BaseFieldTraits<bool, UxmlBoolAttributeDescription> { }

    // The "new" keyword is used here becase "BaseField" has these exact same variables
    // and we want our custom instance to have the same ones. We could eliminate the use of "new"
    // by simply using our own unique variable names for these assignments
    public static readonly new string ussClassName = "menu-toggle";
    public static readonly new string inputUssClassName = "menu-toggle__input";
    public static readonly string inputKnobUssClassName = "menu-toggle__input-knob";
    public static readonly string inputCheckedUssClassName = "menu-toggle__input--checked";
    public static readonly string inputUnCheckedUssClassName = "menu-toggle__input--unchecked";

    VisualElement m_Input;
    VisualElement m_Knob;

    // Custom controls need a default constructor with no parameters. This default constructor calls the other constructor in this
    // class. this(null) is calling the constructor directly below.
    public MenuToggle() : this(null) { }

    public MenuToggle(string label) : base(label, null) {
      AddToClassList(ussClassName);
      // Get the BaseField's visual input element and use it as the background of the slide.
      // This is Querying the the BaseFields element using the className assigned to its own instance of
      // "inputUssClassName"
      m_Input = this.Q(className: BaseField<bool>.inputUssClassName);
      //labelElement.AddToClassList("font__riffic-free--white");
      m_Input.AddToClassList(inputUssClassName);
      Add(m_Input);


      // Create a "knob" child element for the background to represent the actual slide of the toggle.
      m_Knob = new();
      m_Knob.AddToClassList(inputKnobUssClassName);
      m_Input.Add(m_Knob);


      var labelElement = this.Q(className: labelUssClassName);
      //labelElement.AddToClassList("font__riffic-free--white");
      // There are three main ways to activate or deactivate the SlideToggle. All three event handlers use the
      // static function pattern described in the Custom control best practices.

      // ClickEvent fires when a sequence of pointer down and pointer up actions occurs.
      RegisterCallback<ClickEvent>(evt => OnClick(evt));
      // KeydownEvent fires when the field has focus and a user presses a key.
      RegisterCallback<KeyDownEvent>(evt => OnKeydownEvent(evt));
      // NavigationSubmitEvent detects input from keyboards, gamepads, or other devices at runtime.
      RegisterCallback<NavigationSubmitEvent>(evt => OnSubmit(evt));
    }

    static void OnClick(ClickEvent evt) {
      var menuToggle = evt.currentTarget as MenuToggle;
      menuToggle.ToggleValue();

      evt.StopPropagation();
    }

    static void OnSubmit(NavigationSubmitEvent evt) {
      var menuToggle = evt.currentTarget as MenuToggle;
      menuToggle.ToggleValue();

      evt.StopPropagation();
    }

    static void OnKeydownEvent(KeyDownEvent evt) {
      var menuToggle = evt.currentTarget as MenuToggle;

      // NavigationSubmitEvent event already covers keydown events at runtime, so this method shouldn't handle
      // them.
      if (menuToggle.panel?.contextType == ContextType.Player)
        return;

      // Toggle the value only when the user presses Enter, Return, or Space.
      if (evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space) {
        menuToggle.ToggleValue();
        evt.StopPropagation();
      }
    }

    // All three callbacks call this method.
    void ToggleValue() {
      value = !value;
    }

    // Because ToggleValue() sets the value property, the BaseField class dispatches a ChangeEvent. This results in a
    // call to SetValueWithoutNotify(). This example uses it to style the toggle based on whether it's currently
    // enabled.
    public override void SetValueWithoutNotify(bool newValue) {
      base.SetValueWithoutNotify(newValue);

      //This line of code styles the input element to look enabled or disabled.
      m_Input.EnableInClassList(inputUnCheckedUssClassName, !newValue);
      m_Input.EnableInClassList(inputCheckedUssClassName, newValue);
    }
  }
}
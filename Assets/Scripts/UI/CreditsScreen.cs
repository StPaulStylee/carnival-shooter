using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace CarnivalShooter.UI {
  public class CreditsScreen : GameUIScreen {
    public static event Action BackButtonClicked;

    const string k_BackButton = "credits-screen--back-button";
    const string k_DblPlusGoodWeb = "credits-screen--logo-website";
    const string k_DblPlusGoodX = "credits-screen--logo-x";
    const string k_DblPlusGoodInsta = "credits-screen--logo-instagram";
    const string k_SmashIcons = "smashicons";
    const string k_AbdulAllib = "abdul-allib";
    const string k_Freepik = "freepik";
    const string k_PixelPerfect = "pixelperfect";
    const string k_AlfanSubekti = "alfan-subekti";
    const string k_NawIcon = "nawicon";

    private VisualElement m_Web, m_X, m_Insta, m_BackButton;
    private Label m_AbdulAllib, m_SmashIcons, m_Freepik, m_PixelPerfect, m_AlfanSubekti, m_NawIcon;
    private void OnEnable() {
      base.SetGameUIElements();
      m_BackButton = m_GameUIElement.Q<VisualElement>(k_BackButton);
      m_Web = m_GameUIElement.Q<VisualElement>(k_DblPlusGoodWeb);
      m_X = m_GameUIElement.Q<VisualElement>(k_DblPlusGoodX);
      m_Insta = m_GameUIElement.Q<VisualElement>(k_DblPlusGoodInsta);
      m_AbdulAllib = m_GameUIElement.Q<Label>(k_AbdulAllib);
      m_SmashIcons = m_GameUIElement.Q<Label>(k_SmashIcons);
      m_Freepik = m_GameUIElement.Q<Label>(k_Freepik);
      m_PixelPerfect = m_GameUIElement.Q<Label>(k_PixelPerfect);
      m_AlfanSubekti = m_GameUIElement.Q<Label>(k_AlfanSubekti);
      m_NawIcon = m_GameUIElement.Q<Label>(k_NawIcon);

      m_BackButton.RegisterCallback<ClickEvent>(OnBackButtonClicked);
      m_Web.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.doubleplusgood.studio");
      m_X.RegisterCallback<ClickEvent, string>(OnElementClick, "https://twitter.com/Dbl_Plus_Good");
      m_Insta.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.instagram.com/dbl_plus_good/");
      m_AbdulAllib.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/abdul-allib");
      m_SmashIcons.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/smashicons");
      m_Freepik.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/freepik");
      m_PixelPerfect.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/pixel-perfect");
      m_AlfanSubekti.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/alfan-subekti");
      m_NawIcon.RegisterCallback<ClickEvent, string>(OnElementClick, "https://www.flaticon.com/authors/nawicon");
    }

    private void OnElementClick(ClickEvent e, string url) {
      Application.OpenURL(url);
    }

    private void OnBackButtonClicked(ClickEvent e) {
      BackButtonClicked?.Invoke();
    }
  }
}

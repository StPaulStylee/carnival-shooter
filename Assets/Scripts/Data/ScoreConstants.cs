using UnityEngine;

namespace CarnivalShooter.Data {
  public static class ScoreConstants {
    public const int One = 1;
    public const int Three = 3;
    public const int Five = 5;
    public const int Seven = 7;
    public const int Ten = 10;
    public const int Fifteen = 15;
    public const int Twenty = 20;

    public const string Green = "#085405";
    public const string Yellow = "#EBEC0D";
    public const string Orange = "#EA2811";
    public const string White = "#FFFFFF";

    public const string BullseyeLabel = "Bullseye";
    public const string OuterZoneLabel = "OuterZone";
    public const string InnerZoneLabel = "InnerZone";
    public const string StuffieLabel = "Stuffy";
  }

  // Custom property attribute to display the integer constants as a dropdown in the Inspector
  public class ScoreValueAttribute : PropertyAttribute { }
  public class ColorValueAttribute : PropertyAttribute { }
  public class ScoreableLabelAttribute : PropertyAttribute { }

}

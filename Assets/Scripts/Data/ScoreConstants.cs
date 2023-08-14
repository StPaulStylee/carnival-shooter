using UnityEngine;

namespace CarnivalShooter.Data {
  public static class ScoreConstants {
    public const int Five = 5;
    public const int Ten = 10;
    public const int Fifteen = 15;

    public const string Green = "#085405";
    public const string Yellow = "#EBEC0D";
    public const string Orange = "#EA2811";
  }

  // Custom property attribute to display the integer constants as a dropdown in the Inspector
  public class ScoreValueAttribute : PropertyAttribute { }
  public class ColorValueAttribute : PropertyAttribute { }

}

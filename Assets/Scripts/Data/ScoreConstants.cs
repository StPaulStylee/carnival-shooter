using UnityEngine;

namespace CarnivalShooter.Data {
  public static class ScoreConstants {
    public const int Five = 5;
    public const int Ten = 10;
    public const int Fifteen = 15;
  }

  // Custom property attribute to display the integer constants as a dropdown in the Inspector
  public class ScoreValueAttribute : PropertyAttribute { }
}

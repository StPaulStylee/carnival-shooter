namespace CarnivalShooter.Data {
  public struct PostRoundStatsData {
    public int TotalBullseyeHits;
    public int TotalBullseyeScore;
    public int TotalInnerZoneHits;
    public int TotalInnerZoneScore;
    public int TotalOuterZoneHits;
    public int TotalOuterZoneScore;
    public int TotalStuffieScore;
    public int TotalShotsFired;
    public int TotalShotsHit;
    public float HitAccuracy;
    public int RoundBonus;
    public int RoundScore;
    public int TotalScore;
    public bool IsHighScore;
    public PostRoundStatsData(int totalBullseyeHits, int totalBullseyeScore, int totalInnerZoneHits, int totalInnerZoneScore, int totalOuterZoneHits, int totalOuterZoneScore, int totalStuffieScore, int totalShotsFired, int totalHits, float accuracy, int roundBonus, int roundScore, int totalScore, bool isHighScore) {
      TotalBullseyeHits = totalBullseyeHits;
      TotalBullseyeScore = totalBullseyeScore;
      TotalInnerZoneHits = totalInnerZoneHits;
      TotalInnerZoneScore = totalInnerZoneScore;
      TotalOuterZoneHits = totalOuterZoneHits;
      TotalOuterZoneScore = totalOuterZoneScore;
      TotalStuffieScore = totalStuffieScore;
      TotalShotsFired = totalShotsFired;
      TotalShotsHit = totalHits;
      HitAccuracy = accuracy;
      RoundBonus = roundBonus;
      RoundScore = roundScore;
      TotalScore = totalScore;
      IsHighScore = isHighScore;
    }
  }
}

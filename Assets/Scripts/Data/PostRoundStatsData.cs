namespace CarnivalShooter.Data {
  public struct PostRoundStatsData {
    public int TotalShotsFired;
    public int TotalBullseyeHits;
    public int TotalInnerZoneHits;
    public int TotalOuterZoneHits;
    public int TotalShotsHit;
    public float HitAccuracy;
    public int RoundScore;
    public PostRoundStatsData(int totalShotsFired, int totalBullseye, int totalInnerZone, int totalOuterZone, int totalHits, float accuracy, int roundScore) {
      TotalShotsFired = totalShotsFired;
      TotalBullseyeHits = totalBullseye;
      TotalInnerZoneHits = totalInnerZone;
      TotalOuterZoneHits = totalOuterZone;
      TotalShotsHit = totalHits;
      HitAccuracy = accuracy;
      RoundScore = roundScore;
    }
  }
}

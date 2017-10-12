namespace DataAccess.Models.Services.FantasyData
{
    public class GamePitching
    {
        public PitcherStatistics win { get; set; }
        public PitcherStatistics loss { get; set; }
        public PitcherStatistics save { get; set; }
        public PitcherStatistics[] hold { get; set; }
        public PitcherStatistics[] blown_save { get; set; }
    }
}

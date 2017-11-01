namespace Fantasy.API.DataAccess.Models.Services.FantasyData
{
    public class TeamStatistics
    {
        public string name { get; set; }
        public string market { get; set; }
        public string abbr { get; set; }
        public string id { get; set; }
        public int runs { get; set; }
        public int hits { get; set; }
        public int errors { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public Pitcher probable_pitcher { get; set; }
        public Pitcher starting_pitcher { get; set; }
        public Roster[] roster { get; set; }
        public Lineup[] lineup { get; set; }
        public Scoring[] scoring { get; set; }
        public StatisticsPlayer statistics { get; set; }
        public PlayerStatistic[] players { get; set; }
    }

}

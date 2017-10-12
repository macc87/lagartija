namespace DataAccess.Models.Services.FantasyData
{
    public class Player
    {
        public string id { get; set; }
        public string status { get; set; }
        public string position { get; set; }
        public string primary_position { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string jersey_number { get; set; }
        public string preferred_name { get; set; }        
    }

    public class PlayerStatistic : Player
    {
        public StatisticsPlayer statistics { get; set; }
    }

    public class PlayerInjury : Player
    {
        public Injury[] injuries { get; set; }
    }
}

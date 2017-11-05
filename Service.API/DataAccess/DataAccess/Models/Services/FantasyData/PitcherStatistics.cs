namespace Fantasy.API.DataAccess.Models.Services.FantasyData
{
    public class PitcherStatistics
    {
        public string id { get; set; }
        public string status { get; set; }
        public string position { get; set; }
        public string primary_position { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string jersey_number { get; set; }
        public string preferred_name { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public int save { get; set; }
        public int hold { get; set; }
        public int blown_save { get; set; }
    }
}

namespace DataAccess.Models.Services.FantasyData
{
    public class Team
    {
        public string name { get; set; }
        public string abbr { get; set; }
        public string market { get; set; }
        public string id { get; set; }
    }

    public class TeamInjury : Team
    {
        public PlayerInjury[] players { get; set; }
    }
}

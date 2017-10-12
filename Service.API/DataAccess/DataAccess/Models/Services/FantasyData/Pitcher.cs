namespace DataAccess.Models.Services.FantasyData
{
    public class Pitcher
    {
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string preferred_name { get; set; }
        public string jersey_number { get; set; }
        public string id { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public float era { get; set; }
    }
}

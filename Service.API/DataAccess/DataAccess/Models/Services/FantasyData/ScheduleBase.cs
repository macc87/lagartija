namespace DataAccess.Models.Services.FantasyData
{
    public class ScheduleBase
    {
        public string date { get; set; }
        public League league { get; set; }
        public GameSchedule[] games { get; set; }
        public string _comment { get; set; }
    }
}

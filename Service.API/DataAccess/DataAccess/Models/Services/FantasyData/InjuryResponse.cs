namespace Fantasy.API.DataAccess.Models.Services.FantasyData
{
    public class InjuryResponse
    {
        public League league { get; set; }
        public TeamInjury[] teams { get; set; }
        public string _comment { get; set; }
    }
}

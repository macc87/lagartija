namespace DataAccess.Models.Services.FantasyData
{
    public class StealHitter : Steal
    {
        public float pct { get; set; }
    }

    public class Steal
    {
        public int caught { get; set; }
        public int stolen { get; set; }
        public int pickoff { get; set; }
    }    
}

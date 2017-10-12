using System;
using System.Collections.Generic;

namespace DataAccess.Models.Services.FantasyData
{
    public class Game
    {
        public string id { get; set; }
        public string status { get; set; }
        public string coverage { get; set; }
        public int attendance { get; set; }
        public string duration { get; set; }
        public int game_number { get; set; }
        public string day_night { get; set; }
        public DateTime scheduled { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public Venue venue { get; set; }
        public Broadcast broadcast { get; set; }
    }

    public class GameSchedule : Game
    {
        public Team home { get; set; }
        public Team away { get; set; }
    }

    public class GameSummary : Game
    {
        public Final final { get; set; }
        public TeamStatistics home { get; set; }
        public TeamStatistics away { get; set; }
        public GamePitching pitching { get; set; }
        public IEnumerable<Official> officials { get; set; }
    }

    public class Final
    {
        public int inning { get; set; }
        public string inning_half { get; set; }
    }
}

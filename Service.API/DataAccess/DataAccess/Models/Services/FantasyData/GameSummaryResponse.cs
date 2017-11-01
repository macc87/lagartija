namespace Fantasy.API.DataAccess.Models.Services.FantasyData
{
    public class GameSummaryResponse
    {
        public GameSummary game { get; set; }
        public string _comment { get; set; }
    }

    public class HittingTeam
    {
        public HittingOverall overall { get; set; }
    }

    public class HittingOverall
    {
        public int ab { get; set; }
        public int lob { get; set; }
        public int rbi { get; set; }
        public float abhr { get; set; }
        public float abk { get; set; }
        public int bip { get; set; }
        public float babip { get; set; }
        public float bbk { get; set; }
        public float bbpa { get; set; }
        public float iso { get; set; }
        public float obp { get; set; }
        public float ops { get; set; }
        public float seca { get; set; }
        public float slg { get; set; }
        public int xbh { get; set; }
        public int pitch_count { get; set; }
        public int lob_risp_2out { get; set; }
        public int team_lob { get; set; }
        public int ab_risp { get; set; }
        public int hit_risp { get; set; }
        public int rbi_2out { get; set; }
        public int linedrive { get; set; }
        public int groundball { get; set; }
        public int popup { get; set; }
        public int flyball { get; set; }
        public int ap { get; set; }
        public string avg { get; set; }
        public float gofo { get; set; }
        public Onbase onbase { get; set; }
        public Runs runs { get; set; }
        public Outcome outcome { get; set; }
        public Outs outs { get; set; }
        public StealHitter steal { get; set; }
    }

    public class Onbase
    {
        public int s { get; set; }
        public int d { get; set; }
        public int t { get; set; }
        public int hr { get; set; }
        public int tb { get; set; }
        public int bb { get; set; }
        public int ibb { get; set; }
        public int hbp { get; set; }
        public int fc { get; set; }
        public int roe { get; set; }
        public int h { get; set; }
        public int cycle { get; set; }
    }

    public class Runs
    {
        public int total { get; set; }
    }

    public class Outs
    {
        public int po { get; set; }
        public int fo { get; set; }
        public int fidp { get; set; }
        public int lo { get; set; }
        public int lidp { get; set; }
        public int go { get; set; }
        public int gidp { get; set; }
        public int klook { get; set; }
        public int kswing { get; set; }
        public int ktotal { get; set; }
        public int sacfly { get; set; }
        public int sachit { get; set; }
    }

    public class Pitching
    {
        public Overall1 overall { get; set; }
    }

    public class Overall1
    {
        public float oba { get; set; }
        public int lob { get; set; }
        public float era { get; set; }
        public float k9 { get; set; }
        public float whip { get; set; }
        public float kbb { get; set; }
        public int pitch_count { get; set; }
        public int wp { get; set; }
        public int bk { get; set; }
        public int ip_1 { get; set; }
        public float ip_2 { get; set; }
        public int bf { get; set; }
        public float gofo { get; set; }
        public Onbase1 onbase { get; set; }
        public Runs1 runs { get; set; }
        public Outcome outcome { get; set; }
        public Outs1 outs { get; set; }
        public Steal steal { get; set; }
        public Games games { get; set; }
    }

    public class Onbase1
    {
        public int s { get; set; }
        public int d { get; set; }
        public int t { get; set; }
        public int hr { get; set; }
        public int tb { get; set; }
        public int bb { get; set; }
        public int ibb { get; set; }
        public int hbp { get; set; }
        public int fc { get; set; }
        public int roe { get; set; }
        public int h { get; set; }
    }

    public class Runs1
    {
        public int total { get; set; }
        public int unearned { get; set; }
        public int earned { get; set; }
    }

    public class Outs1
    {
        public int po { get; set; }
        public int fo { get; set; }
        public int fidp { get; set; }
        public int lo { get; set; }
        public int lidp { get; set; }
        public int go { get; set; }
        public int gidp { get; set; }
        public int klook { get; set; }
        public int kswing { get; set; }
        public int ktotal { get; set; }
        public int sacfly { get; set; }
        public int sachit { get; set; }
    }

    public class Games
    {
        public int svo { get; set; }
        public int qstart { get; set; }
        public int shutout { get; set; }
        public int team_shutout { get; set; }
        public int complete { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public int save { get; set; }
        public int hold { get; set; }
        public int blown_save { get; set; }
    }

    public class Errors
    {
        public int throwing { get; set; }
        public int fielding { get; set; }
        public int interference { get; set; }
        public int total { get; set; }
    }

    public class Roster
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

    public class Lineup
    {
        public string id { get; set; }
        public int inning { get; set; }
        public int order { get; set; }
        public int position { get; set; }
        public int sequence { get; set; }
        public string inning_half { get; set; }
    }

    public class HittingPlayer
    {
        public Overall3 overall { get; set; }
    }

    public class Overall3
    {
        public int ab { get; set; }
        public int lob { get; set; }
        public int rbi { get; set; }
        public float abhr { get; set; }
        public float abk { get; set; }
        public int bip { get; set; }
        public float babip { get; set; }
        public float bbk { get; set; }
        public float bbpa { get; set; }
        public float iso { get; set; }
        public float obp { get; set; }
        public float ops { get; set; }
        public float seca { get; set; }
        public float slg { get; set; }
        public int xbh { get; set; }
        public int pitch_count { get; set; }
        public int lob_risp_2out { get; set; }
        public int team_lob { get; set; }
        public int ab_risp { get; set; }
        public int hit_risp { get; set; }
        public int rbi_2out { get; set; }
        public int linedrive { get; set; }
        public int groundball { get; set; }
        public int popup { get; set; }
        public int flyball { get; set; }
        public int ap { get; set; }
        public string avg { get; set; }
        public float gofo { get; set; }
        public Onbase2 onbase { get; set; }
        public Runs2 runs { get; set; }
        public Outcome outcome { get; set; }
        public Outs2 outs { get; set; }
        public StealHitter steal { get; set; }
        public Games1 games { get; set; }
    }

    public class Onbase2
    {
        public int s { get; set; }
        public int d { get; set; }
        public int t { get; set; }
        public int hr { get; set; }
        public int tb { get; set; }
        public int bb { get; set; }
        public int ibb { get; set; }
        public int hbp { get; set; }
        public int fc { get; set; }
        public int roe { get; set; }
        public int h { get; set; }
        public int cycle { get; set; }
    }

    public class Runs2
    {
        public int total { get; set; }
    }

    public class Outs2
    {
        public int po { get; set; }
        public int fo { get; set; }
        public int fidp { get; set; }
        public int lo { get; set; }
        public int lidp { get; set; }
        public int go { get; set; }
        public int gidp { get; set; }
        public int klook { get; set; }
        public int kswing { get; set; }
        public int ktotal { get; set; }
        public int sacfly { get; set; }
        public int sachit { get; set; }
    }

    public class Games1
    {
        public int start { get; set; }
        public int play { get; set; }
        public int finish { get; set; }
        public int complete { get; set; }
    }

    public class Pitching1
    {
        public Overall4 overall { get; set; }
    }

    public class Overall4
    {
        public float oba { get; set; }
        public int lob { get; set; }
        public float era { get; set; }
        public float k9 { get; set; }
        public float whip { get; set; }
        public float kbb { get; set; }
        public int pitch_count { get; set; }
        public int wp { get; set; }
        public int bk { get; set; }
        public int ip_1 { get; set; }
        public float ip_2 { get; set; }
        public int bf { get; set; }
        public float gofo { get; set; }
        public Onbase3 onbase { get; set; }
        public Runs3 runs { get; set; }
        public Outcome outcome { get; set; }
        public Outs3 outs { get; set; }
        public Steal steal { get; set; }
        public Games2 games { get; set; }
    }

    public class Onbase3
    {
        public int s { get; set; }
        public int d { get; set; }
        public int t { get; set; }
        public int hr { get; set; }
        public int tb { get; set; }
        public int bb { get; set; }
        public int ibb { get; set; }
        public int hbp { get; set; }
        public int fc { get; set; }
        public int roe { get; set; }
        public int h { get; set; }
    }

    public class Runs3
    {
        public int total { get; set; }
        public int unearned { get; set; }
        public int earned { get; set; }
    }

    

    public class Outs3
    {
        public int po { get; set; }
        public int fo { get; set; }
        public int fidp { get; set; }
        public int lo { get; set; }
        public int lidp { get; set; }
        public int go { get; set; }
        public int gidp { get; set; }
        public int klook { get; set; }
        public int kswing { get; set; }
        public int ktotal { get; set; }
        public int sacfly { get; set; }
        public int sachit { get; set; }
    }



    public class Games2
    {
        public int start { get; set; }
        public int play { get; set; }
        public int finish { get; set; }
        public int complete { get; set; }
        public int svo { get; set; }
        public int qstart { get; set; }
        public int shutout { get; set; }
        public int win { get; set; }
        public int loss { get; set; }
        public int save { get; set; }
        public int hold { get; set; }
        public int blown_save { get; set; }
        public int team_win { get; set; }
        public int team_loss { get; set; }
    }

    public class Fielding1
    {
        public Overall5 overall { get; set; }
    }

    public class Overall5
    {
        public int po { get; set; }
        public int a { get; set; }
        public int dp { get; set; }
        public int tp { get; set; }
        public int error { get; set; }
        public int tc { get; set; }
        public float fpct { get; set; }
        public int c_wp { get; set; }
        public int pb { get; set; }
        public float rf { get; set; }
        public Errors errors { get; set; }
        public Games1 games { get; set; }
    }
    public class Fielding
    {
        public Overall2 overall { get; set; }
    }

    public class Overall2
    {
        public int po { get; set; }
        public int a { get; set; }
        public int dp { get; set; }
        public int tp { get; set; }
        public int error { get; set; }
        public int tc { get; set; }
        public float fpct { get; set; }
        public int c_wp { get; set; }
        public int pb { get; set; }
        public Errors errors { get; set; }
    }
}

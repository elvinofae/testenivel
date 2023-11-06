using Newtonsoft.Json;

public class Program
{
    private const string url = $"https://jsonmock.hackerrank.com/api/football_matches";

    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        string year = "2013";
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = "2014";
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        //// Output expected:
        //// Team Paris Saint - Germain scored 109 goals in 2013
        //// Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, string year)
    {
        int pages = TotalPages(year, team);
        int goalsTeam1 = TotalGoalsTeam1(year, team, pages);
        int goalsTeam2 = TotalGoalsTeam2(year, team, pages);
        return goalsTeam1 + goalsTeam2;
    }

    static int TotalPages(string year, string teamName) 
    {
        var pages = 0;

        using (HttpClient client = new())
        {
            var response = client.GetAsync($"{url}?year={year}&team1={teamName}").Result;
            if (response.IsSuccessStatusCode)
            {
                var contentString = response.Content.ReadAsStringAsync().Result;
                var content = JsonConvert.DeserializeObject<TotalPagesResponse>(contentString);

                if (content != null)                
                    pages = content.Total_Pages;
                
            }
        }

        return pages;
    }

    static int TotalGoalsTeam1(string year, string teamName, int pages)
    {
        var result = new List<int>();

        for (var page = 1; page <= pages; page++)
        {
            using (HttpClient client = new())
            {
                var response = client.GetAsync($"{url}?year={year}&team1={teamName}&page={page}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var contentString = response.Content.ReadAsStringAsync().Result;
                    var content = JsonConvert.DeserializeObject<TeamResponse>(contentString);

                    if (content != null && content.Data != null && content.Data.Any())
                    {
                        result.AddRange(content.Data.Select(data => data.Team1Goals));
                    }
                }
            }
        }

        return result.Sum();
    }

    static int TotalGoalsTeam2(string year, string teamName, int pages)
    {
        var result = new List<int>();

        for (var page = 1; page <= pages; page++)
        {
            using (HttpClient client = new())
            {
                var response = client.GetAsync($"{url}?year={year}&team2={teamName}&page={page}").Result;
                if (response.IsSuccessStatusCode)
                {
                    var contentString = response.Content.ReadAsStringAsync().Result;
                    var content = JsonConvert.DeserializeObject<TeamResponse>(contentString);

                    if (content != null && content.Data != null && content.Data.Any())
                    {
                        result.AddRange(content.Data.Select(data => data.Team2Goals));
                    }
                }
            }
        }

        return result.Sum();
    }

    class TotalPagesResponse
    {
        public int Total_Pages { get; set; }
    }

    class TeamResponse
    {
        public List<TeamData>? Data { get; set; }
    }

    class TeamData
    {
        public int Team1Goals { get; set; }
        public int Team2Goals { get; set; }
    }
}
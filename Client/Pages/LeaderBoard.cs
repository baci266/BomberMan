using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BomberMan.Shared;
using Microsoft.AspNetCore.Components;

namespace BomberMan.Client.Pages
{
    public partial class LeaderBoard
    {
        [Inject] private HttpClient Http { get; set; }
        private List<PlayerScore> Leaderboard { get; set; } = new List<PlayerScore>();

        protected override async Task OnInitializedAsync()
        {
            Leaderboard = await Http.GetFromJsonAsync<List<PlayerScore>>("Score/GetLeaderboard");
        }
    }
}
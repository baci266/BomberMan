using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BomberMan.Client.Data;
using BomberMan.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BomberMan.Client.Pages
{
    public partial class Game
    {
        #region Parameters
        [Parameter]
        public string UserNick { get; set; }
        
        [Parameter]
        public string Level { get; set; }
        #endregion

        #region Dependency Injection
        [Inject] private IJSRuntime JSRuntime {get; set; }
        
        [Inject] private GameUniverse GameUniverse { get; set; }
        
        [Inject] private HttpClient Http { get; set; }
        
        [Inject] private NavigationManager NavManager { get; set; }
        #endregion
        private List<GameElement> ToRender { get; } = new List<GameElement>();
        
        protected ElementReference GameDiv;
        
        private bool _isFocusSet = false;

        private GameState GameState = GameState.Playing;
        
        private List<PlayerScore> PlayerScores { get; set; } = new List<PlayerScore>();

        private PlayerScore PlayerScore { get; set; }
        private string ElapsedTime { get; set; }

        private bool _createScoreSent = false;
        
        private void Move(KeyboardEventArgs args)
        {
            GameUniverse.KeyPressed = args;
        }
        
        private void StopMove()
        {
            GameUniverse.KeyPressed = null;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_isFocusSet)
            {
                await JSRuntime.InvokeVoidAsync("SetFocusToElement", GameDiv);
                // set focus only once
                _isFocusSet = true;
            }
        }
        
        protected override async Task OnInitializedAsync()
        {
            var charMap = await Http.GetFromJsonAsync<char[][]>("DataFetcher/LoadMap?level=" + Level);
            PlayerScores = await Http.GetFromJsonAsync<List<PlayerScore>>("Score/GetScoreBoard?level=" + Level);
            GameUniverse.StartGame(charMap);
            GameUniverse.RenderMethod += Render;
        }

        protected async Task CreatePlayerScore()
        {
            PlayerScore = new PlayerScore()
            {
                UserNick = UserNick,
                DateTime = new DateTime(),
                Level = int.Parse(Level),
                TimeElapsed = GameUniverse.GameLogic.GameTime.TotalMilliseconds
            };
            await Http.PostAsJsonAsync("Score/Create", PlayerScore);
        }

        public void Render(object sender, EventArgs eventArgs)
        {
            var toRender = GameUniverse.GameLogic.AllElements;
            lock(ToRender) 
            {
                ToRender.Clear();
                ToRender.AddRange(toRender);
            }

            GameState = GameUniverse.GameLogic.GameState;
            if (GameState == GameState.Win && !_createScoreSent)
            {
                CreatePlayerScore();
                _createScoreSent = true;
            }
            ElapsedTime = GameUniverse.GameLogic.GameTime.GetFormattedElapsedTime();
            InvokeAsync( StateHasChanged );
        }

        private void RestartLevel()
        {
            NavManager.NavigateTo($"/game/{UserNick}/{Level}", true);
        }
        
        private void NextLevel()
        {
            int nextLevel = Int32.Parse(Level);
            nextLevel++;
            NavManager.NavigateTo($"/game/{UserNick}/{nextLevel}", true);
        }

        private void NewGame()
        {
            NavManager.NavigateTo("/");
        }
    }
}
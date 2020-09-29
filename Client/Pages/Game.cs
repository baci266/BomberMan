using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BomberMan.Client.Data;
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
        
        private void Move(KeyboardEventArgs args)
        {
            GameUniverse.KeyPressed = args;
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
            GameUniverse.StartGame(charMap);
            GameUniverse.RenderMethod += Render;
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
            InvokeAsync( StateHasChanged );
        }

        private void RestartLevel()
        {
            NavManager.NavigateTo($"/game/{UserNick}/{Level}");
        }

        private void NewGame()
        {
            NavManager.NavigateTo("/");
        }
    }
}
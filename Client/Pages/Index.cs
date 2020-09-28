using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BomberMan.Client.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager NavManager { get; set; }

        private string UserNick { set; get; } = "";

        private void StartGame()
        {
            if (string.IsNullOrWhiteSpace(UserNick)) return;
            
            NavManager.NavigateTo($"/game/{UserNick}/1");
        }
    }
}
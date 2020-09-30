using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace BomberMan.Client.Data
{
    public class GameUniverse
    {
        public const int Fps = 30;
        
        public static int FpsDelay => (int) 1000.0 / Fps;
        
        public bool IsGameRunning { get; set; }
        
        public static event EventHandler RenderMethod;
        
        public KeyboardEventArgs KeyPressed;

        public GameLogic GameLogic;
        
        public void StartGame(char[][] map)
        { 
            GameLogic = new GameLogic(map);
            IsGameRunning = true;
            GameLoop();
        }
        
        public async void GameLoop()
        {
            Stopwatch stopWatch = new Stopwatch();
            while (IsGameRunning)
            {
                stopWatch.Reset();
                stopWatch.Start();
                this.OnTick();
                stopWatch.Stop();
                long timeElapsed = stopWatch.ElapsedMilliseconds;
                int delay = (int) (FpsDelay - timeElapsed);
                await Task.Delay(delay > 1 ? delay : 1);
            }
        }

        private void OnTick()
        {
            KeyboardEventArgs keyPressed = KeyPressed;
            KeyPressed = null;
            
            GameLogic.MakeMove(keyPressed);
            RenderMethod?.Invoke(this, EventArgs.Empty);
        }
    }
}
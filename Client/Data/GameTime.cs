using System;
using System.Diagnostics;

namespace BomberMan.Client.Data
{
    public class GameTime
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
        }
        
        public long TotalMilliseconds => _stopwatch.ElapsedMilliseconds;

        public string GetFormattedElapsedTime()
        {
            TimeSpan ts = _stopwatch.Elapsed;

            return String.Format("{1:00}:{2:00}.{3:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
    }
}
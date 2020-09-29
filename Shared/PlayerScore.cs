using System;

namespace BomberMan.Shared
{
    public class PlayerScore
    {
        public int Id { get; set; }
        
        public string UserNick { get; set; }
        
        public int Level { get; set; }
        
        public long TimeElapsed { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}
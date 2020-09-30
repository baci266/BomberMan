using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BomberMan.Server.Data;
using BomberMan.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace BomberMan.Server.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class ScoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public ScoreController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("[action]")]
        public List<PlayerScore> GetScoreBoard(string level)
        {
            int.TryParse(level, out int intLevel);
            
            var query = from ps in _context.PlayerScores
                where ps.Level == intLevel
                orderby ps.TimeElapsed ascending
                select ps;
   
            return query.Take(10).ToList();
        }
        
        [HttpPost("[action]")]
        public void Create([FromBody] PlayerScore playerScore)
        {
            _context.Add(playerScore);
            _context.SaveChangesAsync();
        }
        
        [HttpGet("[action]")]
        public List<PlayerScore> GetLeaderboard()
        {
            var query = from ps in _context.PlayerScores
                orderby ps.Level ascending, ps.TimeElapsed ascending
                select ps;
   
            return query.ToList();
        }
    }
}
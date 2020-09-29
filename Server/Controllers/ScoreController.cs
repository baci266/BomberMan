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
        [Inject] private ApplicationDbContext Context { get; set; }

        [HttpGet("[action]")]
        public List<PlayerScore> GetScoreBoard(string level)
        {
            var query = from ps in Context.PlayerScores
                orderby ps.TimeElapsed ascending
                select ps;
   
            return query.Take(10).ToList();
        }
        
        [HttpPost("[action]")]
        public void Create([FromBody] PlayerScore playerScore)
        {
            Context.Add(playerScore);
        }
        
    }
}
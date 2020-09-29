using System.Collections.Generic;
using System.IO;
using System.Text;
using BomberMan.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BomberMan.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreController : ControllerBase
    {

        [HttpGet("[action]")]
        public List<PlayerScore> GetScoreBoard(string level)
        {
            return new List<PlayerScore>();
        }
        
        [HttpPost("[action]")]
        public void Create([FromBody] PlayerScore playerScore)
        {
            /*if (ModelState.IsValid)
                this.employee.AddEmployee(employee);*/
        }
        
    }
}
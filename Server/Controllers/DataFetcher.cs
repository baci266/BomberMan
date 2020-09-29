using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BomberMan.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFetcher : ControllerBase
    {

        [HttpGet("[action]")]
        public char[][] LoadMap(string level)
        {
            var path = $"plan{level}.txt";

            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
            
            int width = int.Parse(sr.ReadLine());
            int height = int.Parse(sr.ReadLine());

            var map = new char[height][];
            

            for (int i = 0; i < height; i++)
            {
                string line = sr.ReadLine();
                map[i] = new char[width];
                for (int j = 0; j < width; j++)
                {
                    map[i][j] = line[j];
                }
            }
            sr.Close();

            return map;
        }
    }
}
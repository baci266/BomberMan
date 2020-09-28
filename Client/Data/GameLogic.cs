using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BomberMan.Client.Data
{
    public class GameLogic
    {
        private readonly Dictionary<string, Direction> directionKeys = new Dictionary<string, Direction>()
        {
            {"KeyW", Direction.Up},
            {"KeyA", Direction.Left},
            {"KeyS", Direction.Down},
            {"KeyD", Direction.Right}
        };
        public List<Wall> Walls = new List<Wall>();
        
        public List<Box> Boxes = new List<Box>();
        
        public List<Enemy> Enemies = new List<Enemy>();
        
        public List<Bomb> Bombs = new List<Bomb>();
        
        public List<Explosion> Explosions = new List<Explosion>();
        
        public Player Player;
        
        public List<GameElement> Obstacles
        {
            get
            {
                var obstacles = new List<GameElement>();
                obstacles.AddRange(Walls);
                obstacles.AddRange(Boxes);
                return obstacles;
            }
        }
        
        public List<GameElement> AllElements
        {
            get
            {
                var elements = new List<GameElement>();
                elements.AddRange(Walls);
                elements.AddRange(Boxes);
                elements.AddRange(Enemies);
                elements.Add(Player);
                return elements;
            }
        }
        
        public GameLogic(char[][] charMap)
        {
            CreateMap(charMap);
        }

        private void CreateMap(char[][] charMap)
        {
            for (int i = 0; i < charMap.Length; i++)
            {
                for (int j = 0; j < charMap[i].Length; j++)
                {
                    char c = charMap[i][j];
                    switch (c)
                    {
                        case 'X':
                            Walls.Add(new Wall(j, i));
                            break;
                        case 'B':
                            Boxes.Add(new Box(j, i));
                            break;
                        case 'P':
                            Player = new Player(j, i);
                            break;
                        case 'E':
                            Enemies.Add(new Enemy(j, i));
                            break;
                        case '.':
                            break;
                        default:
                            throw new Exception($"Unknow character '{c}' in map");
                    }
                }
            }
        }

        public void MakeMove(KeyboardEventArgs keyPressed)
        {
            MoveEnemy();

            if (keyPressed != null)
            {
                if (directionKeys.ContainsKey(keyPressed.Code)) MovePlayer(keyPressed);
                if (keyPressed.Code == "Space") AddBomb();
            }
        }

        private void MoveEnemy()
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.Movement != null && enemy.CanMove(enemy.Movement, Obstacles))
                {
                    enemy.Move(enemy.Movement);
                }
                else
                {
                    enemy.Movement = Movement.CreateRandomMovement(enemy.Speed);
                }
            }
        }

        private void MovePlayer(KeyboardEventArgs keyPressed)
        {
            var playerMovement = Movement.CreateFromDirection(directionKeys[keyPressed.Code], Player.Speed);
            if (Player.CanMove(playerMovement, Obstacles))
            {
                Player.Move(playerMovement);
            }  
        }
        
        private void AddBomb()
        {
            
        }
    }
}
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
        
        private ElementMap ElementMap { get; set; }
        
        public List<Enemy> Enemies = new List<Enemy>();
        
        public List<Bomb> Bombs = new List<Bomb>();
        
        public List<Explosion> Explosions = new List<Explosion>();
        
        public Player Player;
        
        public List<GameElement> AllElements
        {
            get
            {
                var elements = new List<GameElement>();
                elements.AddRange(ElementMap.GetAllElements());
                elements.AddRange(Enemies);
                elements.AddRange(Bombs);
                elements.AddRange(Explosions);
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
            ElementMap = new ElementMap(charMap[0].Length, charMap.Length);
            
            for (int i = 0; i < charMap.Length; i++)
            {
                for (int j = 0; j < charMap[i].Length; j++)
                {
                    char c = charMap[i][j];
                    switch (c)
                    {
                        case 'X':
                            ElementMap.AddElement(new Wall(j,i));
                            break;
                        case 'B':
                            ElementMap.AddElement(new Box(j,i));
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
            
            if (EnemyEatPlayer()) Player.isDead = true;
            if (ExplosionDestroyPlayer()) Player.isDead = true;
            ExplosionDestroyEnemy();
            ProcessBombs();
            ProcessExplosions();

        }
        
        private void MoveEnemy()
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.Movement != null && enemy.CanMove(enemy.Movement, ElementMap.GetCloseElements(enemy)))
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
            if (Player.CanMove(playerMovement, ElementMap.GetCloseElements(Player)))
            {
                Player.Move(playerMovement);
            }
        }
        
        private void AddBomb()
        {
            Bombs.Add(new Bomb(Player.MapPositionX, Player.MapPositionY));
        }

        private void ProcessBombs()
        {
            for (int i = Bombs.Count -1; i >= 0; i--)
            {
                Bomb bomb = Bombs[i];
                if (bomb.Exploded())
                {
                    CreateExplosion(bomb);
                    Bombs.RemoveAt(i);
                }
            }
        }
        
        private void CreateExplosion(Bomb bomb)
        {
            
        }
        
        private void ProcessExplosions()
        {
            Explosions.RemoveAll(bomb => !bomb.isActive());
        }

        private bool EnemyEatPlayer()
        {
            bool result = false;
            foreach (var enemy in Enemies)
            {
                if (enemy.IntersectWith(Player))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        
        private bool ExplosionDestroyPlayer()
        {
            bool result = false;
            foreach (var explosion in Explosions)
            {
                if (explosion.IntersectWith(Player))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        
        private void ExplosionDestroyEnemy()
        {
            foreach (var explosion in Explosions)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemy enemy = Enemies[i];
                    if (enemy.IntersectWith(explosion))
                    {
                        Enemies.RemoveAt(i);
                    }
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;

namespace BomberMan.Client.Data
{
    public class GameLogic
    {
        public GameState GameState;
        
        public GameTime GameTime;
        
        private readonly Dictionary<string, Direction> directionKeys = new Dictionary<string, Direction>()
        {
            {"KeyW", Direction.Up},
            {"KeyA", Direction.Left},
            {"KeyS", Direction.Down},
            {"KeyD", Direction.Right}
        };
        
        private ElementMap ElementMap { get; set; }
        
        private List<Enemy> Enemies = new List<Enemy>();
        
        private List<Bomb> Bombs = new List<Bomb>();
        
        private List<Explosion> Explosions = new List<Explosion>();
        
        private Player Player;

        private Finish Finish;
        
        /// <summary>
        /// Get All Elements To Render
        /// </summary>
        public List<GameElement> AllElements
        {
            get
            {
                List<GameElement> elements = new List<GameElement>();
                
                elements.AddRange(ElementMap.GetAllElements());
                elements.AddRange(Enemies);
                elements.AddRange(Bombs);
                elements.AddRange(Explosions);
                elements.Add(Player);
                
                // show finish
                if (Finish != null) elements.Add(Finish);
                
                return elements;
            }
        }
        
        public GameLogic(char[][] charMap)
        {
            CreateMap(charMap);
            ChangeGameState(GameState.Playing);
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
                            ElementMap.AddElement(new Grass(j,i));
                            Player = new Player(j, i);
                            break;
                        case 'E':
                            ElementMap.AddElement(new Grass(j,i));
                            Enemies.Add(new Enemy(j, i));
                            break;
                        case '.':
                            ElementMap.AddElement(new Grass(j,i));
                            break;
                        default:
                            throw new Exception($"Unknow character '{c}' in map");
                    }
                }
            }
        }

        private void ChangeGameState(GameState gameState)
        {
            if (gameState == GameState.Playing)
            {
                GameTime = new GameTime();
                GameTime.Start();
            }
            else if (gameState == GameState.Lose || gameState == GameState.Win)
            {
                GameTime.Stop();
            }
            GameState = gameState;
        }
        
        public void MakeMove(KeyboardEventArgs keyPressed)
        {
            if (GameState != GameState.Playing) return;
            
            //move with enemies
            MoveEnemy();

            // process pressed keys
            if (keyPressed != null)
            {
                if (directionKeys.ContainsKey(keyPressed.Code)) MovePlayer(keyPressed);
                if (keyPressed.Code == "Space") AddBomb();
            }
            
            // check if enemy destroys player
            if (EnemyEatPlayer())
            {
                Player.isDead = true;
                ChangeGameState(GameState.Lose);
            }
            
            // check if explosion destroys player
            if (ExplosionDestroyPlayer())
            {
                Player.isDead = true;
                ChangeGameState(GameState.Lose);
            }
            
            // check if explosion destroys enemy
            ExplosionDestroyEnemy();
            
            // explosion and bombs ticking
            ProcessBombs();
            ProcessExplosions();
            
            // if all enemies are dead and Finish is not set create Finish
            if (Enemies.Count == 0 && Finish == null)
            {
                (int x, int y) = ElementMap.GetFinishPosition();
                Finish = new Finish(x, y);
            }
            
            // if there is finish check if player is on it
            if (Finish != null && Finish.IntersectWith(Player))
            {
                ChangeGameState(GameState.Win);
            }
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
            Movement playerMovement = Movement.CreateFromDirection(directionKeys[keyPressed.Code], Player.Speed);
            
            if (Player.CanMove(playerMovement, ElementMap.GetCloseElements(Player)))
            {
                Player.Move(playerMovement);
            }
        }
        
        private void AddBomb()
        {
            foreach (var bomb in Bombs)
            {
                // if there is bomb on the same tile
                if (GameElement.OnTheSameTile(bomb, Player)) return;
            }
            Bombs.Add(new Bomb(Player.MapCenterPositionX, Player.MapCenterPositionY));
        }

        private void ProcessBombs()
        {
            foreach (var bomb in Bombs)
            {
                bomb.Tick();
                if (bomb.Remove) CreateExplosion(bomb);
            }

            Bombs.RemoveAll(bomb => bomb.Remove);
        }
        
        private void CreateExplosion(Bomb bomb)
        {
            Explosions.AddRange(ElementMap.CreateExplosionForBomb(bomb));
        }
        
        private void ProcessExplosions()
        {
            Explosions.RemoveAll(explosion => !explosion.isActive());
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
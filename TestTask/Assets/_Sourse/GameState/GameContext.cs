using Core;
using Environment.Asteroids;
using Environment.Boosters;
using Player;
using Score;

namespace GameState
{
    public class GameContext
    {
        public IInputListener InputListener { get; }
        public ScoreManager ScoreManager { get; }
        public PlayerController PlayerController { get; }
        public AsteroidSpawner AsteroidSpawner { get; }
        public BoosterSpawner BoosterSpawner { get; }

        public GameContext(
            IInputListener inputListener, 
            ScoreManager scoreManager, 
            PlayerController playerController, 
            AsteroidSpawner asteroidSpawner, 
            BoosterSpawner boosterSpawner)
        {
            InputListener = inputListener;
            ScoreManager = scoreManager;
            PlayerController = playerController;
            AsteroidSpawner = asteroidSpawner;
            BoosterSpawner = boosterSpawner;
        }
    }
}
using Core;
using Environment.Asteroids;
using Environment.Boosters;
using Player;
using Score;

namespace GameState
{
    public class GameContext
    {
        public InputListener InputListener { get; }
        public ScoreManager ScoreManager { get; }
        public PlayerController PlayerController { get; }
        public AsteroidFactory AsteroidFactory { get; }
        public BoosterFactory BoosterFactory { get; }

        public GameContext(
            InputListener inputListener, 
            ScoreManager scoreManager, 
            PlayerController playerController, 
            AsteroidFactory asteroidFactory, 
            BoosterFactory boosterFactory)
        {
            InputListener = inputListener;
            ScoreManager = scoreManager;
            PlayerController = playerController;
            AsteroidFactory = asteroidFactory;
            BoosterFactory = boosterFactory;
        }
    }
}
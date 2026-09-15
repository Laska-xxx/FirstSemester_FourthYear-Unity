using Core;

namespace GameState
{
    public class GameContext
    {
        public InputListener InputListener { get; private set; }
        // Add other game-related properties and services here

        public GameContext(InputListener inputListener)
        {
            InputListener = inputListener;
        }
    }
}
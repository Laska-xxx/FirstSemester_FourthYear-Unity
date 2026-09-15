using System;

namespace Score
{
    public class ScoreManager
    {
        public event Action<int> OnScoreChanged;
        public int CurrentScore { get; private set; }

        public void AddScore(int score)
        {
            CurrentScore += score;
            OnScoreChanged?.Invoke(CurrentScore);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            OnScoreChanged?.Invoke(CurrentScore);
        }
    }
}
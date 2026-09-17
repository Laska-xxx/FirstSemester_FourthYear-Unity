using Score;
using TMPro;
using UnityEngine;
using VContainer;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreManager _scoreManager;

    [Inject] private void Init(ScoreManager scoreManager)
    {
        _scoreManager = scoreManager;
    }

    private void OnEnable()
    {
        _scoreManager.OnScoreChanged += UpdateScoreUI;

        UpdateScoreUI(_scoreManager.CurrentScore);
    }

    private void OnDisable()
    {
        _scoreManager.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }
}

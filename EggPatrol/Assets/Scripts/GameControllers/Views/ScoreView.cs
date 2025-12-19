using TMPro;
using UnityEngine;

namespace GameControllers.Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _bestScoreText;

        public void UpdateScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void UpdateBestScore(int bestScore)
        {
            _bestScoreText.text = bestScore.ToString();
        }
    }
}

using TMPro;
using UnityEngine;

namespace GameControllers.Views
{
    public class EggsScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void UpdateEggs(int score)
        {
            _scoreText.text = $"{score}";
        }
    }
}

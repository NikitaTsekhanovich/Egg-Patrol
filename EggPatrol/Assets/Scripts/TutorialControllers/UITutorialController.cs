using TMPro;
using UnityEngine;

namespace TutorialControllers
{
    public class UITutorialController : MonoBehaviour
    {
        [SerializeField] private GameObject _textFrame;
        [SerializeField] private TMP_Text _messegeText;

        public void ShowText(string message)
        {
            _textFrame.SetActive(true);
            _messegeText.text = message;
        }

        public void HideText()
        {
            _textFrame.SetActive(false);
        }
    }
}

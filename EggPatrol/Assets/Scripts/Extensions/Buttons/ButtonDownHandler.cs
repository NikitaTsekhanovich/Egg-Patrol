using GameControllers.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extensions.Buttons
{
    public class ButtonDownHandler : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private InputController _inputController;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _inputController.ClickJump();
        }
    }
}

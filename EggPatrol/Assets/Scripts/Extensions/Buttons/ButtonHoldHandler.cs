using GameControllers.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using MoveDirection = GameControllers.Models.Enums.MoveDirection;

namespace Extensions.Buttons
{
    public class ButtonHoldHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private MoveDirection _moveDirection;
        
        public void OnPointerUp(PointerEventData eventData)
        {
            _inputController.StopHoldMove();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inputController.HoldMove(_moveDirection);
        }
    }
}

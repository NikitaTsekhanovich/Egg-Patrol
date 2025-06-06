using System;
using GameControllers.Controllers.Properties;
using GameControllers.Models.Enums;
using UnityEngine;

namespace GameControllers.Controllers
{
    public class InputController : MonoBehaviour, IInputController
    {
        private MoveDirection _moveDirection;
        
        public event Action HoldMoveRightButton;
        public event Action HoldMoveLeftButton;
        public event Action StopHoldMoveButton;
        public event Action ClickJumpButton;

        public void HoldMove(MoveDirection moveDirection)
        {
            if (moveDirection == MoveDirection.LeftDirection)
                HoldMoveLeft();
            else if (moveDirection == MoveDirection.RightDirection) 
                HoldMoveRight();
        }

        public void StopHoldMove()
        {
            StopHoldMoveButton?.Invoke();
        }

        public void ClickJump()
        {
            ClickJumpButton?.Invoke();
        }
        
        private void HoldMoveRight()
        {
            HoldMoveRightButton?.Invoke();
        }
        
        private void HoldMoveLeft()
        {
            HoldMoveLeftButton?.Invoke();
        }
    }
}

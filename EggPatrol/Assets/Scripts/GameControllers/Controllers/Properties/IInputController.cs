using System;

namespace GameControllers.Controllers.Properties
{
    public interface IInputController
    {
        public event Action HoldMoveRightButton;
        public event Action HoldMoveLeftButton;
        public event Action StopHoldMoveButton;
        public event Action ClickJumpButton;
    }
}

using System;

namespace GameControllers.Bootstrap.Properties
{
    public interface IGameStateController
    {
        public event Action<bool> OnPauseGame;
        public event Action OnEndGame;
        public void PauseGame();
        public void ResumeGame();
        public void EndGame();
    }
}

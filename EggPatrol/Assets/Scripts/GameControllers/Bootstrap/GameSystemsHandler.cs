using System.Collections.Generic;
using GameControllers.Bootstrap.Properties;
using GameControllers.Controllers.Properties;

namespace GameControllers.Bootstrap
{
    public class GameSystemsHandler : ISystemsHandler, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly List<IHaveUpdate> _updatesSystems = new ();
        private readonly List<IHaveUpdate> _pendingUpdatesAdditions = new ();
        private readonly List<IHaveFixedUpdate> _fixedUpdatesSystems = new ();
        private readonly List<IHaveFixedUpdate> _pendingFixedUpdatesAdditions = new ();

        public GameSystemsHandler()
        {
            _updatesSystems.Capacity = 32;
            _pendingUpdatesAdditions.Capacity = 8;
            _fixedUpdatesSystems.Capacity = 32;
            _pendingFixedUpdatesAdditions.Capacity = 8;
        }
        
        public void RegisterUpdateSystem(IHaveUpdate system)
        {
            _pendingUpdatesAdditions.Add(system);
        }

        public void UnregisterUpdateSystem(IHaveUpdate system)
        {
            
        }

        public void RegisterFixedUpdateSystem(IHaveFixedUpdate system)
        {
            _pendingFixedUpdatesAdditions.Add(system);
        }

        public void UnregisterFixedUpdateSystem(IHaveFixedUpdate system)
        {
            
        }
        
        public void UpdateSystem()
        {
            foreach (var updateSystem in _updatesSystems)
            {
                updateSystem?.UpdateSystem();
            }

            if (_pendingUpdatesAdditions.Count > 0)
            {
                _updatesSystems.AddRange(_pendingUpdatesAdditions);
                _pendingUpdatesAdditions.Clear();
            }
        }

        public void FixedUpdateSystem()
        {
            foreach (var fixedUpdateSystem in _fixedUpdatesSystems)
                fixedUpdateSystem?.FixedUpdateSystem();
            
            if (_pendingFixedUpdatesAdditions.Count > 0)
            {
                _fixedUpdatesSystems.AddRange(_pendingFixedUpdatesAdditions);
                _pendingFixedUpdatesAdditions.Clear();
            }
        }
    }
}

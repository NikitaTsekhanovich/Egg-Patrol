using GameControllers.Models;
using UnityEngine;

namespace GameControllers.Factories.FactoriesTypes
{
    public class PlayerFactory : Factory<Player>
    {
        private readonly Player _playerPrefab;

        public PlayerFactory(Player playerPrefab)
        {
            _playerPrefab = playerPrefab;
        }
        
        public override Player GetEntity(Transform transformSpawn)
        {
            var newPlayer = Object.Instantiate(_playerPrefab, transformSpawn.position, transformSpawn.rotation);
            return newPlayer;
        }
    }
}

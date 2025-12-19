using GameControllers.Models.Enums;

namespace GameControllers.Controllers.Properties
{
    public interface ICanTakePeriodicDamage
    {
        public void TakePeriodicDamage(DamageType damageType, int damage, float timeInterval, float periodicDamageDuration);
    }
}

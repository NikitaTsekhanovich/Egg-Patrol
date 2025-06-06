namespace GameControllers.Controllers.Properties
{
    public interface IPlayer : 
        ICanTakeDamage, 
        ICanIncreaseScore,
        ICanInteractInsideEgg, 
        ICanTakePeriodicDamage,
        IClickableObject,
        IHaveUpdate,
        IHaveFixedUpdate
    {
        
    }
}

using UnityEngine;

namespace MainMenuControllers.StoreControllers
{
    [CreateAssetMenu(fileName = "StoreItemConfig", menuName = "Configs/StoreItemConfig")]
    public class StoreItemConfig : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public int MaxCount { get; private set; }
        
        public virtual string GUID { get; protected set; }
    }
}

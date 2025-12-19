using SaveSystems.DataTypes;
using UnityEngine;

namespace MainMenuControllers.StoreControllers.Types
{
    [CreateAssetMenu(fileName = "ItemDestroyObjectConfig", menuName = "Configs/Store/ItemDestroyObjectConfig")]
    public class ItemDestroyObjectConfig : StoreItemConfig
    {
        public override string GUID => StoreSaveData.GUIDDestroyObjectCount;
    }
}

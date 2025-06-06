using SaveSystems.DataTypes;
using UnityEngine;

namespace MainMenuControllers.StoreControllers.Types
{
    [CreateAssetMenu(fileName = "ItemImmunityConfig", menuName = "Configs/Store/ItemImmunityConfig")]
    public class ItemImmunityConfig : StoreItemConfig
    {
        public override string GUID => StoreSaveData.GUIDHasImmunity;
    }
}

using SaveSystems.DataTypes;
using UnityEngine;

namespace MainMenuControllers.StoreControllers.Types
{
    [CreateAssetMenu(fileName = "ItemLivesConfig", menuName = "Configs/Store/ItemLivesConfig")]
    public class ItemLivesConfig : StoreItemConfig
    {
        public override string GUID => StoreSaveData.GUIDLivesCount;
    }
}

namespace SaveSystems.Properties
{
    public interface ISaveDataHandler
    {
        public void RefreshDataForSave();
        public void RefreshDataForLoad();
        public void SaveData<TType>(TType data, string guid);
        public TType GetData<TType>(string guid);
    }
}

using PersonListApp.Tools.DataStorage;

namespace PersonListApp.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;
        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }
        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
    }
}

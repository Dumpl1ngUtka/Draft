using System;

namespace Services.SaveLoadSystem
{
    [Serializable]
    public class LocationData
    {
        public int[] CompletedDungeonsIndexes ;
        
        public LocationData()
        {
            CompletedDungeonsIndexes = Array.Empty<int>();
        }
        
        public LocationData(int[] completedDungeonsIndexes)
        {
            CompletedDungeonsIndexes = completedDungeonsIndexes;
        }
    }
}
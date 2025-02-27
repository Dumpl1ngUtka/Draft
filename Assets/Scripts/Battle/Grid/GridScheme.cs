using UnityEngine;

namespace Battle.Grid
{
    
    [CreateAssetMenu(fileName = "GridScheme", menuName = "Config/GridScheme", order = 0)]
    public class GridScheme : ScriptableObject
    {
        public int FirstLineCount;
        public int SecondLineCount;
        public int ThirdLineCount;
    }
}
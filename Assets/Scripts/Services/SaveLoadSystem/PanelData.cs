using System;

namespace Services.SaveLoadSystem
{
    
    [Serializable]
    public struct PanelData
    {
        public readonly string Name;
        public readonly string Description;
        //private string ;

        public PanelData(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
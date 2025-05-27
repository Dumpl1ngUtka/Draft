using System;
using UnityEngine;

namespace Services.SaveLoadSystem
{
    
    [Serializable]
    public struct PanelData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly string ColorHex;
        //private string ;

        public PanelData(string name, string description, string colorHex)
        {
            Name = name;
            Description = description;
            ColorHex = colorHex;
        }
    }
}
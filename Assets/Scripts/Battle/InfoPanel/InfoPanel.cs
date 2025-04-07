using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.InfoPanel
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextWithLinks _textField;

        protected virtual string GetString(string key)
        {
            var _testData = new Dictionary<string, string>()
            {
                {"level", "The higher the duck's level, the greater its power\n+1 to the main characteristic for every 5 levels\n+1 to the secondary characteristic for every 10 levels\n+1 to the rest of the stats for every 20 levels"}, 
                {"chemistry", "Chemistry affects the strength of certain skills \n+1 for a duck of the same race\n+1 for a covenant of the same color\n+1 for the same covenant"}, 
                {"strength", "Strength makes you less likely to get hit and makes your hits hurt more."}, 
                {"covenant", "Ducks from the same covenant receive +2 [chemistry] points. +1 if only the color matches"}
            };
            return _testData.GetValueOrDefault(key, "invalid_key");
        }
        
        public void Instantiate(string id)
        {
            var canvas = FindFirstObjectByType(typeof(Canvas)) as Canvas;
            if (canvas != null)
            {
                var panel = Instantiate(this, canvas.transform);
                panel.Render(GetString(id));
            }
        }
        
        public void Render(string text)
        {
            _textField.Render(text);
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}

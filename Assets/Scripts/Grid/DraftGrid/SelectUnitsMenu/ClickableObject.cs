using Services.PanelService;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    [RequireComponent(typeof(Image))]
    public class ClickableObject : MonoBehaviour
    {
        private string _key;
        
        public void Init(string key, Sprite icon)
        {
            _key = key;
            GetComponent<Image>().sprite = icon;
        }

        public void Click()
        {
            PanelService.Instance.InstantiateInfoPanel(_key);
        }
    }
}
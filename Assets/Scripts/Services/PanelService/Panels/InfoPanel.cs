using Services.SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.PanelService.Panels
{
    public class InfoPanel : Panel
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TextWithLinks _textField;
        
        public void Init(string key)
        {
            var data = SaveLoadService.Instance.GetAdviceByKey(key);
            Render(data);
            SetRandomRotation();
        }

        public void CreateInstance(string key)
        {
            PanelService.Instance.InstantiateInfoPanel(key);
        }
        
        private void Render(PanelData data)
        {
            _nameField.text = data.Name;
            _textField.Render(data.Description);
        }
    }
}

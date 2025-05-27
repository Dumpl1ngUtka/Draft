using System.Collections.Generic;
using Services.SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Services.PanelService.Panels
{
    public class ErrorPanel : Panel
    {
        private const float _destroyTime = 5f;
        [SerializeField] private TextWithLinks _textField;
        
        [SerializeField] private Image _progressBar;
        
        private float _timer;
        
        public void Init(string key)
        {
            _timer = 0f;
            var data = SaveLoadService.Instance.GetErrorByKey(key);
            Render(data);
        }
        
        private void Render(PanelData data)
        {
            _textField.Render(data.Name);
        }
        
        private void Update()
        {
            _timer += Time.deltaTime;
            _progressBar.fillAmount = _timer / _destroyTime;
            if (_timer >= _destroyTime)
            {
                Destroy();
            }
        }
    }
}
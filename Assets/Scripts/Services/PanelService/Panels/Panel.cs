using UnityEngine;

namespace Services.PanelService.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private RectTransform _cardInfoPanel;
        [SerializeField, Min(0)] private float _maxRotation = 3;
        
        protected void SetRandomRotation()
        {
            if (_cardInfoPanel == null || _maxRotation == 0)
                return;
            _cardInfoPanel.rotation = Quaternion.Euler(0, 0, Random.Range(-_maxRotation, _maxRotation));
        }
        
        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}
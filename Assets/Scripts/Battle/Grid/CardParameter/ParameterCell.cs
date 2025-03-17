using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.CardParameter
{
    public class ParameterCell : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Image _outlineImage;
        private static readonly Color _defaultOutlineColor = new Color(0.278f, 0.125f, 0.023f );
        
        public void Render(Color color)
        {
            _fillImage.color = color;
            _outlineImage.color = _defaultOutlineColor;
        }
        
        public void Render(Color color, Color outlineColor)
        {
            _fillImage.color = color;
            _outlineImage.color = outlineColor;
        }
    }
}

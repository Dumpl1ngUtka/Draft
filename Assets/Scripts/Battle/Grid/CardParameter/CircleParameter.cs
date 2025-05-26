using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid.CardParameter
{
    public class CircleParameter : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private CircleParameterCell[] _cells;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        private void OnValidate()
        {
            Render(_cells.Length);
        }
        
        public void Render(int activeCellsCount)
        {
            if (activeCellsCount < 0)
                return;

            for (int i = 0; i < _cells.Length; i++)
            {
                if (i < activeCellsCount)
                    _cells[i].Render(_activeColor);
                else
                    _cells[i].Render(_inactiveColor, _inactiveColor);
            }
        }
    }
}

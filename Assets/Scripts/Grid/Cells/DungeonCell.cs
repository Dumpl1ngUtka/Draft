using DungeonMap;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.Cells
{
    public class DungeonCell : GridCell
    {
        [SerializeField] private DungeonInfo _dungeonInfo;
        private Image _image;
        
        public DungeonInfo DungeonInfo => _dungeonInfo;
        public RectTransform RectTransform => transform as RectTransform;

        public void SetAlphaHitTestMinimumThreshold(float value = 0.5f)
        {
            if (_image == null)
                _image = GetComponent<Image>();
            
            _image.alphaHitTestMinimumThreshold = value;
        }

        public void SetActive(bool isActive)
        {
            _image.raycastTarget = isActive;
            _image.color = isActive ? Color.white : Color.gray;
        }
    }
}
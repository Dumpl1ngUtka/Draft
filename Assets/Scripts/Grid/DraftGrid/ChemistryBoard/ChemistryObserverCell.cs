using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.DraftGrid.ChemistryBoard
{
    public class ChemistryObserverCell : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public void SetImageColor(Color color)
        {
            _image.color = color;
        }
        
        public void Init(Sprite sprite, string text)
        {
            _image.sprite = sprite;
            _text.text = text;
        }
    }
}
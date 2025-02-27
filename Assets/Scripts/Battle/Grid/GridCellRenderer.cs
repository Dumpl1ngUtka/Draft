using Battle.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Grid
{
    public class GridCellRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _chemistryText;
        [SerializeField] private Image _covenantImage;
        [SerializeField] private Image _raceImage;
        [SerializeField] private TMP_Text _levelText;

        public void Render(Unit unit)
        {
            _levelText.text = unit.Level.ToString();
            _chemistryText.text = unit.IsMaxChem? "MAX" : unit.Chemestry.ToString();
            _covenantImage.sprite = unit.Covenant.Icon;
            _raceImage.sprite = unit.Race.Icon;
        }
    }
}

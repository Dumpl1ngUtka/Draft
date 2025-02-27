using TMPro;
using UnityEngine;

namespace Battle.Grid
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _totalChemText;
        [SerializeField] private TMP_Text _avaregeLevelText;
        private Grid _grid;

        public void Init(Grid grid)
        {
            _grid = grid;
            _grid.TeamChanged += Render;
        }

        private void Render()
        {
            _totalChemText.text = _grid.AllTeamChem.ToString();
            _avaregeLevelText.text = (_grid.AllTeamSumLevel / _grid.FillCellsCount).ToString();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsPanel : MonoBehaviour
    {
        [SerializeField] private SelectUnitsCell[] _selectUnitsCells;
        [SerializeField] private GridLayoutGroup _container;
        [Header("Animation")]
        [SerializeField] private RectTransform _animatedBody;
        [SerializeField] private float _animationDuration;
        [SerializeField] private AnimationCurve _heightCurve;
        public SelectUnitsCell SelectedCell { get; private set; }
        
        public void Init()
        {
            ControlSize();
        }
        
        public void SetActive(bool isActive)
        {
            SelectedCell = null;
            SelectUnitCell(SelectedCell);
            gameObject.SetActive(isActive);
            if (isActive)
                StartCoroutine(EnableAimation());
        }

        public void RenderUnits(List<Unit> units)
        {
            var i = 0;
            foreach (var cell in _selectUnitsCells)
                cell.Init(this, units[i++]);
        }

        public void SelectUnitCell(SelectUnitsCell selectedCell)
        {
            foreach (var cell in _selectUnitsCells)
            {
                cell.SetOutline(false);
            }
            selectedCell?.SetOutline(true);
            SelectedCell = selectedCell;
        }
        
        private void ControlSize()
        {
            var cellWidth = (((RectTransform)_container.transform).rect.width) / _container.constraintCount;
            var cellHeight = (((RectTransform)_container.transform).rect.height) / (_selectUnitsCells.Length / _container.constraintCount);
            _container.cellSize = new Vector2(cellWidth, cellHeight);
        }

        private IEnumerator EnableAimation()
        {
            var maxHeight = Screen.height / 2;
            _animatedBody.localPosition = Vector3.up * Screen.height;
            var timer = 0f;
            while (timer < _animationDuration)
            {
                var height = _heightCurve.Evaluate(timer / _animationDuration) * maxHeight;
                _animatedBody.localPosition = Vector3.up * height;
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}

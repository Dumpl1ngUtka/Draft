using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Grid.DraftGrid.SelectUnitsMenu
{
    public class SelectUnitsPanel : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private SelectUnitsCell _selectUnitsCellPrefab;
        [SerializeField] private Transform _container;
        [Header("Animation")]
        [SerializeField] private RectTransform _animatedBody;
        [SerializeField] private float _animationDuration;
        [SerializeField] private AnimationCurve _heightCurve;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private float _cellMoveSpeed;
        private List<SelectUnitsCell> _selectUnitsCells = new List<SelectUnitsCell>();
        private List<Unit> _units;
        private int _selectedUnitIndex;
        private DraftGridController _controller;
        private Vector2 _delta;
        private Vector2 _center;
        private SelectUnitsCell _cellOnTop => _selectUnitsCells[_selectedUnitIndex];

        public void Init(DraftGridController draftController)
        {
            _controller = draftController;
            // ReSharper disable once PossibleLossOfFraction
            _center = new Vector2(Screen.width / 2, Screen.height / 2);
        }
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
            //if (isActive)
                
                //StartCoroutine(EnableAimation());
        }

        public void RenderUnits(List<Unit> units)
        {
            ClearCells();
            _selectedUnitIndex = 0;
            _units = units;
            var maxHeight = Screen.height / 2;

            for (var i = 0; i < _units.Count; i++)
            {
                var unit = units[i];
                _selectUnitsCells.Add(CreateCellInstance(i + 1, _units.Count, unit));
            }
            _selectUnitsCells[0].transform.SetAsLastSibling();
        }

        private void ClearCells()
        {
            _selectUnitsCells.Clear();
            foreach (Transform child in _container)
            {
                Destroy(child.gameObject);
            }
        }

        private void SwitchCell(bool isPositive)
        {
            _selectedUnitIndex += isPositive ? -1 : 1;
            if (_selectedUnitIndex < 0)
                _selectedUnitIndex = _units.Count - 1;
            if (_selectedUnitIndex >= _units.Count)
                _selectedUnitIndex = 0;
            _selectUnitsCells[_selectedUnitIndex].transform.SetAsLastSibling();
        }
        
        private SelectUnitsCell CreateCellInstance(int index, int maxIndex, Unit unit)
        {
            var instance = Instantiate(_selectUnitsCellPrefab, _container);
            instance.Init(index, maxIndex, unit);
            return instance;
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

        private void SetCellsActive(bool isActive)
        {
            _container.gameObject.SetActive(isActive);
            _backgroundImage.color = new Color(0, 0, 0, isActive ? 0.9f : 0.05f);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _delta += eventData.delta;
            if (_delta.y > Screen.width * 0.2f && _container.gameObject.activeInHierarchy)
                SetCellsActive(false);
            else if (_delta.y < Screen.width * 0.2f && !_container.gameObject.activeInHierarchy)
                SetCellsActive(true);
            _cellOnTop.SetDelta(_delta/ 10);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _cellOnTop.SetDelta(Vector2.zero);
            if (_delta.magnitude > Screen.width * 0.2f)
                if (Mathf.Abs(_delta.x) > Mathf.Abs(_delta.y))
                    SwitchCell(_delta.x > 0);
                else if (_delta.y < 0)
                    _controller.SelectMenuFinished(_units[_selectedUnitIndex]);
            _delta = new Vector2();
            SetCellsActive(true);
        }
    }
}

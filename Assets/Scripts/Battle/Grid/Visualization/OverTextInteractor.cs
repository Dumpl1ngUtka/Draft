using System;
using Battle.Units;
using TMPro;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverTextInteractor : GridCellInteractor
    {
        [SerializeField] private TMP_Text _overText;

        protected override void ActiveChanged(bool isActive)
        {
            _overText.gameObject.SetActive(isActive);
        }

        public void SetText(string text)
        {
            _overText.text = text;
        }
        
        protected override void UpdateInfo(Unit unit)
        {
        }
    }
}
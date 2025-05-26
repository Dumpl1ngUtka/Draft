using System;
using TMPro;
using UnityEngine;

namespace Battle.Grid.Visualization
{
    [Serializable]
    public class OverTextInteractor : GridCellInteractor
    {
        [SerializeField] private TMP_Text _overText;

        public void SetText(string text)
        {
            if (_overText == null)
                return;
            
            _overText.text = text;
        }

        protected override void UpdateInfo()
        {
        }

        protected override void SetActive(bool isActive)
        {
            _overText?.gameObject.SetActive(isActive);
        }
    }
}
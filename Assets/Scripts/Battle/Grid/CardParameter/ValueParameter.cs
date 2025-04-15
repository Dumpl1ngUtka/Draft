using TMPro;
using UnityEngine;

namespace Battle.Grid.CardParameter
{
    public class ValueParameter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _value;

        public void Render(int value, int maxValue = -1)
        {
            if (maxValue == -1)
                _value.text = value.ToString();
            else 
                _value.text = value + "/" + maxValue;
        }
    }
}
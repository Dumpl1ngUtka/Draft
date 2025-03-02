using UnityEngine;
using UnityEngine.UI;

namespace Battle.InfoPanel
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;

        public void Render()
        {
            var canvas = FindFirstObjectByType(typeof(Canvas)) as Canvas;
            if (canvas != null)
            {
                var info = Instantiate(this.gameObject, canvas.transform);
            }
            //info.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        public void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}

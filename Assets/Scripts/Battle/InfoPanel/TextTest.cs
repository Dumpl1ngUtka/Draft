using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.InfoPanel
{
    public class TextTest : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private InfoPanel _infoPanelPrefab; 
        private const string _format = @"\[{1}\w+\]{1}";
    
        private TMP_Text _textMessage;

        public void Render(string text)
        {
            _textMessage = GetComponent<TMP_Text>();
            _textMessage.text = text;
            CheckLinks();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_textMessage, eventData.position, eventData.pressEventCamera);
            if (linkIndex == -1) 
                return;
            TMP_LinkInfo linkInfo = _textMessage.textInfo.linkInfo[linkIndex];
            string selectedLink = linkInfo.GetLinkID();
        
            if (selectedLink != "")
                ClickToLink(selectedLink);
        }

        private void ClickToLink(string link)
        {
            link = Regex.Replace(link, @"\[|\]", "");
            _infoPanelPrefab.Instantiate(link);
        }
    
        private void CheckLinks () {
            Regex regx = new Regex (_format , RegexOptions.IgnoreCase | RegexOptions.Singleline); 
            MatchCollection matches = regx.Matches (_textMessage.text); 
            foreach (Match match in matches) 
                _textMessage.text = _textMessage.text.Replace(match.Value, FormatLink(match.Value));     	
        }
    
        private string FormatLink (string link) {
            link = Regex.Replace(link, @"\[|\]", "");
            var text = link; // name from file
            return $"<u><link=\"{link}\">{text}</link></u>";
        }
    }
}

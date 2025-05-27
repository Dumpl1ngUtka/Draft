using System.Text.RegularExpressions;
using Services.SaveLoadSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services.PanelService.Panels
{
    public class TextWithLinks : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text _textMessage;
        private const string _format = @"\[{1}\w+\]{1}";

        private TMP_Text TextMessage
        {
            get
            {
                if (!_textMessage)
                    _textMessage = GetComponent<TMP_Text>();
                return _textMessage;
            }
        }

        public void Render(string text)
        {
            TextMessage.text = text;
            CheckLinks();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(TextMessage, eventData.position, eventData.pressEventCamera);
            if (linkIndex == -1) 
                return;
            TMP_LinkInfo linkInfo = TextMessage.textInfo.linkInfo[linkIndex];
            string selectedLink = linkInfo.GetLinkID();
        
            if (selectedLink != "")
                ClickToLink(selectedLink);
        }

        private void ClickToLink(string link)
        {
            link = Regex.Replace(link, @"\[|\]", "");
            PanelService.Instance.InstantiateInfoPanel(link);
        }
    
        private void CheckLinks () {
            Regex regx = new Regex(_format , RegexOptions.IgnoreCase | RegexOptions.Singleline); 
            MatchCollection matches = regx.Matches(TextMessage.text);
            foreach (Match match in matches)
                TextMessage.text = TextMessage.text.Replace(match.Value, FormatLink(match.Value));
        }
    
        private string FormatLink (string link) { 
            link = Regex.Replace(link, @"\[|\]", "");
            var data = SaveLoadService.Instance.GetAdviceByKey(link);
            var text = $"<color={data.ColorHex}>{data.Name}</color>";
            return $"<link=\"{link}\">{text}</link>";
        }
    }
}

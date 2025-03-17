using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private readonly string _draftSceneName = "DraftScene";
        
        public void StartGame()
        {
            SceneManager.LoadScene(_draftSceneName);
        }
    }
}

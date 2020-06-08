using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        public string newGameScene;
        public string loadGameScene;

        public GameObject continueButton;

        private void Start()
        {
        }

        public void Continue()
        {
            SceneManager.LoadScene(loadGameScene);
        }

        public void NewGame()
        {
            SceneManager.LoadScene(newGameScene);
        }

        public void ExitGame()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
                Application.Quit();
        }
    }
}

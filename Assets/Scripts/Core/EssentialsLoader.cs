using Audio;
using Terminal;
using Movement;
using UnityEngine;

namespace Core
{
    public class EssentialsLoader : MonoBehaviour
    {
        public GameObject UIScreen;
        public GameObject player;
        public GameObject gameMan;
        public GameObject console;
        public GameObject audioManager;

        private void Start()
        {
            if (UIFade.Instance == null)
            {
                UIFade.Instance = Instantiate(UIScreen).GetComponent<UIFade>();
            }

            if (PlayerController.Instance == null)
            {
                var clone = Instantiate(player).GetComponent<PlayerController>();
                PlayerController.Instance = clone;
            }

            if (GameManager.Instance == null)
            {
                Instantiate(gameMan);
            }

            if (TerminalController.Instance == null)
            {
                Instantiate(console);
            }

            if (AudioManager.Instance == null)
            {
                Instantiate(audioManager);
            }
        }
    }
}

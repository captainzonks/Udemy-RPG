using Movement;
using UnityEngine;

namespace Core
{
    public class EssentialsLoader : MonoBehaviour
    {
        public GameObject userInterface;
        public GameObject player;
        public GameObject theGameManager;

        private void Start()
        {
            if (UIFade.instance == null)
            {
                UIFade.instance = Instantiate(userInterface).GetComponent<UIFade>();
            }

            if (PlayerController.Instance == null)
            {
                var clone = Instantiate(player).GetComponent<PlayerController>();
                PlayerController.Instance = clone;
            }

            if (GameManager.Instance == null)
            {
                Instantiate(theGameManager);
            }
        }
    }
}

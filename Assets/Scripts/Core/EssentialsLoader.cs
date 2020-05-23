using Movement;
using UnityEngine;

namespace Core
{
    public class EssentialsLoader : MonoBehaviour
    {
        public GameObject UIScreen;
        public GameObject player;
        public GameObject gameMan;

        // Start is called before the first frame update
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

            if (GameManager.instance == null)
            {
                Instantiate(gameMan);
            }
        }
    }
}

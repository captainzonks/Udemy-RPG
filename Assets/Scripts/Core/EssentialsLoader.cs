using Movement;
using UnityEngine;

namespace Core
{
    public class EssentialsLoader : MonoBehaviour
    {
        [HideInInspector] public GameObject UIScreen;
        [HideInInspector] public GameObject player;
        [HideInInspector] public GameObject gameMan;

        private void Start()
        {
            if (UIFade.instance == null)
            {
                UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
            }

            if (PlayerController.instance == null)
            {
                var clone = Instantiate(player).GetComponent<PlayerController>();
                PlayerController.instance = clone;
            }

            if (GameManager.instance == null)
            {
                Instantiate(gameMan);
            }
        }
    }
}

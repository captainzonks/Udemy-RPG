using Movement;
using UnityEngine;

namespace Core
{
    public class PlayerLoader : MonoBehaviour
    {

        [SerializeField] private GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            if (PlayerController.Instance == null)
            {
                Instantiate(player);
            }
        }
    }
}
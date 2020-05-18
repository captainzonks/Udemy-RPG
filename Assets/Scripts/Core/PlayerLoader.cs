using Movement;
using UnityEngine;

namespace Core
{
    public class PlayerLoader : MonoBehaviour
    {

        public GameObject player;

        // Start is called before the first frame update
        private void Start()
        {
            if (PlayerController.instance == null)
            {
                Instantiate(player);
            }
        }
    }
}
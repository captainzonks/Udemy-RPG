using Movement;
using UnityEngine;

namespace Core
{
    public class PlayerLoader : MonoBehaviour
    {

        [SerializeField] GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerController.instance == null)
            {
                Instantiate(player);
            }
        }
    }
}
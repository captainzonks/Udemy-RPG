using Movement;
using UnityEngine;

namespace Core
{
    public class ShopKeeper : MonoBehaviour
    {
        bool _canOpen;

        [SerializeField] string[] itemsForSale = new string[40];

        void Update()
        {
            if (!_canOpen || !Input.GetButtonDown("Fire1") || !PlayerController.instance.CanMove() ||
                Shop.instance.shopMenu.activeInHierarchy) return;
            Shop.instance.itemsForSale = itemsForSale;

            Shop.instance.OpenShop();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canOpen = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canOpen = false;
            }
        }
    }
}

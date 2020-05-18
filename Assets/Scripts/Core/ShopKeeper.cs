using Movement;
using UnityEngine;

namespace Core
{
    public class ShopKeeper : MonoBehaviour
    {
        private bool _canOpen;

        [SerializeField] private string[] itemsForSale = new string[40];

        private void Update()
        {
            if (!_canOpen || !Input.GetButtonDown("Fire1") || !PlayerController.Instance.CanMove() ||
                Shop.Instance.ShopMenu.activeInHierarchy) return;
            Shop.Instance.ItemsForSale = itemsForSale;

            Shop.Instance.OpenShop();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canOpen = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canOpen = false;
            }
        }
    }
}

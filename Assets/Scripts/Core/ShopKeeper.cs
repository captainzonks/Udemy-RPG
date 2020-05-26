using Movement;
using UnityEngine;

namespace Core
{
    public class ShopKeeper : MonoBehaviour
    {
        private bool _canOpen;

        public string[] itemsForSale = new string[40];

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            if (!_canOpen || !Input.GetButtonDown("Fire1") || PlayerController.Instance.currentState != PlayerState.Walk ||
                Shop.Instance.shopMenu.activeInHierarchy) return;
            Shop.Instance.itemsForSale = itemsForSale;

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

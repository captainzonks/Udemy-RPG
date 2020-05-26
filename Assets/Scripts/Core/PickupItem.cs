using Movement;
using UnityEngine;

namespace Core
{
    public class PickupItem : MonoBehaviour
    {
        private bool _canPickup;

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            if (!_canPickup || !Input.GetButtonDown("Fire1") || PlayerController.Instance.currentState != PlayerState.Walk) return;
            GameManager.Instance.AddItem(GetComponent<Item>().itemName);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canPickup = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _canPickup = false;
            }
        }
    }
}

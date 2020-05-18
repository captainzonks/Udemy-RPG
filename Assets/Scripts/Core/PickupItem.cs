using Movement;
using UnityEngine;

namespace Core
{
    public class PickupItem : MonoBehaviour
    {
        private bool canPickup;

        // Update is called once per frame
        private void Update()
        {
            if (!canPickup || !Input.GetButtonDown("Fire1") || !PlayerController.instance.CanMove()) return;
            GameManager.instance.AddItem(GetComponent<Item>().GetItemName());
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canPickup = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                canPickup = false;
            }
        }
    }
}

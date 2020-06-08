using System;
using UnityEngine;

namespace Core
{
    public class Doorway : MonoBehaviour
    {
        [SerializeField] private Sprite lockedDoorSprite;
        [SerializeField] private Sprite unlockedDoorSprite;

        private Sprite doorSprite;

        private void Start()
        {
            doorSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

            doorSprite = lockedDoorSprite;
        }

        private void UnlockDoor()
        {
            doorSprite = unlockedDoorSprite;
        }

        private void LockDoor()
        {
            doorSprite = lockedDoorSprite;
        }
    }
}

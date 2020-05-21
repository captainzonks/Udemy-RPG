using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class RoomMove : MonoBehaviour
    {
        public Tilemap theGround;
        public Vector3 playerChange;

        private CameraController _cam;

        private void Start()
        {
            if (Camera.main != null)
                _cam = Camera.main.GetComponent<CameraController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            other.transform.position += playerChange;
            _cam.theMap = theGround;
            _cam.UpdateBounds();
        }
    }
}

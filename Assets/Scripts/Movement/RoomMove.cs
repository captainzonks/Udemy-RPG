using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class RoomMove : MonoBehaviour
    {
        public Tilemap theGround;
        public Vector3 playerChange;

        private CameraController _cam;

        // title card variables
        public bool needText;
        public string placeName;
        public GameObject text;
        public Text placeText;

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

            if (needText)
            {
                StartCoroutine(PlaceNameCo());
            }
        }

        private IEnumerator PlaceNameCo()
        {
            placeText.text = placeName;

            text.SetActive(true);

            yield return new WaitForSeconds(4f);

            text.SetActive(false);
        }
    }
}

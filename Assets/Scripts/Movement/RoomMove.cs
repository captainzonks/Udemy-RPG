using System.Collections;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class RoomMove : MonoBehaviour
    {
        public Tilemap theGround;
        public int musicToPlay;
        public Vector3 playerChange;

        private CameraController _cam;

        private bool _coRunning;

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
            AudioManager.Instance.PlayBGM(musicToPlay);

            if (!needText) return;
            if (_coRunning)
                StopCoroutine(PlaceNameCo());
            StartCoroutine(PlaceNameCo());
        }

        private IEnumerator PlaceNameCo()
        {
            _coRunning = true;
            placeText.text = placeName;

            text.SetActive(true);

            yield return new WaitForSeconds(4f);

            text.SetActive(false);
            _coRunning = false;
        }
    }
}

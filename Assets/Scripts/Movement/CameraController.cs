using Audio;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class CameraController : MonoBehaviour
    {
        // set references in editor
        public Transform target;
        public float smoothing;
        public Tilemap theMap;

        // map limits
        [HideInInspector] public Vector3 bottomLeftLimit;
        [HideInInspector] public Vector3 topRightLimit;

        private float _halfHeight;
        private float _halfWidth;

        // audio
        public int musicToPlay;
        private bool musicStarted;
        public int sfxToPlay;

        private void Start()
        {
            target = FindObjectOfType<PlayerController>().transform;

            UpdateBounds();
        }

        public void UpdateBounds()
        {
            if (Camera.main != null)
            {
                var main = Camera.main;
                _halfHeight = main.orthographicSize;
                _halfWidth = _halfHeight * main.aspect;
            }

            var localBounds = theMap.localBounds;
            bottomLeftLimit = localBounds.min + new Vector3(_halfWidth, _halfHeight, 0f);
            topRightLimit = localBounds.max + new Vector3(-_halfWidth, -_halfHeight, 0f);

            PlayerController.Instance.SetBounds(localBounds.min, localBounds.max);
        }

        private void LateUpdate()
        {
            if (transform.position == target.position) return;

            var cameraPosition = transform.position;
            var playerPosition = target.position;
            var newTargetPosition = new Vector3(playerPosition.x, playerPosition.y, cameraPosition.z);
            cameraPosition = Vector3.Lerp(cameraPosition, newTargetPosition, smoothing);

            //// keep the camera inside the bounds
            cameraPosition = new Vector3(Mathf.Clamp(cameraPosition.x, bottomLeftLimit.x, topRightLimit.x),
                Mathf.Clamp(cameraPosition.y, bottomLeftLimit.y, topRightLimit.y),
                cameraPosition.z);
            transform.position = cameraPosition;

            if (musicStarted) return;
            musicStarted = true;
            AudioManager.Instance.PlayBGM(musicToPlay);
        }
    }
}
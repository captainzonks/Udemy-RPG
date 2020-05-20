using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float smoothing;

        public Tilemap theMap;
        private Vector3 _bottomLeftLimit;
        private Vector3 _topRightLimit;

        private float _halfHeight;
        private float _halfWidth;

        private void Start()
        {
            target = FindObjectOfType<PlayerController>().transform;

            if (Camera.main != null)
            {
                var main = Camera.main;
                _halfHeight = main.orthographicSize;
                _halfWidth = _halfHeight * main.aspect;
            }

            var localBounds = theMap.localBounds;
            _bottomLeftLimit = localBounds.min + new Vector3(_halfWidth, _halfHeight, 0f);
            _topRightLimit = localBounds.max + new Vector3(-_halfWidth, -_halfHeight, 0f);

            PlayerController.Instance.SetBounds(localBounds.min, localBounds.max);
        }

        private void LateUpdate()
        {
            if (transform.position == target.position) return;

            var transformPosition = transform.position;
            var targetPosition = target.position;
            var newTargetPosition = new Vector3(targetPosition.x, targetPosition.y, transformPosition.z);
            transformPosition = Vector3.Lerp(transformPosition, newTargetPosition, smoothing);

            //// keep the camera inside the bounds
            transformPosition = new Vector3(Mathf.Clamp(transformPosition.x, _bottomLeftLimit.x, _topRightLimit.x),
                Mathf.Clamp(transformPosition.y, _bottomLeftLimit.y, _topRightLimit.y),
                transformPosition.z);
            transform.position = transformPosition;
        }
    }
}
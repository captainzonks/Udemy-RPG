﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace Movement
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Transform target;

        [SerializeField] Tilemap theMap;
        Vector3 _bottomLeftLimit;
        Vector3 _topRightLimit;

        float _halfHeight;
        float _halfWidth;

        // Start is called before the first frame update
        void Start()
        {
            //target = PlayerController.instance.transform;
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

            PlayerController.instance.SetBounds(localBounds.min, localBounds.max);
        }

        // LateUpdate is called once per frame after Update
        void LateUpdate()
        {
            var transformPosition = transform.position;
            var targetPosition = target.position;
            transformPosition = new Vector3(targetPosition.x, targetPosition.y, transformPosition.z);

            // keep the camera inside the bounds
            transformPosition = new Vector3(Mathf.Clamp(transformPosition.x, _bottomLeftLimit.x, _topRightLimit.x),
                Mathf.Clamp(transformPosition.y, _bottomLeftLimit.y, _topRightLimit.y),
                transformPosition.z);
            transform.position = transformPosition;
        }
    }
}
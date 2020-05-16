using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    private void Start()
    {
        //target = PlayerController.instance.transform;
        target = FindObjectOfType<PlayerController>().transform;

        if (Camera.main != null)
        {
            var main = Camera.main;
            halfHeight = main.orthographicSize;
            halfWidth = halfHeight * main.aspect;
        }

        var localBounds = theMap.localBounds;
        bottomLeftLimit = localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        PlayerController.instance.SetBounds(localBounds.min, localBounds.max);
    }

    // LateUpdate is called once per frame after Update
    private void LateUpdate()
    {
        var transformPosition = transform.position;
        var targetPosition = target.position;
        transformPosition = new Vector3(targetPosition.x, targetPosition.y, transformPosition.z);

        // keep the camera inside the bounds
        transformPosition = new Vector3(Mathf.Clamp(transformPosition.x, bottomLeftLimit.x, topRightLimit.x), 
            Mathf.Clamp(transformPosition.y, bottomLeftLimit.y, topRightLimit.y), 
            transformPosition.z);
        transform.position = transformPosition;
    }
}

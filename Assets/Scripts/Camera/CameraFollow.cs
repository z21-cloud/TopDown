using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float speed = 2f;

    [SerializeField] private Vector2 offset = new Vector2(0f, 0f);

    [SerializeField] private Camera mainCamera;
    private Vector3 minBackgroundBounds;
    private Vector3 maxBackgroundBounds;
    private float cameraHalfHeight;
    private float cameraHalfWidth;

    private void Start()
    {

        CalculateBackgroundBounds();
        CalculateCameraSize();
    }

    private void CalculateBackgroundBounds()
    {
        Collider2D backgroundCollider = background.GetComponent<Collider2D>();
        if(backgroundCollider != null)
        {
            minBackgroundBounds = backgroundCollider.bounds.min;
            maxBackgroundBounds = backgroundCollider.bounds.max;
        }
        else
        {
            minBackgroundBounds = background.bounds.min;
            maxBackgroundBounds = background.bounds.max;
        }
    }

    private void CalculateCameraSize()
    {
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * mainCamera.aspect;
    }

    private void OnDrawGizmosSelected()
    {
        if (background == null) return;

        CalculateBackgroundBounds();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((minBackgroundBounds + maxBackgroundBounds) / 2,
                            maxBackgroundBounds - minBackgroundBounds);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform == null) return;

        Vector3 targetPosition = playerTransform.position + (Vector3)offset;
        targetPosition.z = transform.position.z;

        float clampedX = Mathf.Clamp(
                targetPosition.x,
                minBackgroundBounds.x + cameraHalfWidth,
                maxBackgroundBounds.x - cameraHalfWidth
            );

        float clampedY = Mathf.Clamp(
                targetPosition.y,
                minBackgroundBounds.y + cameraHalfHeight,
                maxBackgroundBounds.y - cameraHalfHeight
            );

        transform.position = new Vector3(clampedX, clampedY, targetPosition.z);
    }
}

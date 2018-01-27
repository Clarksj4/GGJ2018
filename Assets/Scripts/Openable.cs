using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    public XWindow xWindowPrefab;
    public RectTransform canvas;
    public content Content;

    private XWindow openInstance;
    private Coroutine dragging;
    private Vector3 startPosition;

    private void OnMouseDown()
    {
        // Save location
        startPosition = transform.position;

        // Begin drag
        dragging = StartCoroutine(DoDrag());
    }

    private void OnMouseUp()
    {
        // Stop drag
        StopCoroutine(dragging);

        // if not on a folder, go home
        transform.position = startPosition;
    }

    private Vector2 WorldPositionToCanvasPosition()
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 canvasPosition = new Vector2(
        ((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

        return canvasPosition;
    }

    private Vector3 mousePositionToWorldPosition()
    {
        Vector3 screenPoint = (Input.mousePosition);
        screenPoint.z = 10.0f; //distance of the plane from the camera
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }

    IEnumerator DoDrag()
    {
        // Drag until coroutine is stopped
        while (true)
        {
            // Move icon to cursor position
            transform.position = mousePositionToWorldPosition();

            yield return null;
        }
    }
}

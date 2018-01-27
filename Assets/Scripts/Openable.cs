using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour
{
    public XWindow xWindowPrefab;
    public RectTransform canvas;
    public content Content;
    public float openMaxHoldTime = 0.5f;

    private XWindow openInstance;
    private Coroutine dragging;
    private Vector3 startPosition;
    private new BoxCollider2D collider;
    private Collider2D touchedCollider;
    private float downTime;

    private void OnMouseDown()
    {
        downTime = Time.time;

        // Save location
        startPosition = transform.position;

        // Begin drag
        dragging = StartCoroutine(DoDrag());
    }

    private void OnMouseUp()
    {
        float currentTime = Time.time;
        float timeDelta = currentTime - downTime;

        if (timeDelta < openMaxHoldTime)
        {
            if (openInstance == null)
            {
                openInstance = Instantiate(xWindowPrefab, transform.parent) as XWindow;
                openInstance.Open(Content, WorldPositionToCanvasPosition(), Vector2.zero);
            }

            else
            {
                openInstance.Close(WorldPositionToCanvasPosition());
            }
        }

        if (dragging != null)
        {
            // Stop drag
            StopCoroutine(dragging);
    
            // If over a folder
            if (touchedCollider != null)
            {
                if (openInstance != null)
                    openInstance.Close(WorldPositionToCanvasPosition());
                
                Dropbox dropBox = touchedCollider.GetComponent<Dropbox>();

                Debug.Log(Content.proper_destination);
                Debug.Log(dropBox.DropDestination);
                GameMaster.gameMaster.HandleDrop(Content.proper_destination, dropBox.DropDestination);
                Destroy(gameObject);
            }

            else
            {
                // if not on a folder, go home
                transform.position = startPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        touchedCollider = collision.GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D exitedCollider = collision.GetComponent<Collider2D>();
        if (exitedCollider == touchedCollider)
        {
            touchedCollider = null;
        }
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

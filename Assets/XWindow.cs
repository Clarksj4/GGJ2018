using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XWindow : MonoBehaviour
{
    public float OpenTime = 1;

    private new RectTransform transform;
    private Text title;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        title = GetComponentInChildren<Text>(true);
    }

    public void Open(string mediaPath)
    {

    }

    public void Close(Vector2 destination)
    {
        StartCoroutine(DoClose(destination));
    }

    public void Open(Vector2 fromPosition, Vector2 toPosition, Vector2 size)
    {
        // Start Coroutine to expand window @ position to given size
        StartCoroutine(DoOpen(fromPosition, toPosition, size));
    }

    IEnumerator DoOpen(Vector2 fromPosition, Vector2 toPosition, Vector2 size)
    {
        // Move to position, set scale to zero
        transform.anchoredPosition = fromPosition;
        transform.sizeDelta = Vector2.zero;
        float time = 0;

        while (time < OpenTime)
        {
            // Proprotion of scaling that has occured
            float t = time / OpenTime;
            
            // Calculate and set scale at this frame
            Vector2 newScale = Vector2.Lerp(Vector2.zero, size, t);
            transform.sizeDelta = newScale;

            Vector2 newPosition = Vector2.Lerp(fromPosition, toPosition, t);
            transform.anchoredPosition = newPosition;

            // Update time
            time += Time.deltaTime;

            yield return null;
        }

        // Set to desired scale
        transform.anchoredPosition = toPosition;
        transform.sizeDelta = size;
        title.gameObject.SetActive(true);
    }

    IEnumerator DoClose(Vector2 destination)
    {
        float time = 0;
        Vector2 startingScale = transform.sizeDelta;
        Vector2 startingPosition = transform.anchoredPosition;

        while (time < OpenTime)
        {
            // Proprotion of scaling that has occured
            float t = time / OpenTime;

            // Calculate and set scale at this frame
            Vector2 newScale = Vector2.Lerp(startingScale, Vector2.zero, t);
            transform.sizeDelta = newScale;

            Vector2 newPosition = Vector2.Lerp(startingPosition, destination, t);
            transform.anchoredPosition = newPosition;

            // Update time
            time += Time.deltaTime;

            yield return null;
        }

        // Set to desired scale
        transform.sizeDelta = Vector2.zero;

        Destroy(gameObject);
    }
}

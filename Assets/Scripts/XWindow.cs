using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class XWindow : MonoBehaviour
{
    public event EventHandler OnClose;

    public float OpenTime = 1;
    public RectTransform ContentPanel;

    private new RectTransform transform;
    private Text title;

    private GameObject message_prefab;

    private void Awake()
    {
        transform = GetComponent<RectTransform>();
        title = GetComponentInChildren<Text>(true);
    }

    public void CloseButtonPressed()
    {
        if (OnClose != null)
            OnClose(this, new EventArgs());

        Close(Vector2.zero);
    }

    public void Open(content content, Vector2 fromPosition, Vector2 toPosition)
    {
        StartCoroutine(DoOpen(content, fromPosition, toPosition));
    }

    public void Maximize(Vector2 fromPosition, Vector2 toPosition)
    {
        StartCoroutine(DoMaximize(fromPosition, toPosition, transform.sizeDelta));
    }

    public void Minimize(Vector2 destination)
    {
        StartCoroutine(DoMinimize(destination));
    }

    public void Close(Vector2 destination)
    {
        StartCoroutine(DoClose(destination));
    }

    IEnumerator DoOpen(content content, Vector2 fromPosition, Vector2 toPosition)
    {
        // Maximize window
        //Sprite sprite = Resources.Load<Sprite>(content.content_name);

        Vector2 size = content.content_prefab.GetComponent<RectTransform>().sizeDelta;
        size.x = size.x + 8;
        size.y = size.y + 27;

        StartCoroutine(DoMaximize(fromPosition, toPosition, size));

        // Wait until maximized
        yield return new WaitForSeconds(OpenTime);

        // Add content to window
        LoadContent(content);
    }

    IEnumerator DoClose(Vector2 destination)
    {
        // Minimize window
        StartCoroutine(DoMinimize(destination));

        // Wait until minimized
        yield return new WaitForSeconds(OpenTime);

        // Kill window
        Destroy(gameObject);
    }

    // Adds the component that will display the content and puts the content it
    private void LoadContent(content content)
    {
        message_prefab = Instantiate(content.content_prefab,ContentPanel);
        title.text = content.title_bar_label;
    }

    //Murder our loaded content with murder
    private void UnloadContent(content content)
    {
        Destroy(content.content_prefab);
    }

    IEnumerator DoMaximize(Vector2 fromPosition, Vector2 toPosition, Vector2 size)
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

    IEnumerator DoMinimize(Vector2 destination)
    {
        Destroy(message_prefab);
        ContentPanel.gameObject.SetActive(false);
        
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
    }
}

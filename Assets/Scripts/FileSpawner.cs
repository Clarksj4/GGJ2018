using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSpawner : MonoBehaviour
{
    public Openable FilePrefab;
    public RectTransform Canvas;

    private content_creator contentCreator = new content_creator();
    private Openable currentFile;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, 2);
    }

    public void Spawn()
    {
        if (currentFile == null)
        {
            // Get random content
            content randomContent = contentCreator.get_me_content();

            // Create a file object at position
            currentFile = Instantiate(FilePrefab, transform.position, Quaternion.identity, transform.parent);
            currentFile.canvas = Canvas;

            // Give file object content
            currentFile.Content = randomContent;
        }
    }
}

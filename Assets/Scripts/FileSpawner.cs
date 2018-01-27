using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSpawner : MonoBehaviour
{
    public Openable FilePrefab;
    public RectTransform Canvas;
    public List<content> content_items;
    static System.Random rnd = new System.Random();       // Lets keep random random
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
            content randomContent = get_me_content();

            // Create a file object at position
            currentFile = Instantiate(FilePrefab, transform.position, Quaternion.identity, transform.parent);
            currentFile.canvas = Canvas;

            // Give file object content
            currentFile.Content = randomContent;
        }
    }

    public content get_me_content()
    {
        int r = rnd.Next(content_items.Count);
        Debug.Log(content_items[r].content_prefab.ToString());
        return content_items[r];
    }
}

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
    private bool[] viewed_message;
    private int total_viewed =0;

    private void Start()
    {
        Invoke("Spawn", 0);
        viewed_message = new bool[content_items.Count];
    }

    public void Spawn()
    {
        //if (GameMaster.gameMaster.messageCounter == GameMaster.gameMaster.totalMessageNumberTarget)
        if (GameMaster.gameMaster.messageCounter == 2)
        {
            GameMaster.gameMaster.LoadScene("result");
        }
        else if (currentFile == null)
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
        //choose a random number
        int r = rnd.Next(content_items.Count);

        //check to see if we have already used this response. If so, used another 
        while (viewed_message[r])
        {
            r = rnd.Next(content_items.Count);
        }

        //increase our count, marked our message as viewed
        total_viewed++;
        viewed_message[r] = true;

        //reset our viewed messages if the have all been viewed
        if (total_viewed == content_items.Count)
        {
            viewed_message = new bool[content_items.Count];
        }

        //return the things.
        Debug.Log(content_items[r].content_prefab.ToString());
        return content_items[r];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class content_creator : MonoBehaviour {

    static System.Random rnd = new System.Random();       // Lets keep random random
    public List<content>  content_items;    // hold our content

    public content_creator()
    {
        List<content> content_items = new List<content>
        {
            new content() { content_name = "cabbage_recipe", content_type = content.content_types.image, proper_destination = content.proper_destinations.grandma},
            new content() { content_name = "spam_can", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            new content() { content_name = "puppy_and_kitten", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended}

            // new content here
        };
    }


    public content get_me_content() 
    {
        int r = rnd.Next(content_items.Count);

        return content_items[r];
    }


}

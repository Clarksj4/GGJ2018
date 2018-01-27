using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class content_creator {

    static System.Random rnd = new System.Random();       // Lets keep random random
    public List<content>  content_items;    // hold our content

    public content_creator()
    {
        content_items = new List<content>
        {
            new content() { content_name = "cabbage_recipe", content_type = content.content_types.image, proper_destination = content.proper_destinations.grandma},
            new content() { content_name = "spam_can", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            new content() { content_name = "puppy_and_kitten", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            new content() { content_name = "Banana Shack", content_type = content.content_types.image, proper_destination = content.proper_destinations.cia},
            new content() { content_name = "Carl's guns", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            new content() { content_name = "Cocaine Girl", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},

            new content() { content_name = "Pregante", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            new content() { content_name = "Shona's Food Blog", content_type = content.content_types.image, proper_destination = content.proper_destinations.cia},
            new content() { content_name = "Millionaire", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended},
            //new content() { content_name = "Malicious", content_type = content.content_types.video, proper_destination = content.proper_destinations.intended}
            //new content() { content_name = "puppy_and_kitten", content_type = content.content_types.image, proper_destination = content.proper_destinations.intended}

            // new content here
        };
    }


    public content get_me_content() 
    {
        int r = rnd.Next(content_items.Count);
        Debug.Log(content_items[r].content_name);

        return content_items[r];
    }


}

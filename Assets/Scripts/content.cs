using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class content {

    public enum content_types { image, video, audio };
    public enum proper_destinations { grandma, intended, cia };

    public string content_name { get; set; }
    public content_types content_type { get; set; }
    public proper_destinations proper_destination { get; set; }
}

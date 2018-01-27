using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Enums;

namespace Enums
{
    public enum proper_destinations { grandma, intended, cia };
}


[Serializable]
public struct content {
    public GameObject content_prefab;
    public proper_destinations proper_destination;
    public string title_bar_label;
}

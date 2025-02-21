using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ConsumableData
{
    public string id = "";
    public string name = "???";
    public string description = "???";
    
    public bool instant = false;
}

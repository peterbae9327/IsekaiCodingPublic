using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCChat : CSVControll
{
    private string eventName;
    
    public int Day { get; private set; }

    NPCEvent npcEvent;

    void Awake()
    {
        LoadCSV("NewGirlCSV");
    }
}

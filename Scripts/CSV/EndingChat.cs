using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingChat : CSVControll
{
    private string eventName;

    private void Awake()
    {
        LoadCSV("EndingCSV");
    }


}

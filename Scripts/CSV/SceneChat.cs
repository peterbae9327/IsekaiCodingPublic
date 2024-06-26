using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneIndex
{
    Start,
    Select
}

public class SceneChat : CSVControll
{
    // Start is called before the first frame update
    void Awake()
    {
        LoadCSV("메인화면CSV");
    }
}

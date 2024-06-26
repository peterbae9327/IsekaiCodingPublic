using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public CSVControll sceneCSV { get; private set; }
    public JsonControll Json { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        sceneCSV = GetComponent<CSVControll>();
        Json = GetComponent<JsonControll>();

        sceneCSV.LoadCSV("메인화면CSV");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputName;

    private void Start()
    {
        inputName.text =  DataManager.Instance.sceneCSV.GetDescript("Start",0);
    }
}

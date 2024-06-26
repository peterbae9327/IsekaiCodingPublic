using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueCSV : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PrologueText;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            Debug.Log("Start Scene");
            PrologueText.text = DataManager.Instance.sceneCSV.GetDescript("Prologue", 0);
        }
        else if (SceneManager.GetActiveScene().name == "SelectScene")
        {
            Debug.Log("Select Scene");
            PrologueText.text = DataManager.Instance.sceneCSV.GetDescript("Prologue", 1);
        }
    }

    public void SettingPlace(int index)
    {
        PrologueText.text = DataManager.Instance.sceneCSV.GetDescript("Prologue", index);
    }
}

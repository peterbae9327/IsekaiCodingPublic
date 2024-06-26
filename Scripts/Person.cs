using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Person : MonoBehaviour
{
    public string personName;
    public string personDescription;

    public UIManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Debug.Log("UI∫∏¿Ã±‚");
            manager.npcPanel.SetActive(true);
        }
        else if (collision.gameObject.tag == "Ending")
        {
            SceneManager.LoadScene("EndingScene");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        manager.npcPanel.SetActive(false);
    }
}

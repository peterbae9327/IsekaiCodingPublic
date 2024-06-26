using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EndingUI : UIManager
{
    private EndingChat endChat;

    private enum EndingBG
    {
        endingBG1,
        endingBG2
    }
    EndingBG endingBG;

    public GameObject ending;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject narration;
    public GameObject NextBtn;
    public GameObject EndBtn;

    [SerializeField] private TextMeshProUGUI EndDescriptTxt;
    private TextMeshProUGUI endingNaration;
    private int nowIndex = 0;
    private string eventName;


    private void Awake()
    {
        endChat = GetComponent<EndingChat>();
        endingNaration = narration.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        EndBtn.SetActive(false);
        NextBtn.SetActive(true);

        nowIndex = 0;

        //charm을 GameManager.Instane.player.charm으로 변경부탁
        if (GameManager.Instance.npc.GetRelationship() <= 1)
        {
            ending.SetActive(true);
            ending1.SetActive(true);
            endingBG = EndingBG.endingBG1;
            eventName = "Ending1";
        }
        else if (GameManager.Instance.npc.GetRelationship() >= 2)
        {
            ending.SetActive(true);
            ending2.SetActive(true);
            endingBG = EndingBG.endingBG2;
            eventName = "Ending2";
        }
        EndDescriptTxt.text = string.Empty;
        ApplyText();
    }

    public void NextTxt()
    {
        EndDescriptTxt.text = string.Empty;
        nowIndex++;
        if (endChat.GetEventSize(eventName) > nowIndex)
        {
            if(endChat.GetName("Ending2",nowIndex).Equals("Narration"))
            {
                TxtEnd();
            }

            if(nowIndex == endChat.GetEventSize(eventName) - 1)
            {
                EndBtn.SetActive(true);
                NextBtn.SetActive(false);
            }
            ApplyText();
        }
        else
            TxtEnd();
        //switch (endingBG)
        //{
        //    case EndingBG.endingBG1:
        //    case EndingBG.endingBG2:
        //        if (마지막 텍스트면)
        //        {
        //            txtEnd.SetActive(true);
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }

    public void TxtEnd()
    {
        if (endingBG == EndingBG.endingBG1)
        {
            ending1.SetActive(false);
            ending.SetActive(false);
        }
        else if (endingBG == EndingBG.endingBG2)
        {
            ending2.SetActive(false);
            ending.SetActive(false);
            StartCoroutine(EndingPanel());
        }
    }

    IEnumerator EndingPanel()
    {
        prologue.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            endingNaration.text = endChat.GetDescript(eventName, nowIndex);
            nowIndex++;
            yield return new WaitForSeconds(2);
        }
        prologue.SetActive(false);
    }

    private void ApplyText()
    {
        NextBtn.SetActive(false);
        StartCoroutine(AddText(endChat.GetDescript(eventName, nowIndex)));
    }
    IEnumerator AddText(string descrpt)
    {
        for (int i = 0; i < descrpt.Length; i++)
        {
            EndDescriptTxt.text += descrpt[i];
            yield return new WaitForSeconds(0.1f);
        }
        //다음 버튼 표시
        NextBtn.SetActive(true);
    }

    public void RetryButton()
    {
        //처음부터 다시 할건지?
    }
}

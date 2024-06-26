using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DayTime
{
    Morning,
    Afternoon,
    Evening,
    Nothing //시간대가 없는 내용일 때
}
public enum NPCEvent
{
    Conversation,
    Goodbye
}

public class DialogUI : MonoBehaviour
{
    [SerializeField] private GameObject Morning;
    [SerializeField] private GameObject Afternoon;
    [SerializeField] private GameObject Evening;
    [SerializeField] private GameObject SelectPanel;
    [SerializeField] private TextMeshProUGUI NPCDescriptText;

    [SerializeField] private Button NextBtn, EndBtn;
    [SerializeField] private Button Select1, Select2;
    private TextMeshProUGUI SelectTxt1, SelectTxt2;

    PlayerChoiceChat playerChat;
    NPCChat npcChat;

    public DayTime nowTime { get; private set; }

    int playerPhase;
    int npcPhase;
    int buttonSelected = 0;

    bool badEnd = false;

    private void Awake()
    {
        playerChat = GetComponent<PlayerChoiceChat>();
        npcChat = GetComponent<NPCChat>();

        Morning.SetActive(false);
        Afternoon.SetActive(false);
        Evening.SetActive(false);
        SelectPanel.SetActive(false);
        NextBtn.gameObject.SetActive(false);
        EndBtn.gameObject.SetActive(false);
        NPCDescriptText.text = string.Empty;

        SelectTxt1 = Select1.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        SelectTxt2 = Select2.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        nowTime = DayTime.Morning;
        playerPhase = 0;
        npcPhase = 0;
    }

    private void OnEnable()
    {
        //초기설정
        NPCDescriptText.text = string.Empty;
        DescriptGirl();
    }

    private void DescriptGirl()
    {
        Morning.SetActive(false);
        Afternoon.SetActive(false);
        Evening.SetActive(false);

        switch (nowTime)
        {
            case DayTime.Morning:
                Morning.SetActive(true);
                break;
            case DayTime.Afternoon:
                Afternoon.SetActive(true);
                break;
            case DayTime.Evening:
                Evening.SetActive(true);
                break;
        }

        //DescriptText.text = playerChat.GetDescriptInfo(0, 0).description;
        if (npcChat.GetDescript(nowTime.ToString(), 0) == null)
        {
            npcChat.LoadCSV("NewGirlCSV");
        }
        TextEffect(npcChat.GetDescript(nowTime.ToString(), 0));
    }

    private void TextEffect(string description)
    {
        StartCoroutine(AddText(description));
    }
    IEnumerator AddText(string descrpt)
    {

        if (descrpt.Contains("<Name>"))
            descrpt = descrpt.Replace("<Name>", GameManager.Instance.player.personName);
        int txtLength = descrpt.Length;

        for (int i = 0; i < txtLength; i++)
        {
            NPCDescriptText.text += descrpt[i];
            yield return new WaitForSeconds(0.1f);
        }

        //플레이어 선택 (선택지가 나오면)
        if (npcChat.GetName(nowTime.ToString(), npcPhase).Contains("대화하기"))
        {
            SelectPanel.SetActive(true);
            NextBtn.gameObject.SetActive(false);
            EndBtn.gameObject.SetActive(false);
            SelectTxt1.text = playerChat.GetDescriptInfo(0, playerPhase).description;
            SelectTxt2.text = playerChat.GetDescriptInfo(0, playerPhase+1).description;
        }
        else
        {
            //선택지가 없고 다음으로 버튼이 나올 때
            //대화 내용이 마지막이면
            if (npcChat.GetEventSize(nowTime.ToString()) > npcPhase - 1)
            {
                EndBtn.gameObject.SetActive(true);
                NextBtn.gameObject.SetActive(false);
            }
            else
            {
                EndBtn.gameObject.SetActive(false);
                NextBtn.gameObject.SetActive(true);
            }
            
            SelectPanel.SetActive(false);
        }
    }

    public void ButtonSelected1()
    {
        NPCDescriptText.text = string.Empty;
        //대화 넘어감
        playerPhase += 2;
        npcPhase += 1;

        buttonSelected = 1;
        StartCoroutine(AddText(npcChat.GetDescript(nowTime.ToString(), npcPhase)));

        SelectPanel.SetActive(false);
    }

    public void ButtonSelected2()
    {
        NPCDescriptText.text = string.Empty;
        //대화 넘어감
        playerPhase += 2;
        npcPhase += 2;

        buttonSelected = 2;
        StartCoroutine(AddText(npcChat.GetDescript(nowTime.ToString(), npcPhase)));

        SelectPanel.SetActive(false);
    }

    public void NextText()
    {
        NPCDescriptText.text = string.Empty;

        if (buttonSelected == 1)
            npcPhase += 2;
        else if (buttonSelected == 2)
            npcPhase += 1;

        DescriptGirl();
    }

    public void EndText()   //대화종료
    {
        if (NPCDescriptText.text.Contains("(Bad End)"))
            GameManager.Instance.npc.SubtractRelationship(1);
        else
            GameManager.Instance.npc.AddRelationship(1); //대화 끝나면 호감도 증가(?)

        switch (nowTime)
        {
            case DayTime.Morning:
                nowTime = DayTime.Afternoon;
                break;
            case DayTime.Afternoon:
                nowTime = DayTime.Evening;
                break;
            case DayTime.Evening:
                //엔딩씬으로
                SceneManager.LoadScene("EndingScene");
                break;
        }
        npcPhase = 0;
        gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject opt;
    public GameObject info;
    public GameObject help;
    public GameObject ask;
    public GameObject impossible;
    public TextMeshProUGUI impossibleText;
    public GameObject prologue;
    public GameObject npcPanel;
    public GameObject dialoguePanel;
    public GameObject volume;

    public TMP_InputField nameInput;


    public TextMeshProUGUI[] text;
    public TextMeshProUGUI[] askTxt;

    private enum Select
    {
        isSchool = 1,
        isField,
        isLibrary,
        isSkip
    }
    Select select;
    private int isStage;    //현재 스테이지 상태 (1.학교, 2.운동장, 3.도서관)

    //private bool isSchool = false;
    //private bool isLibrary = false;
    //private bool isField = false;
    //private bool isSkip = false;

    private string[] impossibleInfo = new string[3];

    private void Start()
    {
        //astTxt 세팅
        for (int i = 0; i < askTxt.Length; i++)
            askTxt[i].text = DataManager.Instance.sceneCSV.GetDescript("Select", i);

        for (int i = 0; i < impossibleInfo.Length; i++)
        {
            impossibleInfo[i] = DataManager.Instance.sceneCSV.GetDescript("Impossible", i);
            Debug.Log(impossibleInfo[i]);
        }

        //스테이지 초기 (저장 데이터를 불러와야 됨)
        isStage = 1;
    }

    public void StartGame()
    {
        if (nameInput.text.Length < 1)
        {
            return;
        }
        GameManager.Instance.player.personName = nameInput.text;
        StartCoroutine(GoSelect());
    }

    IEnumerator GoSelect()
    {
        prologue.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SelectScene");
    }

    public void OptionOn()
    {
        opt.SetActive(true);
    }

    public void OptionOff()
    {
        opt.SetActive(false);
    }

    public void VolumeSettingOn()
    {
        volume.SetActive(true);
    }

    public void VolumeSettingOff()
    {
        volume.SetActive(false);
    }

    public void GoHome()
    {

        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

    public void InfoOn()
    {
        info.SetActive(true);
        UpdateUI();
    }

    public void InfoOff()
    {
        info.SetActive(false);
    }

    public void HelpOn()
    {
        help.SetActive(true);
    }

    public void HelpOff()
    {
        help.SetActive(false);
    }

    public void AskSchool()
    {
        select = Select.isSchool;
        ask.SetActive(true);
        askTxt[0].gameObject.SetActive(true);
        //isSchool = true;
    }

    public void AskLibrary()
    {
        select = Select.isLibrary;
        ask.SetActive(true);
        askTxt[2].gameObject.SetActive(true);
        // 도서관 구현 시 true
    }

    public void AskField()
    {
        select = Select.isField;
        ask.SetActive(true);
        askTxt[1].gameObject.SetActive(true);
        // 운동장 구현 시 true
    }

    public void AskSkipDay()
    {
        select = Select.isSkip;
        ask.SetActive(true);
        askTxt[3].gameObject.SetActive(true);
        //isSkip = true;
    }

    public void Yes()
    {
        //yes에 누를 경우 (scene로 넘어가면) //저장데이터를 받아와야 할것 같음
        switch (select)
        {
            case Select.isSchool:
            case Select.isField:
            case Select.isLibrary:
                if (isStage >= (int)select)
                {
                    StartCoroutine(GoMain());
                    ask.SetActive(false);
                    for (int i = 0; i < text.Length; i++)
                    {
                        askTxt[i].gameObject.SetActive(false);
                    }
                }
                //불가능할 경우
                else
                {
                    ask.SetActive(false);
                    impossible.SetActive(true);
                    impossibleText.text = impossibleInfo[(int)select - 1];
                }
                break;
            case Select.isSkip:
                // 시간 구현되면 하루 흐르게 하기
                break;
            default:
                //오류 방지 코드
                return;
        }
        //if (isSchool == true || isLibrary == true || isField == true)
        //{
        //    SceneManager.LoadScene("MainScene");
        //    ask.SetActive(false);
        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        askTxt[i].gameObject.SetActive(false);
        //    }
        //}
        //else if (isSkip == true)
        //{
        //    // 시간 구현되면 하루 흐르게 하기
        //}
        //else
        //{
        //    switch (select)
        //    {
        //        case Select.isSchool:
        //            ask.SetActive(false);
        //            impossible.SetActive(true);
        //            impossibleText.text = impossibleInfo[0];
        //            break;
        //        case Select.isField:
        //            ask.SetActive(false);
        //            impossible.SetActive(true);
        //            impossibleText.text = impossibleInfo[1];
        //            break;
        //        case Select.isLibrary:
        //            ask.SetActive(false);
        //            impossible.SetActive(true);
        //            impossibleText.text = impossibleInfo[2];
        //            break;
        //        default:
        //            return;
        //    }
        //}
    }

    IEnumerator GoMain()
    {
        prologue.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }

    public void No()
    {
        //isSchool = false;
        //isLibrary = false;
        //isField = false;
        //isSkip = false;
        ask.SetActive(false);
        for (int i = 0; i < text.Length; i++)
        {
            askTxt[i].gameObject.SetActive(false);
        }
    }

    public void Ok()
    {
        impossible.SetActive(false);
        for (int i = 0; i < text.Length; i++)
        {
            askTxt[i].gameObject.SetActive(false);
        }
    }

    public void Allowance()
    {
        // Time 만들어 지면 하루에 한번만 받을 수 있게
        GameManager.Instance.player.AddStat(Stats.Money, 5000);
        UpdateUI();
    }

    public void BuyCharm()
    {
        if (GameManager.Instance.player.money >= 1000)
        {
            GameManager.Instance.player.AddStat(Stats.Charm, 1);
            GameManager.Instance.player.SubtractStat(Stats.Money, 1000);
            UpdateUI();
        }
    }

    public void BuyKnowledge()
    {
        if (GameManager.Instance.player.money >= 1000)
        {
            GameManager.Instance.player.AddStat(Stats.Knowledge, 1);
            GameManager.Instance.player.SubtractStat(Stats.Money, 1000);
            UpdateUI();
        }
    }

    public void BuyStrength()
    {
        if (GameManager.Instance.player.money >= 1000)
        {

            GameManager.Instance.player.AddStat(Stats.Strength, 1);
            GameManager.Instance.player.SubtractStat(Stats.Money, 1000);
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        text[0].text = GameManager.Instance.player.money.ToString();
        text[1].text = GameManager.Instance.player.charm.ToString();
        text[2].text = GameManager.Instance.player.knowledge.ToString();
        text[3].text = GameManager.Instance.player.strength.ToString();
    }

    public void DialogueOn()
    {
        dialoguePanel.SetActive(true);
        npcPanel.SetActive(false);
        // 시간이 아침이면 Moring true
        // 점심이면 afternoon true
        // 저녁이면 evening true
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoiceChat : CSVControll
{
    public class CharmDescription  //호감도에 따른 데스크립션
    {
        public DayTime time { get; private set; }    //아침, 점심
        public bool hasSelect;    //선택지가 포함되어 있는가?
        public string description { get; private set; } //내용

        public CharmDescription(string phase, string description)
        {
            if (phase.Contains("아침"))
                time = DayTime.Morning;
            else if (phase.Contains("점심"))
                time = DayTime.Afternoon;
            else if (phase.Contains("하교"))
                time = DayTime.Evening;
            else
                time = DayTime.Nothing;
            this.description = description;
            hasSelect = phase.Contains("선택지");
        }
    }

    private string eventName;

    //플레이어 호감도에 따른 사전형 예를 들어 0따로 1따로
    Dictionary<int, List<CharmDescription>> playerDescription = new Dictionary<int, List<CharmDescription>>();

    void Awake()
    {
        LoadCSV("PlayerCSV");
        eventName = "Interaction";

        SettingCharm();
    }

    private void SettingCharm()
    {
        for (int i = 0; i <= 2; i++) //호감도 사전 초기화
            playerDescription[i] = new List<CharmDescription>();

        foreach (CSVData dict in chatData[eventName])
        {
            if (dict.Name.Contains("호감도0"))
            {
                playerDescription[0].Add(new CharmDescription(dict.Name,dict.Descript));
            }
            else if(dict.Name.Contains("호감도1"))
            {
                playerDescription[1].Add(new CharmDescription(dict.Name, dict.Descript));
            }
            else if(dict.Name.Contains("호감도2"))
            {
                playerDescription[2].Add(new CharmDescription(dict.Name, dict.Descript));
            }
        }
    }

    public CharmDescription GetDescriptInfo(int charm, int index)
    {
        return playerDescription[charm][index];
    }

    //Todo : 플레이어의 호감도를 받아서
    //CSV의 Name에서 호감도n을 분리시켜서 리스트를 따로 만들어보자
}

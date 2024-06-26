using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CSVData
{
    public string Event;
    public string Name;
    public string Descript;

    public CSVData(string _event, string _name, string _descript)
    {
        Event = _event;
        Name = _name;
        Descript = _descript;
    }
}

public class CSVControll : MonoBehaviour
{
    protected Dictionary<string, List<CSVData>> chatData = new Dictionary<string, List<CSVData>>();
    string csvPath = "CSV/";

    //여기에 CSV파일을 불러옴
    public void LoadCSV(string fileName)
    {
        //csv파일 로드
        var csvData = Resources.Load<TextAsset>(csvPath + fileName);
        Deserial(csvData.text.TrimEnd());
    }

    private void Deserial(string data)
    {
        var rowData = data.Split('\n');

        for (int i = 0; i < rowData.Length; i++)
        {
            var datas = rowData[i].Split('/');

            if (chatData.ContainsKey(datas[0]))
            {
                chatData[datas[0]].Add(new CSVData(datas[0], datas[2], datas[3]));
            }
            else
            {
                chatData[datas[0]] = new List<CSVData>();
                chatData[datas[0]].Add(new CSVData(datas[0], datas[2], datas[3]));
            }
        }
    }

    //해당하는 이벤트의 인덱스를 반환
    public string GetName(string _event,int index) //스테이지, 챕터
    {
        if (chatData.ContainsKey(_event))
            return chatData[_event][index].Name;
        else
            return null;
    }

    public string GetDescript(string _event,int index)
    {
        if (chatData.ContainsKey(_event))
            return chatData[_event][index].Descript;
        else
            return null;
    }

    public int GetEventSize(string _event)
    {
        return chatData[_event].Count;
    }
}

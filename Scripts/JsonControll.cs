using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonControll : MonoBehaviour
{
    public UserData userData;

    public void SaveData()
    {
        var json = JsonUtility.ToJson(userData);
        SetDataToFile();
        File.WriteAllText(Application.dataPath + "/Data.json", json);
    }

    public void LoadData() 
    {
        var jsonData = File.ReadAllText(Application.dataPath + "/Data.json");
        userData = JsonUtility.FromJson<UserData>(jsonData);
        GetDataFromFile();
    }

    private void SetDataToFile()
    {
        //플레이어 속성
        Player _player = GameManager.Instance.player;
        userData.Name = _player.personName;
        userData.Money = _player.money;
        userData.Charm = _player.charm;
        userData.Knowledge = _player.knowledge;
        userData.Strngth = _player.strength;

        //스테이지 속성
    }

    private void GetDataFromFile()
    {
        Player _player = GameManager.Instance.player;
        _player.personName = userData.Name;
        _player.money = userData.Money;
        _player.charm = userData.Charm;
        _player.knowledge = userData.Knowledge;
        _player.strength = userData.Strngth;
    }
}

[Serializable]
public class UserData
{
    [Header("플레이어 기본 속성")]
    public string Name;
    public int Money;
    public int Charm;
    public int Knowledge;
    public int Strngth;

    [Header("현재 플레이어 스테이지")]
    public int Scene;   //1 : 학교, 2 : 운동장, 3 : 도서관
    public int Day;
    public List<NPCData> NPCList;
}

[Serializable]
public class NPCData
{
    public string Name;
    public string Event;
    public int Phase;
}